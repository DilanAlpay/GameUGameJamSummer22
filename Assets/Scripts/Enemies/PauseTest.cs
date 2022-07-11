using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseTest : MonoBehaviour
{

    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {

        var pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>();
        isPaused = !isPaused;
        foreach(IPausable pausable in pausables)
        {
            if(isPaused)
                pausable.Pause();
            else
                pausable.Unpause();
            
        }
    }
}
