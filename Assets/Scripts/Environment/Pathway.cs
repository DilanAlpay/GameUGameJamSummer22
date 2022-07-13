using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathway : MonoBehaviour
{
    /// <summary>
    /// The area this pathway is a part of
    /// </summary>
    protected Area _area;

    /// <summary>
    /// How far locally from the center
    /// the player will be when entering this pathway
    /// </summary>
    private float _enterOffset = 0.5f;

    /// <summary>
    /// The direction this pathway is headed
    /// RELATIVE TO THE AREA IT IS IN
    /// </summary>
    private Direction _direction;

    public Vector3 Position { get { return transform.position - (transform.forward * _enterOffset); } }

    public void Initialize(Area a, Direction d)
    {
        _area = a;
        _direction = d;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInChildren<Player>();

        //The player has entered, so now we tell the area that we are leaving
        if (player)
            MovePlayer(player);
    }

    protected virtual void MovePlayer(Player player)
    {
        _area.ExitFrom(player, _direction);
    }
}
