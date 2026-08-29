using System;
using FTRGames.Alpaseh.Views;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace FTRGames.Alpaseh.Services
{
    public sealed class GameInputService : IDisposable
    {
        private readonly InputAction numberAction;
        private readonly InputAction deleteAction;
        private readonly InputAction submitAction;

        public UnityEvent<int> NumberPressed { get; } = new UnityEvent<int>();
        public UnityEvent DeletePressed { get; } = new UnityEvent();
        public UnityEvent SubmitPressed { get; } = new UnityEvent();

        public GameInputService()
        {
            numberAction = new InputAction("Number", InputActionType.Button);

            for (int i = 0; i <= 9; i++)
            {
                numberAction.AddBinding($"<Keyboard>/digit{i}");
                numberAction.AddBinding($"<Keyboard>/numpad{i}");
            }

            deleteAction = new InputAction("Delete", InputActionType.Button, "<Keyboard>/backspace");
            deleteAction.AddBinding("<Keyboard>/delete");

            submitAction = new InputAction("Submit", InputActionType.Button, "<Keyboard>/enter");
            submitAction.AddBinding("<Keyboard>/numpadEnter");

            numberAction.performed += OnNumberPerformed;
            deleteAction.performed += OnDeletePerformed;
            submitAction.performed += OnSubmitPerformed;
        }

        public void Enable()
        {
            numberAction.Enable();
            deleteAction.Enable();
            submitAction.Enable();
        }

        public void EnterNumber(GameView gameView, int buttonIndex)
        {
            if (buttonIndex < 0 || buttonIndex > 9)
            {
                return;
            }

            if (gameView.enteredNumberWordText.text.Length >= gameView.questionText.text.Length)
            {
                return;
            }

            gameView.enteredNumberWordText.text += GetNumberValue(buttonIndex);
        }

        public void Delete(GameView gameView)
        {
            if (string.IsNullOrEmpty(gameView.enteredNumberWordText.text))
            {
                return;
            }

            gameView.enteredNumberWordText.text = gameView.enteredNumberWordText.text.Remove(
                gameView.enteredNumberWordText.text.Length - 1);
        }

        public void Dispose()
        {
            numberAction.performed -= OnNumberPerformed;
            deleteAction.performed -= OnDeletePerformed;
            submitAction.performed -= OnSubmitPerformed;

            numberAction.Dispose();
            deleteAction.Dispose();
            submitAction.Dispose();
        }

        private void OnNumberPerformed(InputAction.CallbackContext context)
        {
            if (context.control is not KeyControl keyControl)
            {
                return;
            }

            int buttonIndex = KeyToNumber(keyControl.keyCode);

            if (buttonIndex >= 0)
            {
                NumberPressed.Invoke(buttonIndex);
            }
        }

        private void OnDeletePerformed(InputAction.CallbackContext context)
        {
            DeletePressed.Invoke();
        }

        private void OnSubmitPerformed(InputAction.CallbackContext context)
        {
            SubmitPressed.Invoke();
        }

        private static int KeyToNumber(Key key)
        {
            return key switch
            {
                Key.Digit0 or Key.Numpad0 => 0,
                Key.Digit1 or Key.Numpad1 => 1,
                Key.Digit2 or Key.Numpad2 => 2,
                Key.Digit3 or Key.Numpad3 => 3,
                Key.Digit4 or Key.Numpad4 => 4,
                Key.Digit5 or Key.Numpad5 => 5,
                Key.Digit6 or Key.Numpad6 => 6,
                Key.Digit7 or Key.Numpad7 => 7,
                Key.Digit8 or Key.Numpad8 => 8,
                Key.Digit9 or Key.Numpad9 => 9,
                _ => -1
            };
        }

        private static string GetNumberValue(int buttonIndex)
        {
            // Alpaseh's existing domain mapping intentionally maps button 9 to value 6.
            return buttonIndex == 9 ? "6" : buttonIndex.ToString();
        }
    }
}
