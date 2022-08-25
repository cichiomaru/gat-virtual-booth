using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class VirtualJoystick : MonoBehaviour
    {
        private Joystick Joystick => GetComponent<Joystick>();
        private Player Player => FindObjectOfType<Player>();

        private void Update()
        {
            Player.SetDirection(Joystick.Direction);
        }
    }
}
