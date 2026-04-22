using UnityEngine;

public class DestroyAndShow : MonoBehaviour 
{
    public string childNameToDetect;
    public GameObject[] objectToShow; // multiple objects

    void Start()
    {
        if (objectToShow != null)
        {
            foreach (GameObject obj in objectToShow)
            {
                if (obj != null)
                    obj.SetActive(false); // hide all
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(childNameToDetect))
        {
            GameObject otherRoot = other.transform.root.gameObject;

            // Show all objects
            foreach (GameObject obj in objectToShow)
            {
                if (obj != null)
                    obj.SetActive(true);
            }

            // Destroy both
            Destroy(otherRoot);
            Destroy(gameObject);
        }
    }
}