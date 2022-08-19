using System.Threading.Tasks;
using UnityEngine;

namespace LabGAT.SceneTransition {
    public interface ISceneTransition {
        public Canvas canvas { get; }

        public Task OpenTransition();
        public Task CloseTransition();
    }
}