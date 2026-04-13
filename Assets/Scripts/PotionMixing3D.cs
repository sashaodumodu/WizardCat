using UnityEngine;

public class PotionMixing3D : MonoBehaviour
{
    public string objectAName = ("Dropped Item Prefab(Clone)");
    public string objectBName = ("Dropped Item Prefab(Clone)");

    public string childNameA;
    public string childNameB;

    public GameObject itemToSpawn;
    public Transform spawnPoint;
    
    void OnCollisionEnter(Collision collision)
    {
        GameObject objectA = GameObject.Find(objectAName);
        GameObject objectB = GameObject.Find(objectBName);

        if (objectA == null || objectB == null)
        {
            Debug.LogWarning("Could not find objects by name.");
            return;
        }

        if (HasChild(objectA.transform, childNameA) &&
            HasChild(objectB.transform, childNameB))
        {
            SpawnItem(objectA, objectB);

            Destroy(objectA);
            Destroy(objectB);
        }
    }

    void SpawnItem(GameObject a, GameObject b)
    {
        Vector3 spawnPos;

        if (spawnPoint != null)
            spawnPos = spawnPoint.position;
        else
            spawnPos = (a.transform.position + b.transform.position) / 2f;

        Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
    }

    bool HasChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return true;

            if (HasChild(child, childName))
                return true;
        }
        return false;
    }
}