using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("Hit by player");
        }

        if (collision.CompareTag("Enemy")) {
            Debug.Log("Hit by enemy");
            Destroy(collision.gameObject);
        }
    }
}
