using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LabGAT.InputSystem
{
    [CreateAssetMenu(fileName = "InputSO", menuName = "Scriptable Objects/InputSO")]
    public class InputSO : ScriptableObject, CustomInput.IGameplayActions
    {
        private CustomInput input;

        public event Action<Vector2> MovementDirection;


        private void OnEnable()
        {
            if (input is null)
            {
                input = new CustomInput();
                input.Gameplay.SetCallbacks(this);

                input.Enable();
            }

            input.Gameplay.Enable();
        }

        private void OnDisable()
        {
            input.Gameplay.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Canceled)
            {
                MovementDirection?.Invoke(context.ReadValue<Vector2>());
            }
        }
    }
}
