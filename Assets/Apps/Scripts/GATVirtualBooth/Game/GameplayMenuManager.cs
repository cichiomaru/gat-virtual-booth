using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class GameplayMenuManager : MonoBehaviour
    {
        public static GameplayMenuManager instance;
        public Dictionary<string, IWidget> widgets = new();

        private void Awake()
        {
            if (instance is null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            foreach(GameObject go in FindObjectsOfType<GameObject>())
            {
                IWidget widget = go.GetComponent<IWidget>();

                if (widget is not null)
                {
                    widgets.Add(widget.Path, widget);
                }
            }
        }

        public IWidget Show(string path)
        {
            widgets[path].Show();
            return widgets[path];
        }

        internal void Hide(string path)
        {
            widgets[path].Hide();
        }
    }
}
