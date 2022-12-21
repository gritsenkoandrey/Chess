using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace OnlineChess.Scripts.Extensions
{
    public static class UIAnimationExtension
    {
        public static IEnumerator ImageFadeAndScale(this Image image, float speed)
        {
            Color c = image.color;
            float scale = 1f;
            float fade = 1f;

            while (fade > 0f)
            {
                yield return null;
                
                scale += Time.deltaTime * speed;
                fade -= Time.deltaTime * speed;

                image.transform.localScale = Vector3.one * scale;

                Color color = new Color(c.r, c.g, c.b, fade);

                image.color = color;
            }
            
            image.gameObject.SetActive(false);
        }
        
        public static IEnumerator ButtonScaleZero(this Button button, float speed)
        {
            button.transform.localScale = Vector3.one;
            
            button.interactable = false;

            float scale = 1f;
            
            while (scale > 0f)
            {
                yield return null;

                scale -= Time.deltaTime * speed;

                button.transform.localScale = Vector3.one * EaseOutBack(scale);
            }
            
            button.interactable = true;
            
            button.gameObject.SetActive(false);
        }
        
        public static IEnumerator ButtonScaleOne(this Button button, float speed)
        {
            button.gameObject.SetActive(true);

            button.transform.localScale = Vector3.zero;

            button.interactable = false;
            
            float scale = 0f;

            while (scale < 1f)
            {
                yield return null;

                scale += Time.deltaTime * speed;

                button.transform.localScale = Vector3.one * EaseOutBack(scale);
            }
            
            button.interactable = true;
        }
        
        private static float EaseOutBack(float number)
        {
            float c1 = 0.70158f;
            float c3 = c1 + 1f;

            return 1f + c3 * Mathf.Pow(number - 1f, 3) + c1 * Mathf.Pow(number - 1f, 2);
        }
    }
}