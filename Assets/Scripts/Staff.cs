using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] float _knockbackForce = 100f;
    [SerializeField] float _knockbackTime = 0.25f;
    [SerializeField] Transform _parentTransform;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            enemy.Knockback(_knockbackTime, _knockbackForce, _parentTransform);
        }
    }
}
