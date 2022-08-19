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

        public event Action<Vector3> OnDirectionSet;
        private Vector3 direction = new Vector3();


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
                direction.x = context.ReadValue<Vector2>().x;
                direction.z = context.ReadValue<Vector2>().y;

                OnDirectionSet?.Invoke(direction);
            }
        }
    }
}
