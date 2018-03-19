using System.Collections.Generic;
using UnityEngine;

public class PneumaticConnector : MonoBehaviour {

    public List<PneumaticConnector> connectedObjects = new List<PneumaticConnector>();
    public Color openColor = Color.red;
    public Color selectedColor = Color.green;
    public Color connectedColor = Color.clear;

    public void AddConnection(PneumaticConnector other) {
        if (!connectedObjects.Contains(other)) {
            connectedObjects.Add(other);
            UpdateColor();
        }
    }

    public void RemoveConnection(PneumaticConnector other) {
        if (connectedObjects.Contains(other)) {
            connectedObjects.Remove(other);
            UpdateColor();
        }
    }

    void Start() {
        GetComponent<SpriteRenderer>().color = openColor;
    }

    void Update() {
        if (SelectedComponent.instance.component == gameObject)
            GetComponent<SpriteRenderer>().color = selectedColor;
        else
            UpdateColor();
    }

    private void UpdateColor() {
        if (connectedObjects.Count > 0)
            GetComponent<SpriteRenderer>().color = connectedColor;
        else
            GetComponent<SpriteRenderer>().color = openColor;
    }
}
