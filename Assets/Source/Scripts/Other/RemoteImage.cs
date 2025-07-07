using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace LeaderboardBase
{
    public class RemoteImage
    {
        private readonly string _url;

        private Texture2D _texture;

        public Texture2D Texture
        {
            get
            {
                if (IsDownloadFinished == false)
                {
                    throw new InvalidOperationException($"Attempt to get {nameof(Texture)} while {nameof(IsDownloadFinished)} = {IsDownloadFinished}");
                }

                if (IsDownloadSuccessful == false)
                {
                    throw new InvalidOperationException($"Attempt to get {nameof(Texture)} while {nameof(IsDownloadSuccessful)} = {IsDownloadSuccessful}");
                }

                return _texture;
            }

            private set
            {
                _texture = value;
            }
        }

        public bool IsDownloadFinished { get; private set; } = false;

        public bool IsDownloadSuccessful { get; private set; } = false;

        public string DownloadErrorMessage { get; private set; }

        public RemoteImage(string url)
        {
            _url = url;
        }

        public async void Download(Action<Texture2D> successCallback = null, Action<string> errorCallback = null, CancellationToken cancellationToken = default)
        {
            using (UnityWebRequest downloadTextureWebRequest = UnityWebRequestTexture.GetTexture(_url))
            {
                UnityWebRequestAsyncOperation downloadOperation = downloadTextureWebRequest.SendWebRequest();

                while (downloadOperation.isDone == false)
                {
                    if (cancellationToken.IsCancellationRequested == true)
                    {
                        break;
                    }

                    await Task.Yield();
                }

                IsDownloadFinished = true;

                if (cancellationToken.IsCancellationRequested == true)
                {
                    DownloadErrorMessage = $"Download interrupted via {nameof(CancellationToken)}";
                }
                else if (downloadOperation.webRequest.result != UnityWebRequest.Result.Success)
                {
                    DownloadErrorMessage = downloadOperation.webRequest.error;
                }
                else
                {
                    _texture = DownloadHandlerTexture.GetContent(downloadTextureWebRequest);

                    if (_texture != null)
                    {
                        IsDownloadSuccessful = true;
                    }
                    else
                    {
                        DownloadErrorMessage = "Getting content of a downloaded texture has failed.";
                    }
                }
            }

            if (IsDownloadSuccessful)
            {
                successCallback?.Invoke(_texture);
            }
            else
            {
                errorCallback?.Invoke(DownloadErrorMessage);
            }
        }
    }
}