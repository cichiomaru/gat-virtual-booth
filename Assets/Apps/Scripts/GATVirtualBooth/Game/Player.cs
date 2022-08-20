using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabGAT.InputSystem;
using System;
using UnityEngine.AI;

namespace GATVirtualBooth.Game
{
    public class Player : MonoBehaviour, IEntity, IAttribute, IInteraction
    {
        #region components
        //components
        [SerializeField] private InputSO input;
        public NavMeshAgent Agent => GetComponent<NavMeshAgent>();
        public Animator Animator => GetComponentInChildren<Animator>();
        #endregion

        #region attribute
        //attribute
        public Vector3 MovementDirection { get; set; }
        public List<IInteractible> Interactibles => new List<IInteractible>();
        #endregion


        #region actions
        //actions
        public event Action<Vector3> OnDirectionSet;
        #endregion


        public void Interact()
        {
            if (Interactibles.Count > 0)
            {
                Interactibles[0].Execute();
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
            if (!Interactibles.Contains(interactible))
            {
                Interactibles.Add(interactible);
            }
        }
        
        public void UnregisterInteractible(IInteractible interactible)
        {
            if (Interactibles.Contains(interactible))
            {
                Interactibles.Remove(interactible);
            }
        }


        #region unity functions
        private void Update()
        {
            PositionUpdate();
        }

        private void OnEnable()
        {
            input.OnDirectionSet += SetDestination;
        }

        private void OnDisable()
        {
            input.OnDirectionSet -= SetDestination;
        }
        #endregion
    }
}
