using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

namespace GATVirtualBooth.Game
{
    public class Player : MonoBehaviour, IEntity, IAttribute, IInteraction
    {
        #region components
        public NavMeshAgent Agent => GetComponent<NavMeshAgent>();
        public Animator Animator => GetComponentInChildren<Animator>();
        #endregion

        #region attribute
        public Vector3 MovementDirection { get; set; }
        private List<IInteractible> interactibles = new();
        #endregion


        #region actions
        //actions
        public event Action<Vector3> OnDirectionSet;
        public event Action<IInteractible> OnRegisterInteractible;
        public event Action<IInteractible> OnUnregisterInteractible;
        #endregion


        public void Interact()
        {
            if (interactibles.Count > 0)
            {
                interactibles[0].Execute();
                Logger.Log($"Interact with {interactibles[0].GetName()}.");
            }
        }

        public void SetDestination(Vector3 direction)
        {
            MovementDirection = direction;
        }

        public void PositionUpdate()
        {
            Agent.SetDestination(transform.position + MovementDirection);
            Animator.SetFloat("speed", MovementDirection.magnitude);
        }
        
        public void RegisterInteractible(IInteractible interactible)
        {
            if (!interactibles.Contains(interactible))
            {
                interactibles.Add(interactible);
                OnRegisterInteractible?.Invoke(interactibles[0]);
                Logger.Log($"{interactible.GetName()} is nearby.");
            }
        }
        
        public void UnregisterInteractible(IInteractible interactible)
        {
            if (interactibles.Contains(interactible))
            {
                interactibles.Remove(interactible);

                IInteractible firstInteractible = interactibles.Count > 0 ? interactibles[0] : null;
                OnUnregisterInteractible?.Invoke(firstInteractible);

                Logger.Log($"{interactible.GetName()} is out of interaction area.");
            }
        }


        #region unity functions
        private void Update()
        {
            PositionUpdate();
        }
        #endregion
    }
}
