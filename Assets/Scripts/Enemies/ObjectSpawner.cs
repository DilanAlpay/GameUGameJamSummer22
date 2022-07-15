using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public void SpawnObject(Vector3 position)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        obj.transform.SetParent(transform.parent.parent);
    }

    public void SpawnObject()
    {
        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.SetParent(transform.parent.parent);
    }
}