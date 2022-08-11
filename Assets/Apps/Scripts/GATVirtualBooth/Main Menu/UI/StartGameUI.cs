using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace GATVirtualBooth.Menu
{
    public class StartGameUI : MonoBehaviour
    {
        private Button StartButton => GetComponent<Button>();

        private void Awake()
        {
            StartButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            throw new NotImplementedException();
        }
    }
}
