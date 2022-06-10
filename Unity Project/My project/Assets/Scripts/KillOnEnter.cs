using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    [SerializeField] Transform player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = respawnPoint.transform.position;
    }
}
