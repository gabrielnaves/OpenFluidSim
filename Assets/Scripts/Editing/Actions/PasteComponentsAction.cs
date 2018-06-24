using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action for pasting components onto the editor canvas
/// </summary>
public class PasteComponentsAction : IAction {

    List<BaseComponent> pastedComponents;

    public PasteComponentsAction(List<BaseComponent> pastedComponents) {
        this.pastedComponents = pastedComponents;
    }

    public void DoAction() {}

    public void UndoAction() {
        foreach (var component in pastedComponents) {
            component.gameObject.SetActive(false);
            SelectedObjects.instance.DeselectObject(component);
        }
    }

    public void RedoAction() {
        foreach (var component in pastedComponents)
            component.gameObject.SetActive(true);
    }

    public void OnDestroy() {
        foreach (var component in pastedComponents) {
            SelectedObjects.instance.DeselectObject(component);
            Object.Destroy(component.gameObject);
        }
    }

    public string Name() {
        return "Paste components";
    }
}
