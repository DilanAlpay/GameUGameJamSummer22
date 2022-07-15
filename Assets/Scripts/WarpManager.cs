using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public Player player;
    public Area startingArea;
    public List<Area> nearbyAreas;
    private bool _warping = false;
    
    public void PrepWarp()
    {
        _warping = true;
    }

    public void Warp()
    {
        if (!_warping) return;
        startingArea.gameObject.SetActive(true);
        player.ReturnToStart();

        foreach (Area area in nearbyAreas)
        {
            area.gameObject.SetActive(false);
        }
        _warping = false;
    }

}
