using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { if (_instance == null) { Debug.LogError("GameManager instance is NULL"); } return _instance; } }
    #endregion

    public Action<int> OnLayerChange;

    [SerializeField] List<Transform> _layers = new List<Transform>();

    private void Awake() {
        _instance = this;
    }

    public void NewLayerEntrered(int layer) {
        OnLayerChange?.Invoke(layer);
        MoveCamera.Instance.LayerChange(_layers[layer].position);
    }
}
