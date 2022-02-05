using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float _speed = 3f;
    [SerializeField] Transform _spriteTransform;
    [SerializeField] GameObject _staff;
    [SerializeField] float _swingDuration = 1f;

    private Vector2 _movementInput;
    private WaitForSeconds _swingDelayYield;
    private Rigidbody2D _rigidbody;

    private void Awake() {
        _swingDelayYield = new WaitForSeconds(_swingDuration);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {

        // If we're NOT in the middle of a swing,
        if (!_staff.activeSelf) {

            // Get input
            _movementInput.x = Input.GetAxis("Horizontal");
            _movementInput.y = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space)) {
                StartCoroutine(Swing());
            }
        }
        else {
            // Halt movement
            _movementInput = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        _rigidbody.velocity = _movementInput.normalized * _speed;
        _rigidbody.angularVelocity = 0;

        // Facing
        if (_movementInput != Vector2.zero) {
            float angle = Mathf.Atan2(_movementInput.y, _movementInput.x) * Mathf.Rad2Deg;
            _spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private IEnumerator Swing() {
        _staff.SetActive(true);
        yield return _swingDelayYield;
        _staff.SetActive(false);
    }
}
