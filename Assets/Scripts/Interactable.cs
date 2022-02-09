using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [field: SerializeField] public int Layer { get; private set; } = 1;
    [SerializeField] private Transform _teleportPoint;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerMovement>().NewLayer(Layer);
            collision.transform.position = _teleportPoint.position;
        }

        if (collision.CompareTag("Enemy")) {
            Debug.Log("Hit by enemy");
            collision.GetComponent<EnemyMovement>().NewLayer(Layer);
            collision.transform.position = _teleportPoint.position;
        }
    }
}
