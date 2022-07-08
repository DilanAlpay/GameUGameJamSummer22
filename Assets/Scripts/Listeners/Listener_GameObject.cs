using UnityEngine;
using UnityEngine.Events;
public class Listener_GameObject : ListenerBase<GameObject>
{
    public UnityEvent_GameObject myResponse;

    public override void Call(GameObject obj)
    {
        response = myResponse;
        base.Call(obj);
    }
}


[System.Serializable]
public class UnityEvent_GameObject : UnityEvent<GameObject>
{

}