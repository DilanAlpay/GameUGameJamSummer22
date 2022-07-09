using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    public UnityEvent onStartOfGame;
    // Start is called before the first frame update
    void Start()
    {
        onStartOfGame?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
