using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] Transform _spriteTransform;
    [SerializeField] float _stunDuration = 1f;

    private Vector2 _movementInput;
    private PlayerMovement _player;
    private Rigidbody2D _rigidbody;
    private enum State { Normal, Knockedback, Stunned }
    private State _state = State.Normal;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update() {
        _movementInput = (_player.transform.position - transform.position).normalized;
    }

    private void FixedUpdate() {
        switch (_state) {
            case State.Normal:
                Movement();
                break;
            case State.Knockedback:
                break;
            case State.Stunned:
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.angularVelocity = 0;
                break;
            default:
                break;
        }
    }

    private void Movement() {
        _rigidbody.velocity = _movementInput * _speed;

        // Facing
        if (_movementInput != Vector2.zero) {
            float angle = Mathf.Atan2(_movementInput.y, _movementInput.x) * Mathf.Rad2Deg;
            _spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj) {
        _state = State.Knockedback;

        //float timer = Time.time + knockbackDuration;

        //Debug.Log(timer + " " + knockbackDuration);

        Vector2 direction = (obj.transform.position - transform.position).normalized;
        _rigidbody.AddForce(-direction * knockbackPower);

        /*while (Time.time < timer) {
            //timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            _rigidbody.AddForce(-direction * knockbackPower);

            Debug.Log(timer + " " + knockbackDuration);
        }*/

        //Debug.Log(timer + " " + knockbackDuration);

        //_rigidbody.isKinematic = true;

        yield return new WaitForSeconds(knockbackDuration);

        StartCoroutine(Stun());

        //_rigidbody.isKinematic = false;

        //Debug.Log("I'm done");
        //_rigidbody.velocity = Vector2.zero;
        //_rigidbody.angularVelocity = 0;
    }

    public IEnumerator Stun() {
        _state = State.Stunned;
        yield return new WaitForSeconds(_stunDuration);
        _state = State.Normal;
    }
}
