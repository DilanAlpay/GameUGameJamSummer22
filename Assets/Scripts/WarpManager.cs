using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public Player player;
    public Area startingArea;
    public List<Area> nearbyAreas;
    private bool _warping = false;
    private static int _turningOnArea = -1;

    public void PrepWarp()
    {
        _warping = true;
    }

    public void Warp()
    {
        if (_warping)
        {
            startingArea.gameObject.SetActive(true);
            player.ReturnToStart();

            foreach (Area area in nearbyAreas)
            {
                area.gameObject.SetActive(false);
            }
            _warping = false;
        }
        else if (_turningOnArea >= 0)
        {
            nearbyAreas[_turningOnArea].gameObject.SetActive(true);
            _turningOnArea = -1;
        }
    }

    public static void TurnOnArea(int area)
    {
        _turningOnArea = area-1;
    }

}
