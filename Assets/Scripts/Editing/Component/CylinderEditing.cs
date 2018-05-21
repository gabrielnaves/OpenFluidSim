using System.Collections.Generic;
using UnityEngine;

public class CylinderEditing : MonoBehaviour, IConfigurable {

    public Transform[] cylinderParts;
    public GameObject configWindowPrefab;

    [ViewOnly] public float startingPercentage = 0f;
    [ViewOnly] public float movementTime = 1f;
    [ViewOnly] public int cylinderLength = 1;

    Vector3[] originalPositions;

    new Collider2D collider;
    Dictionary<int, float> lengthToMaxDisplacement = new Dictionary<int, float>() {
        { 1, 0.6f + 0 * 0.12f }, { 2, 0.6f + 1 * 0.12f },
        { 3, 0.6f + 2 * 0.12f }, { 4, 0.6f + 3 * 0.12f },
        { 5, 0.6f + 4 * 0.12f }, { 6, 0.6f + 5 * 0.12f }
    };

    public float GetMaxDisplacement() {
        return lengthToMaxDisplacement[cylinderLength];
    }

    public void UpdateSprites() {
        for (int i = 0; i < cylinderParts.Length; ++i)
            cylinderParts[i].position = originalPositions[i] +
                transform.right * (startingPercentage * GetMaxDisplacement());
    }

    bool IConfigurable.RequestedConfig() {
        return EditorInput.instance.doubleClick &&
            collider.OverlapPoint(EditorInput.instance.mousePosition);
    }

    bool IConfigurable.IsConfigured() {
        return true;
    }

    void IConfigurable.OpenConfigWindow() {
        var cylinderConfigWindow = Instantiate(configWindowPrefab).GetComponent<CylinderConfigWindow>();
        cylinderConfigWindow.cylinderEditing = this;
    }

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    void Start() {
        originalPositions = new Vector3[cylinderParts.Length];
        for (int i = 0; i < cylinderParts.Length; ++i)
            originalPositions[i] = cylinderParts[i].position;
    }

    void OnEnable() {
        SimulationPanel.instance.AddConfigurable(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveConfigurable(this);
    }
}
