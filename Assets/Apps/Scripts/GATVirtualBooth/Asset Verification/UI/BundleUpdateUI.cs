using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace GATVirtualBooth.AssetVerification
{
    public class BundleUpdateUI : MonoBehaviour, IHideShow
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Slider loadingBar;
        [SerializeField] private TMP_Text loadingProgress;


        private void Start()
        {
            Hide(loadingBar.gameObject);
        }

        public void Hide(GameObject obj)
        {
            obj.SetActive(false);
        }

        public void Show(GameObject obj)
        {
            obj.SetActive(true);
        }
    }
}
