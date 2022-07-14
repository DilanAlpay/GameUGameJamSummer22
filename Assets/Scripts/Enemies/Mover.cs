using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] NavMeshMover nmover;
    [SerializeField] IMoverBrain brain;
    private void Awake()
    {
        if (nmover == null)
            nmover = GetComponent<NavMeshMover>();

        if(brain == null)
        {
            brain = GetComponent<IMoverBrain>();
            if(brain == null)
            {
                brain = gameObject.AddComponent<DumbBehavior>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        brain.Move(nmover);
    }
}
