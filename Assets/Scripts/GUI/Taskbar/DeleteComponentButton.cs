using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteComponentButton : TaskbarButton {

    public void DeleteComponents() {
        List<BaseComponent> componentsToDelete = SelectedObjects.instance.GetSelectedComponents();
        List<Wire> wiresToDelete = SelectedObjects.instance.GetSelectedWires();
        if (componentsToDelete.Count > 0 || wiresToDelete.Count > 0)
            ActionStack.instance.PushAction(new DeleteObjectsAction(componentsToDelete, wiresToDelete));
    }

    protected override bool ShouldShowButton() {
        return SelectedObjects.instance.HasObject();
    }
}
