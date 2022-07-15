using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathwayWarp : Pathway
{
    public Transform destination;

    /// <summary>
    /// If this is set then it will additively load in a level
    /// </summary>
    public Zone.Tag zone;

    /// <summary>
    /// If this is true, we will want to either unload or reload a scene when this is done
    /// </summary>
    public bool entering;

    protected override void MovePlayer(Player player)
    {
        if(!entering)
        {
            WarpManager.TurnOnArea((int)zone);
        }
        _area.WarpOut(player, destination.position, zone, entering);
    }   

}
