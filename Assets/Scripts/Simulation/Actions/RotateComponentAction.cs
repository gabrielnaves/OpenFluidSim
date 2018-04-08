using UnityEngine;

/// <summary>
/// Action for rotating components (relative to previous rotation value)
/// </summary>
/// Rotation amount is given in degrees, and is usually
/// a multiple of 90.
public class RotateComponentAction : IAction {

    public GameObject referencedObject;

    /// <summary>
    /// Rotation amount in degrees
    /// </summary>
    public float rotationAmount;

    public void DoAction() {
        Quaternion rotation = referencedObject.transform.rotation;
        Vector3 eulerAngles = rotation.eulerAngles;
        eulerAngles.z += rotationAmount;
        rotation.eulerAngles = eulerAngles;
        referencedObject.transform.rotation = rotation;
    }

    public void UndoAction() {
        Quaternion rotation = referencedObject.transform.rotation;
        Vector3 eulerAngles = rotation.eulerAngles;
        eulerAngles.z -= rotationAmount;
        rotation.eulerAngles = eulerAngles;
        referencedObject.transform.rotation = rotation;
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}
}
