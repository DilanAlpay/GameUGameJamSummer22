using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject obj;

    public void Activate()
    {
        obj.SetActive(true);
    }

    public void Deactivate()
    {
        obj.SetActive(false);
    }
}