using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public void SpawnObject(Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }

    public void SpawnObject()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}