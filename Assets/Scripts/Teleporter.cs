using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teleporter : MonoBehaviour
{
    [Header("Teleporter Settings")] 
    public Transform player, Destination;
    public GameObject playerg;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerg.SetActive(false);
            player.position = Destination.position;
            playerg.SetActive(true);
        }
    }
}
