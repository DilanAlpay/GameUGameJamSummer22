using UnityEngine;
using UnityEngine.Events;

public class PauseableObject : MonoBehaviour, IPausable
{
    public UnityEvent pause;
    public UnityEvent unpause;
    public void Pause()
    {
        pause?.Invoke();
    }

    public void Unpause()
    {
        unpause?.Invoke();
    }
}
