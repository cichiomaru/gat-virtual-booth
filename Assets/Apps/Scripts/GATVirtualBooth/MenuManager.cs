using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GATVirtualBooth
{
    public class MenuManager : MonoBehaviour
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

            NumberSet += Test;
            NumberSet += Test2;

            print(NumberSet?.Invoke("zero"));
        }
        private int Test2(string number)
        {
            return 1 + 1;
        }
        private int Test (string number)
        {
            return 1;
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
                widgetList.Add(widget.Path, widget);
            }
        }
    }
}
