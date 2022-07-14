using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Testaroonie : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        print("It works!");
    }
}
