using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FTRGames.Alpaseh.Services
{
    public class TweenService
    {
        public UnityEvent playWrongAnswerAnimEvent;
        public UnityEvent playCorrectAnswerAnimEvent;
        public UnityEvent tweenTextEvent;

        public void Initialization()
        {
            playCorrectAnswerAnimEvent = new UnityEvent();
            playWrongAnswerAnimEvent = new UnityEvent();
            tweenTextEvent = new UnityEvent();
        }

        public void TweenText(GameObject tweenObject, string textValue, Color colorValue, bool isEarned, Button checkButton)
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
                tweenObject.GetComponent<Text>().color = new Color(
                    tweenObject.GetComponent<Text>().color.r, 
                    tweenObject.GetComponent<Text>().color.g, 
                    tweenObject.GetComponent<Text>().color.b, value);

            }).setOnComplete(() =>
            {
                tweenObject.SetActive(false);
                checkButton.interactable = true;

                tweenTextEvent.Invoke();
            });
        }

        public void PlayWrongAnswerAnim(Text animatedText, Button checkButton)
        {
            checkButton.interactable = false;

            LeanTween.rotate(animatedText.gameObject, new Vector3(0, 0, 180), 1.0f);

            LeanTween.value(animatedText.gameObject, 0.1f, 1.0f, 1.0f).setOnUpdate((value) =>
            {
                animatedText.color = Color.Lerp(animatedText.color, new Color32(189, 56, 4, 255), value);

            }).setOnComplete(() =>
            {
                LeanTween.rotate(animatedText.gameObject, new Vector3(0, 0, 125), 1.0f).setDelay(0.5f).setEasePunch();

                LeanTween.value(animatedText.gameObject, 1.0f, 0.0f, 1.5f).setDelay(0.5f).setOnUpdate((value) =>
                {
                    animatedText.color = new Color(
                        animatedText.color.r,
                        animatedText.color.g,
                        animatedText.color.b, value);

                }).setOnComplete(() =>
                {
                    animatedText.gameObject.SetActive(false);

                    playWrongAnswerAnimEvent.Invoke();
                });
            });
        }

        public void PlayCorrectAnswerAnim(Text animatedText, Button checkButton)
        {
            checkButton.interactable = false;

            LeanTween.rotate(animatedText.gameObject, new Vector3(0, 0, 180), 1.0f);

            LeanTween.value(animatedText.gameObject, 0.1f, 1.0f, 1.0f).setOnUpdate((value) =>
            {
                animatedText.color = Color.Lerp(animatedText.color, new Color32(33, 173, 70, 255), value);

            }).setOnComplete(() =>
            {
                LeanTween.scale(animatedText.gameObject, Vector3.one * 2, 2.0f).setEasePunch();

                LeanTween.value(animatedText.gameObject, 1.0f, 0.0f, 1.5f).setDelay(0.5f).setOnUpdate((value) =>
                {
                    animatedText.color = new Color(
                        animatedText.color.r,
                        animatedText.color.g,
                        animatedText.color.b, value);

                }).setOnComplete(() =>
                {
                    animatedText.gameObject.SetActive(false);

                    playCorrectAnswerAnimEvent.Invoke();
                });
            });
        }
    }
}
