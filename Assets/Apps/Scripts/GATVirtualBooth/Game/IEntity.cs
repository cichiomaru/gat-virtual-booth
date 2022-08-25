using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace GATVirtualBooth.Game
{
    public interface IEntity
    {
        public Animator Animator { get; }
        public NavMeshAgent Agent { get; }

        public void SetDirection(Vector3 direction);
        public void PositionUpdate();
    }
}