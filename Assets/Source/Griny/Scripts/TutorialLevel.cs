using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WordGame
{
    public class TutorialLevel : MonoBehaviour
    {
        [SerializeField] private Image _hand;
        [SerializeField] private Image _wasd;

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
            Time.timeScale = 0;

            PlayIndicator(controller);

            _coroutine = StartCoroutine(WaitDownButton(controller));

            DisableIndicator(controller);
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
            while (Input.anyKey == false)
            {
                yield return null;
            }

            DisableIndicator(controller);

            Time.timeScale = 1;
        }
    }
}