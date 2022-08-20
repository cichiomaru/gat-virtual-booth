using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace GATVirtualBooth.Game
{
    public class SettingButton : MonoBehaviour
    {
        private Button Button => GetComponent<Button>();
        private SettingUI SettingUI => FindObjectOfType<SettingUI>();


        private void Awake()
        {
            Button.onClick.AddListener(ShowSetting);
        }


        private void ShowSetting()
        {
            SettingUI.Show();
        }
    }
}
