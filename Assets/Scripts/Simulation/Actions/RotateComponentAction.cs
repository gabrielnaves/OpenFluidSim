using UnityEngine;

/// <summary>
/// Action for rotating components (relative to previous rotation value)
/// </summary>
/// Rotation amount is given in degrees, and is usually
/// a multiple of 90.
public class RotateComponentAction : IAction {

    GameObject referencedObject;

    /// <summary>
    /// Rotation amount in degrees
    /// </summary>
    float rotationAmount;

    public RotateComponentAction(GameObject referencedObject, float rotationAmount) {
        this.referencedObject = referencedObject;
        this.rotationAmount = rotationAmount;
        RedoAction();
    }

    public void UndoAction() {
        Quaternion rotation = referencedObject.transform.rotation;
        Vector3 eulerAngles = rotation.eulerAngles;
        eulerAngles.z -= rotationAmount;
        rotation.eulerAngles = eulerAngles;
        referencedObject.transform.rotation = rotation;
    }

    public void RedoAction() {
        Quaternion rotation = referencedObject.transform.rotation;
        Vector3 eulerAngles = rotation.eulerAngles;
        eulerAngles.z += rotationAmount;
        rotation.eulerAngles = eulerAngles;
        referencedObject.transform.rotation = rotation;
    }
}
