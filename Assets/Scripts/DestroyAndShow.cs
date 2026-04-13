using UnityEngine;

public class DestroyAndShow : MonoBehaviour 
{
    public string childNameToDetect;
    public GameObject objectToShow; // assign in Inspector

    void Start()
    {
        if (objectToShow != null)
            objectToShow.SetActive(false); // hide at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(childNameToDetect))
        {
            GameObject otherRoot = other.transform.root.gameObject;

            // Show the new object
            objectToShow.SetActive(true);

            // Destroy both
            Destroy(otherRoot);
            Destroy(gameObject);
        }
    }
}