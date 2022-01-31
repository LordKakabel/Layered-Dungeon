using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] Transform _spriteTransform;

    private Vector2 _movementInput;
    private PlayerMovement _player;

    private void Start() {
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update() {
        _movementInput = _player.transform.position - transform.position;
    }

    private void FixedUpdate() {
        transform.Translate(_speed * Time.deltaTime * _movementInput.normalized);
        
        // Facing
        if (_movementInput != Vector2.zero) {
            float angle = Mathf.Atan2(_movementInput.y, _movementInput.x) * Mathf.Rad2Deg;
            _spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
