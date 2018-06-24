using System.Collections.Generic;
using UnityEngine;

public class EditorKeyboardInputHandler : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SelectedObjects.instance.ClearSelection();
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
            RotateCommand(clockwise: false);
        else if (Input.GetKeyDown(KeyCode.R))
            RotateCommand(clockwise: true);
        else if (Input.GetKeyDown(KeyCode.Delete))
            DeleteCommand();
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S)) {
            if (SimulationPanel.instance.activeComponents.Count > 0)
                SaveUtility.instance.SaveSimulationToFile();
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z))
            ActionStack.instance.UndoAction();
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Y))
            ActionStack.instance.RedoAction();
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
            CopyCommand();
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.V))
            PasteCommand();
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

    void CopyCommand() {
        Clipboard.SetClipboard(SaveUtility.instance.GetSelectedComponentsSaveString());
    }

    void PasteCommand() {
        if (!Clipboard.IsEmpty())
            LoadUtility.instance.AddFromClipboard();
    }
}
