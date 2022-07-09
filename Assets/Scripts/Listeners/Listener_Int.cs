using UnityEngine.Events;
public class Listener_Int : ListenerBase<int>
{
    public Event_Int sup;
}

[System.Serializable]
public class Event_Int : UnityEvent2<int> { }
