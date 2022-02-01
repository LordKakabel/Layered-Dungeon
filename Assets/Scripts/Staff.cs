using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] float _knockbackForce = 100f;
    [SerializeField] float _knockbackTime = 0.25f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            //Vector3 direction = (collision.transform.position - transform.position).normalized;

            //collision.GetComponent<Rigidbody2D>().AddForce(direction * _knockbackForce, ForceMode2D.Impulse);
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            StartCoroutine(enemy.Knockback(_knockbackTime, _knockbackForce, transform));
        }
    }
}
