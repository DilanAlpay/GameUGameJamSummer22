using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{

    public Area areaN, areaE, areaS, areaW;

    private Transform _parentPathways;
    private Transform _parentEnemies;
    private Dictionary<Direction, Area> _neighbors;
    private Dictionary<Direction, Pathway> _dictPath;
    private Dictionary<Direction, GameObject> _dictEnemies;

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
            }
            //Otherwise we turn off that part of the geometry
            else
            {
                path.gameObject.SetActive(false);
                path = null;
            }

            //We always set the path, even if it is empty to prevent an NRE
            _dictPath.Add(dir, path);

            //Add the enemies to the dictionary
            _dictEnemies.Add(dir, _parentEnemies.GetChild(i).gameObject);
        }
    }
    
    /// <summary>
    /// The character has moved here from this direction
    /// </summary>
    /// <param name="dir"></param>
    public void EnterFrom(PlayerMovement player, Direction dir)
    {

    }

    /// <summary>
    /// Handles when a character leaves through a Pathway
    /// </summary>
    public void ExitFrom(PlayerMovement player, Direction dir)
    {
        gameObject.SetActive(false);
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
}
