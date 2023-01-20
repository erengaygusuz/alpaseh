using UnityEngine;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Core
{
    public class TweenService
    {
        public void TweenText(GameObject tweenObject, string textValue, Color colorValue, bool isEarned)
        {
            if (isEarned)
            {
                tweenObject.GetComponent<Text>().text = "+" + textValue;
            }

            else
            {
                tweenObject.GetComponent<Text>().text = "-" + textValue;
            }

            LeanTween.scale(tweenObject, Vector3.one * 4, 2.0f).setEasePunch();

            LeanTween.value(tweenObject, 0.1f, 1.0f, 2.0f).setOnUpdate((value) =>
            {
                tweenObject.GetComponent<Text>().color = Color.Lerp(tweenObject.GetComponent<Text>().color, colorValue, value);
            });

            LeanTween.value(tweenObject, 1.0f, 0.0f, 1.5f).setOnUpdate((value) =>
            {
                tweenObject.GetComponent<Text>().color = new Color(tweenObject.GetComponent<Text>().color.r, tweenObject.GetComponent<Text>().color.g, tweenObject.GetComponent<Text>().color.b, value);
            }).setOnComplete(() =>
            {
                tweenObject.SetActive(false);
            });
        }
    }
}
