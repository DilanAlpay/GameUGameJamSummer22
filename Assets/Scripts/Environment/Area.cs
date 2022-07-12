using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Area : MonoBehaviour
{
    public Area areaN, areaE, areaS, areaW;

    public UnityEvent onNoItem;
    public UnityEvent onExit;

    private Transform _parentPathways;
    private Transform _parentEnemies;
    private Transform _parentWalls;

    private Dictionary<Direction, Area> _neighbors;
    private Dictionary<Direction, Pathway> _dictPath;
    private Dictionary<Direction, GameObject> _dictEnemies;

    /// <summary>
    /// Temporarily stored when moving between Areas
    /// </summary>
    private Player _movingThis;
    private Direction _moveDirection;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        //Saves the neighbors of this area in a dictionary
        _neighbors = new Dictionary<Direction, Area>();
        _neighbors.Add(Direction.NORTH, areaN);
        _neighbors.Add(Direction.EAST, areaE);
        _neighbors.Add(Direction.SOUTH, areaS);
        _neighbors.Add(Direction.WEST, areaW);

        //Grabs the children of this object...
        _parentPathways = transform.Find("Pathways");
        _parentEnemies = transform.Find("Enemies");
        _parentWalls = transform.Find("Walls");

        //... then places them in dictionaries for easy access
        _dictPath = new Dictionary<Direction, Pathway>();
        _dictEnemies = new Dictionary<Direction, GameObject>();

        for (int i = 0; i < 4; i++)
        {
            Direction dir = (Direction)i;
            Pathway path = _parentPathways.GetChild(i).GetComponent<Pathway>();

            //We set the path if we have a neighbor in that direction
            if (_neighbors[dir] != null)
            {
                path.Initialize(this, dir);

                //And turn off the full-length wall
                _parentWalls.GetChild(i).gameObject.SetActive(false);
            }
            //Otherwise...
            else
            {
                //we turn off that part of the geometry
                path.gameObject.SetActive(false);
                path = null;
            }

            //We always set the path, even if it is empty to prevent an NRE
            _dictPath.Add(dir, path);

            //Add the enemies to the dictionary
            _dictEnemies.Add(dir, _parentEnemies.GetChild(i).gameObject);
        }

        //All start off by default except for the starting area.
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Handles when a character leaves through a Pathway
    /// </summary>
    public void ExitFrom(Player player, Direction dir)
    {
        //If the player does not have their throwable, we say they cannot leave
        if(player.HasBall)
        {
            _movingThis = player;
            _moveDirection = dir;
            onExit.Invoke();
        }
        else
        {
            onNoItem.Invoke();
        }

    }


    public void MoveCharacter()
    {
        if (!_movingThis) return;

        int otherWay = (int)_moveDirection;
        otherWay = (otherWay + 2) % 4;
        _neighbors[_moveDirection].EnterFrom(_movingThis, (Direction)otherWay);

        _movingThis = null;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// The character has moved here from this direction
    /// </summary>
    /// <param name="dir"></param>
    public void EnterFrom(Player player, Direction dir)
    {
        gameObject.SetActive(true);
        print(_dictPath[dir]);
        player.TeleportTo(_dictPath[dir].Position);
    }



    private void OnEnable()
    {
        RestoreEnemies();
    }


    /// <summary>
    /// Respawn all defeated enemies in this Area
    /// </summary>
    private void RestoreEnemies()
    {

    }

    private void OnDrawGizmos()
    {
        if (areaN)
        {
            Gizmos.color = Color.yellow;
            Vector3 posA = transform.position + transform.forward * 15 + transform.right * 2;
            Vector3 posB = areaN.transform.position - areaN.transform.forward * 15 + transform.right * 2;
            Gizmos.DrawLine(posA, posB);
        }
        if (areaE)
        {
            Gizmos.color = Color.red;
            Vector3 posA = transform.position + transform.right * 17 + transform.forward * 2;
            Vector3 posB = areaE.transform.position - areaE.transform.right * 17 + transform.forward * 2;
            Gizmos.DrawLine(posA, posB);
        }
        if (areaS)
        {
            Gizmos.color = Color.green;
            Vector3 posA = transform.position - transform.forward * 15 + transform.right * -2;
            Vector3 posB = areaS.transform.position + areaS.transform.forward * 15 + transform.right * -2;
            Gizmos.DrawLine(posA, posB);
        }
        if (areaW)
        {
            Gizmos.color = Color.blue;
            Vector3 posA = transform.position - transform.right * 17 + transform.forward * -2;
            Vector3 posB = areaW.transform.position + areaW.transform.right * 17 + transform.forward * -2;
            Gizmos.DrawLine(posA, posB);
        }
    }



}
