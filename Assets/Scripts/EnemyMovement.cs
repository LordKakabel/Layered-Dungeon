using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] Transform _spriteTransform;

    private Vector2 _movementInput;
    private PlayerMovement _player;
    private Rigidbody2D _rigidbody;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update() {
        _movementInput = (_player.transform.position - transform.position).normalized;
    }

    private void FixedUpdate() {
        _rigidbody.velocity = _movementInput * _speed;
        
        // Facing
        if (_movementInput != Vector2.zero) {
            float angle = Mathf.Atan2(_movementInput.y, _movementInput.x) * Mathf.Rad2Deg;
            _spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj) {
        float timer = 0;

        while (knockbackDuration > timer) {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            _rigidbody.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
}
