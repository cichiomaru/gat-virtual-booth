using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace GATVirtualBooth.Game
{
    public class SettingUI : MonoBehaviour, IWidget
    {
        public string Path => GameplayUIPath.Setting;


        #region component
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button okButton;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;
        #endregion

        #region unity function

        private void Start()
        {
            LoadSettingData();
            Hide();
        }

        private void Awake()
        {
            cancelButton.onClick.AddListener(Cancel);
            okButton.onClick.AddListener(OK);
        }

        #endregion


        private void LoadSettingData()
        {
            bgmSlider.value = PlayerPrefs.GetFloat("bgm", 1f);
            sfxSlider.value = PlayerPrefs.GetFloat("sfx", 1f);
        }

        private void SaveSettingData()
        {
            PlayerPrefs.SetFloat("bgm", bgmSlider.value);
            PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        }

        private void OK()
        {
            SaveSettingData();
            Hide();
        }

        private void Cancel()
        {
            LoadSettingData();
            Hide();
        }

        public void Hide()
        {
            canvas.enabled = false;
        }

        public void Show()
        {
            canvas.enabled = true;
        }

        public void SetContent(DataModel content)
        {
            throw new NotImplementedException();
        }
    }
}
