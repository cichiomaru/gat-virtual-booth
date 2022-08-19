using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabGAT.InputSystem;
using System;
using UnityEngine.AI;

namespace GATVirtualBooth.Game
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private InputSO input;

        //components
        private NavMeshAgent agent => GetComponent<NavMeshAgent>();

        //actions
        public event Action<Vector2> OnDirectionSet;


        private void OnEnable()
        {
            input.MovementDirection += SetDirection;
        }

        private void OnDisable()
        {
            input.MovementDirection -= SetDirection;
        }


        private void SetDirection(Vector2 dir)
        {
            agent.SetDestination(transform.position + new Vector3(dir.x, 0, dir.y));
            OnDirectionSet?.Invoke(dir);
        }
    }
}
