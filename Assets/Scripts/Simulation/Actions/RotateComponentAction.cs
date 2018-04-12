using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action for rotating components (relative to previous rotation value)
/// </summary>
/// Rotation amount is given in degrees, and is usually
/// a multiple of 90.
public class RotateComponentAction : IAction {

    List<BaseComponent> referencedComponents;
    bool clockwise;
    float rotationAmount;

    public RotateComponentAction(List<BaseComponent> referencedComponents, bool clockwise) {
        this.referencedComponents = referencedComponents;
        this.clockwise = clockwise;
        rotationAmount = clockwise ? -90f : 90;
    }

    public void DoAction() {
        foreach (var component in referencedComponents) {
            Quaternion rotation = component.transform.rotation;
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles.z += rotationAmount;
            rotation.eulerAngles = eulerAngles;
            component.transform.rotation = rotation;
        }
    }

    public void UndoAction() {
        foreach (var component in referencedComponents) {
            Quaternion rotation = component.transform.rotation;
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles.z -= rotationAmount;
            rotation.eulerAngles = eulerAngles;
            component.transform.rotation = rotation;
        }
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}

    public string Name() {
        if (referencedComponents.Count == 1)
            return "Rotate " + (clockwise ? "clockwise " : "counter-clockwise ") + referencedComponents[0];
        return "Rotate multiple " + (clockwise ? "clockwise" : "counter-clockwise");
    }
}
