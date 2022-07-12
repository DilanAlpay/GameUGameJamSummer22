using UnityEngine;

public class Targetter : MonoBehaviour
{
    public LayerMask targetLayers;
    public LayerMaskEvent assignTargets;
    public void Awake()
    {
        assignTargets?.Invoke(targetLayers);
    } 
}
