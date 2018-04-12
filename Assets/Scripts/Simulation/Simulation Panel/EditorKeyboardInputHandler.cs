using UnityEngine;

public class EditorKeyboardInputHandler : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SelectedObjects.instance.ClearSelection();
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
            RotateCommand(clockwise:false);
        else if (Input.GetKeyDown(KeyCode.R))
            RotateCommand(clockwise:true);
        if (Input.GetKeyDown(KeyCode.Delete))
            DeleteCommand();
    }

    void RotateCommand(bool clockwise) {
        BaseComponent[] componentsToRotate = SelectedObjects.instance.GetSelectedComponents();
        if (componentsToRotate.Length > 0)
            ActionStack.instance.PushAction(new RotateComponentAction(
                componentsToRotate,
                clockwise
            ));
    }

    void DeleteCommand() {
        BaseComponent[] componentsToDelete = SelectedObjects.instance.GetSelectedComponents();
        if (componentsToDelete.Length > 0)
            ActionStack.instance.PushAction(new DeleteObjectsAction(componentsToDelete));
    }
}
