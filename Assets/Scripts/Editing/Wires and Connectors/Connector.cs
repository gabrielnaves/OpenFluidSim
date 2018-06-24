using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements connections between components
/// </summary>
/// This script creates and stores local pneumatic connection information,
/// that is, other components that are connected to this component. 
/// This script also activates the WireCreator when a connection is requested.
public class Connector : MonoBehaviour, IDraggable {

    public enum ConnectorType { pneumatic, electric }
    public ConnectorType type;

    public Sprite openSprite;
    public Sprite connectedSprite;

    [ViewOnly] public List<Connector> connectedObjects = new List<Connector>();
    [ViewOnly] public float signal;

    Color openColor = Color.red;
    Color connectedColor = Color.black;

    Collider2D connectorCollider;
    SpriteRenderer spriteRenderer;

    bool IDraggable.RequestedDrag() {
        return EditorInput.instance.mouseDragStart &&
            connectorCollider.OverlapPoint(EditorInput.instance.startingDragPoint);
    }

    void IDraggable.StartDragging() {
        WireCreator.instance.StartGeneration(this);
    }

    void IDraggable.StopDragging() {
        if (type == ConnectorType.pneumatic)
            RespondToStopDrag(SimulationPanel.instance.GetActivePneumaticConnectors());
        else
            RespondToStopDrag(SimulationPanel.instance.GetActiveElectricConnectors());
        WireCreator.instance.StopGeneration();
    }

    public void AddConnection(Connector other) {
        if (!connectedObjects.Contains(other))
            connectedObjects.Add(other);
    }

    public void RemoveConnection(Connector other) {
        if (connectedObjects.Contains(other))
            connectedObjects.Remove(other);
    }

    void Start() {
        connectorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = openColor;
        GetComponentInParent<ComponentReferences>().AddPneumaticConnector(this);
    }

    void OnEnable() {
        if (type == ConnectorType.pneumatic)
            SimulationPanel.instance.AddPneumaticConnector(this);
        else
            SimulationPanel.instance.AddElectricConnector(this);
        SimulationPanel.instance.AddDraggable(this);
    }

    void OnDisable() {
        if (type == ConnectorType.pneumatic)
            SimulationPanel.instance.RemovePneumaticConnector(this);
        else
            SimulationPanel.instance.RemoveElectricConnector(this);
        SimulationPanel.instance.RemoveDraggable(this);
    }

    void RespondToStopDrag(Connector[] activeConnectors) {
        foreach (var connector in activeConnectors)
            if (connector != this)
                if (connector.GetComponent<Collider2D>().OverlapPoint(EditorInput.instance.mousePosition))
                    CreateConnection(connector);
    }

    void CreateConnection(Connector otherConnector) {
        ActionStack.instance.PushAction(new NewComponentConnectionAction(
            WireCreator.instance.RetrieveWire(otherConnector)
        ));
    }

    void Update() {
        UpdateSprite();
    }

    void UpdateSprite() {
        if (SimulationMode.instance.mode == SimulationMode.Mode.editor) {
            if (connectedObjects.Count > 0 && spriteRenderer.color != connectedColor) {
                spriteRenderer.sprite = connectedSprite;
                spriteRenderer.color = connectedColor;
            }
            else if (connectedObjects.Count == 0 && spriteRenderer.color != openColor) {
                spriteRenderer.sprite = openSprite;
                spriteRenderer.color = openColor;
            }
        }
        else {
            spriteRenderer.color = Color.clear;
        }
    }
}
