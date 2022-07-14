using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zone : MonoBehaviour
{    
    public enum Tag
    {
        HOME,
        NORTHWEST,
        NORTHEAST,
        SOUTHWEST,
        SOUTHEAST
    }

    public Tag zoneName;

    /// <summary>
    /// Typically called when the player dies in the zone
    /// </summary>
    public void Unload()
    {
        SceneManager.UnloadSceneAsync(zoneName.ToString());
    }
}
