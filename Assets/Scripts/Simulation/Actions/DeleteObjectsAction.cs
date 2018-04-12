using UnityEngine;

/// <summary>
/// Action for deleting components from the simulator editor
/// </summary>
/// Deletion is implemented by disabling the object instead of destroying it.
/// Automatically handles connections to the target component.
public class DeleteObjectsAction : IAction {

    BaseComponent[] referencedComponents;
    
    public DeleteObjectsAction(BaseComponent[] referencedComponents) {
        this.referencedComponents = referencedComponents;
    }

    public void DoAction() {
        foreach (var component in referencedComponents) {
            SimulationPanel.instance.RemoveComponent(component.GetComponent<BaseComponent>());
            SelectedObjects.instance.DeselectObject(component);
            component.gameObject.SetActive(false);
        }
        //DeactivateConnections(referencedObject.GetComponent<ComponentConnections>());
    }

    public void UndoAction() {
        foreach (var component in referencedComponents) {
            SimulationPanel.instance.AddComponent(component.GetComponent<BaseComponent>());
            component.gameObject.SetActive(true);
        }
        //ReactivateConnections(referencedObject.GetComponent<ComponentConnections>());
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}

    //void DeactivateConnections(ComponentConnections componentConnections) {
    //    if (componentConnections != null)
    //        foreach (var connector in componentConnections.connectorList)
    //            RemoveExternalConnections(connector);
    //}

    //void RemoveExternalConnections(PneumaticConnector connector) {
    //    foreach (var other in connector.connectedObjects)
    //        other.RemoveConnection(connector);
    //}

    //void ReactivateConnections(ComponentConnections componentConnections) {
    //    if (componentConnections != null)
    //        foreach (var connector in componentConnections.connectorList)
    //            AddExternalConnections(connector);
    //}

    //void AddExternalConnections(PneumaticConnector connector) {
    //    foreach (var other in connector.connectedObjects)
    //        other.AddConnection(connector);
    //}

    public string Name() {
        return "Delete Component";
    }
}
