using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class HealthBarCanvasView : MonoBehaviour
    {

        List<HealthPanelView> panels = new List<HealthPanelView>();

        void Awake()
        {  
            panels.Clear();
        }

        public void AddToCanvas(HealthPanelView objectToAdd)
        {    
            panels.Add(objectToAdd);
            Sort();
        }

        void Sort()
        {

            for (int i = 0; i < panels.Count; i++)
            { 
                panels[i].transform.SetSiblingIndex(i);
            }
            
            panels.Clear();
        }
    }
}

