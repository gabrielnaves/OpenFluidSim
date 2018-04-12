using System.Collections.Generic;
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
        List<BaseComponent> componentsToRotate = SelectedObjects.instance.GetSelectedComponents();
        if (componentsToRotate.Count > 0)
            ActionStack.instance.PushAction(new RotateComponentAction(
                componentsToRotate,
                clockwise
            ));
    }

    void DeleteCommand() {
        List<BaseComponent> componentsToDelete = SelectedObjects.instance.GetSelectedComponents();
        List<Wire> wiresToDelete = SelectedObjects.instance.GetSelectedWires();
        if (componentsToDelete.Count > 0 || wiresToDelete.Count > 0)
            ActionStack.instance.PushAction(new DeleteObjectsAction(componentsToDelete, wiresToDelete));
    }
}
