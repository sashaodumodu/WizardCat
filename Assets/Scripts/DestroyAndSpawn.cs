using UnityEngine;

public class DestroyAndSpawn : MonoBehaviour
{
    public string childNameToDetect;
    public GameObject[] objectsToSpawn;

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(childNameToDetect))
        {
            // Get the full object (root of dropped item)
            GameObject otherRoot = other.transform.root.gameObject;

            // Store spawn position BEFORE destroying
            Vector3 spawnPos = (transform.position + otherRoot.transform.position) / 2f;

            // Destroy both objects
            Destroy(otherRoot);   // dropped item parent
            Destroy(gameObject);  // this object

            // Spawn new item
            foreach (GameObject obj in objectsToSpawn)
            {
                Instantiate(obj, spawnPos, Quaternion.identity);
            }
        }
    }
}