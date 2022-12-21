using System.Collections;
using UnityEngine;

namespace OnlineChess.Infrastructure
{
    public sealed class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public void Show()
        {
            gameObject.SetActive(true);

            _canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (_canvasGroup.alpha > 0f)
            {
                _canvasGroup.alpha -= 0.03f;

                yield return new WaitForSeconds(0.03f);
            }
            
            gameObject.SetActive(false);
        }
    }
}