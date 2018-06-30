using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateComponentButton : TaskbarButton {

    public void RotateComponents(bool clockwise) {
        List<BaseComponent> componentsToRotate = SelectedObjects.instance.GetSelectedComponents();
        if (componentsToRotate.Count > 0)
            ActionStack.instance.PushAction(new RotateComponentAction(
                componentsToRotate,
                clockwise
            ));
    }

    protected override bool ShouldShowButton() {
        if (!SelectedObjects.instance.HasObject())
            return false;
        return SelectedObjects.instance.GetSelectedComponents().Count > 0;
    }
}
