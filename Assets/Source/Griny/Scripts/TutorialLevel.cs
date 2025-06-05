using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

namespace WordGame
{
    public class TutorialLevel : MonoBehaviour
    {
        private const string _tutorialKey = "tutorial";

        [SerializeField] private Image _hand;
        [SerializeField] private Image _wasd;
        [SerializeField] private string _keyPrefse;

        private bool _isMobile;
        private Coroutine _coroutine;

        private void Awake()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        _isMobile = Device.IsMobile;
#endif
            if (_isMobile)
            {
                CheckSave(_hand);
            }
            else
            {
                CheckSave(_wasd);
            }
        }

        private void CheckSave(Image controller)
        {
            if (PlayerPrefs.HasKey(_keyPrefse) == false)
            {
                Time.timeScale = 0;

                PlayIndicator(controller);

                if(_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(WaitDownButton(controller));

                PlayerPrefs.SetString(_keyPrefse, _tutorialKey);
            }
            else
            {
                DisableIndicator(controller);
            }
        }

        private void PlayIndicator(Image controller)
        {
            controller.gameObject.SetActive(true);
        }

        private void DisableIndicator(Image controller)
        {
            controller.gameObject.SetActive(false);
        }

        private IEnumerator WaitDownButton(Image controller)
        {
            while(Input.anyKey)
            {
                yield return null;
            }

            DisableIndicator(controller);

            Time.timeScale = 1;
        }
    }
}