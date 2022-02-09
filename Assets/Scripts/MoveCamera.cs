using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // singleton
    #region Singleton
    private static MoveCamera _instance;
    public static MoveCamera Instance { get { if (_instance == null) { Debug.LogError("MoveCamera instance is NULL"); } return _instance; } }
    #endregion

    [SerializeField] float _speed = 7f;

    private Vector3 _target;
    private float _zPos;

    private void Awake() {
        _instance = this;
        _zPos = transform.position.z;
        _target = new Vector3(0, 0, _zPos);
    }

    private void Start() {
        //GameManager.Instance.OnLayerChange += OnLayerChange;
    }

    private void LateUpdate() {
        

        if (Vector3.Distance(transform.position, _target) > Mathf.Epsilon) {
            //transform.Translate(_speed * Time.deltaTime * (transform.position - _target).normalized);
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }
    }

    public void LayerChange(Vector3 position) {
        _target = new Vector3(position.x, position.y, _zPos);
    }
}
