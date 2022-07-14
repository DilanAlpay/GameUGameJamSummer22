using UnityEngine;

public class RotationRandomizer : MonoBehaviour
{
    [SerializeField] float range;

    public void RandomizeRotation()
    {
        float newX = Random.Range(-range, range);
        float newY = Random.Range(-range, range);
        float newZ= Random.Range(-range, range);
        transform.Rotate(newX, newY, newZ) ;
    }

}
