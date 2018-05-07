using UnityEngine;

/// <summary>
/// Action for moving one or more components on simulation pane
/// </summary>
public class MoveComponentAction : IAction {

    BaseComponent[] referencedComponents;
    Vector2[] previousPositions;
    Vector2[] newPositions;

    public MoveComponentAction(BaseComponent[] referencedComponents, Vector2[] previousPositions, Vector2[] newPositions) {
        this.referencedComponents = referencedComponents;
        this.previousPositions = previousPositions;
        this.newPositions = newPositions;
    }

    public void DoAction() {
        for(int i = 0; i < referencedComponents.Length; ++i)
            referencedComponents[i].transform.position = newPositions[i];
    }

    public void UndoAction() {
        for (int i = 0; i < referencedComponents.Length; ++i)
            referencedComponents[i].transform.position = previousPositions[i];
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}

    public string Name() {
        if (referencedComponents.Length == 1)
            return "Move component " + referencedComponents[0];
        return "Move components";
    }
}
