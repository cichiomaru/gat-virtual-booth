using UnityEngine;

namespace GATVirtualBooth
{
    public abstract class Widget : MonoBehaviour
    {
        public string path;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}