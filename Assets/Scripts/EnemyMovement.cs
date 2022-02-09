using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [field: SerializeField] public int Layer { get; private set; } = 1;

    [SerializeField] float _speed = 3f;
    [SerializeField] Transform _spriteTransform;
    [SerializeField] float _stunDuration = 1f;

    private Vector2 _movementInput;
    private PlayerMovement _player;
    private Rigidbody2D _rigidbody;
    private enum State { Idle, Normal, Knockedback, Stunned }
    private State _state = State.Idle;
    private float _spriteOffset;

    private void Start() {
        _spriteOffset = _spriteTransform.eulerAngles.z;
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();

        GameManager.Instance.OnLayerChange += OnLayerChange;

        OnLayerChange(Layer);
    }

    private void Update() {
        _movementInput = (_player.transform.position - transform.position).normalized;
    }

    private void FixedUpdate() {
        _rigidbody.angularVelocity = 0;

        switch (_state) {
            case State.Idle:
                _rigidbody.velocity = Vector2.zero;
                break;
            case State.Normal:
                Movement();
                break;
            case State.Knockedback:
                break;
            case State.Stunned:
                _rigidbody.velocity = Vector2.zero;
                //_rigidbody.angularVelocity = 0;
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
            _spriteTransform.rotation = Quaternion.AngleAxis(angle + _spriteOffset, Vector3.forward);
        }
    }

    public void Knockback(float knockbackDuration, float knockbackPower, Transform obj) {
        StartCoroutine(KnockbackCoroutine(knockbackDuration, knockbackPower, obj));
    }

    private IEnumerator KnockbackCoroutine(float knockbackDuration, float knockbackPower, Transform obj) {
        _state = State.Knockedback;

        Vector2 direction = (obj.transform.position - transform.position).normalized;
        _rigidbody.AddForce(-direction * knockbackPower);

        yield return new WaitForSeconds(knockbackDuration);

        StartCoroutine(Stun());
    }

    private IEnumerator Stun() {
        _state = State.Stunned;
        yield return new WaitForSeconds(_stunDuration);
        _state = State.Normal;
    }

    private void OnLayerChange(int layer) {
        if (Layer == _player.Layer) {
            _state = State.Normal;
        }
        else {
            _state = State.Idle;
        }
    }

    private void OnDisable() {
        //GameManager.Instance.OnLayerChange -= OnLayerChange;
    }

    public void NewLayer(int layer) {
        Layer = layer;
        OnLayerChange(layer);
    }
}
