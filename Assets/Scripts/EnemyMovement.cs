using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] Transform _spriteTransform;

    private Vector2 _input;
    private PlayerMovement _player;

    private void Start() {
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update() {
        _input = _player.transform.position - transform.position;
    }

    private void FixedUpdate() {
        transform.Translate(_speed * Time.deltaTime * _input.normalized);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_input), Time.deltaTime * _rotationSpeed);
        if (_input != Vector2.zero) {
            float angle = Mathf.Atan2(_input.y, _input.x) * Mathf.Rad2Deg;
            _spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
