using UnityEngine;

public class RotateComponentAction : IAction {

    public GameObject referencedObject;
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
