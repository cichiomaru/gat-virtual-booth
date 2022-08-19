using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabGAT.InputSystem;
using System;
using UnityEngine.AI;

namespace GATVirtualBooth.Game
{
    public class Player : MonoBehaviour, IEntity, IAttribute
    {
        //components
        [SerializeField] private InputSO input;
        public NavMeshAgent Agent => GetComponent<NavMeshAgent>();
        public Animator Animator => GetComponentInChildren<Animator>();

        //attribute
        public Vector3 MovementDirection { get; set; }


        //actions
        public event Action<Vector3> OnDirectionSet;

        public void Interact(IInteractible interactible)
        {
            throw new NotImplementedException();
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
    }
}
