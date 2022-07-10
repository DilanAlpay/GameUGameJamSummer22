using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [ContextMenu("Name Areas")]
    public void NameAreas()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name != "START")
                child.name = "Area " + i;
        }
    }

    /*
    [ContextMenu("Connect Neighbors")]
    public void ConnectNeighbors()
    {        
        foreach(Area area in GetComponentsInChildren<Area>())
        {
            if(area.areaN != null && area.areaN.areaS == null)
            {
                area.areaN.areaS = area;
            }
            if (area.areaE != null && area.areaE.areaW == null)
            {
                area.areaE.areaW = area;
            }
            if (area.areaS != null && area.areaS.areaN == null)
            {
                area.areaS.areaN = area;
            }
            if (area.areaW != null && area.areaW.areaE == null)
            {
                area.areaW.areaE = area;
            }
        }

        ShowPaths();
    }
    */

    [ContextMenu("Show Pathways")]
    public void ShowPaths()
    {
        foreach (Area area in GetComponentsInChildren<Area>())
        {
            Transform pathParent = area.transform.Find("Pathways");
            pathParent.GetChild(0).gameObject.SetActive(area.areaN);
            pathParent.GetChild(1).gameObject.SetActive(area.areaE);
            pathParent.GetChild(2).gameObject.SetActive(area.areaS);
            pathParent.GetChild(3).gameObject.SetActive(area.areaW);
        }
    }



}
