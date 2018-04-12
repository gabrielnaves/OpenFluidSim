using System.Collections;
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
        List<BaseComponent> componentsToRotate = new List<BaseComponent>();
        foreach (var component in SimulationPanel.instance.GetActiveComponents())
            if (SelectedObjects.instance.IsSelected(component))
                componentsToRotate.Add(component);
        if (componentsToRotate.Count > 0)
            ActionStack.instance.PushAction(new RotateComponentAction(
                componentsToRotate.ToArray(),
                clockwise
            ));
    }

    void DeleteCommand() {

    }
}
