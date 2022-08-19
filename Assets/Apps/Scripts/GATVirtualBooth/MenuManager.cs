using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GATVirtualBooth
{
    public abstract class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        [SerializeField] Transform parent;

        private Dictionary<string, IWidget> widgetList;
        private Stack<IWidget> widgetStack;

        private Func<string, int> NumberSet;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            widgetList = new Dictionary<string, IWidget>();
            widgetStack = new Stack<IWidget>();

            GetAllWidget();
        }

        public void Show(string path)
        {
            if (widgetStack.Contains(widgetList[path]))
                return;

            if (widgetStack.Count > 0)
            {
                widgetStack.Peek().Hide();
            }

            widgetList[path].Show();
            widgetStack.Push(widgetList[path]);
        }
        
        private void GetAllWidget()
        {
            foreach(var obj in FindObjectsOfType<GameObject>())
            {
                IWidget widget = obj.GetComponent<IWidget>();
                if (widget is not null)
                {
                    widgetList.Add(widget.Path, widget);                    
                }
            }
        }
    }
}
