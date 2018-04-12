using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements the action stack, for unlimited undo/redo operations
/// </summary>
/// Every single user action that can be undone on the simulation needs
/// to be represented as an object implementing the IAction interface.
/// No size constraints are enforced for the stacks, so the user is
/// able to undo and redo every single action taken.
/// The "redo stack" is cleared whenever a new action is pushed to the
/// action stack.
public class ActionStack : MonoBehaviour {

    static public ActionStack instance { get; private set; }

    public Stack<IAction> actionStack;
    public Stack<IAction> redoStack;

    public void PushAction(IAction action) {
        action.DoAction();
        actionStack.Push(action);
        ClearRedoStack();
    }

    public void UndoAction() {
        actionStack.Peek().UndoAction();
        redoStack.Push(actionStack.Pop());
    }

    public void RedoAction() {
        redoStack.Peek().RedoAction();
        actionStack.Push(redoStack.Pop());
    }

    public int ActionStackSize() {
        if (actionStack == null)
            return 0;
        return actionStack.Count;
    }

    public int RedoStackSize() {
        if (actionStack == null)
            return 0;
        return redoStack.Count;
    }

    void Awake() {
        instance = (ActionStack)Singleton.Setup(this, instance);
        actionStack = new Stack<IAction>();
        redoStack = new Stack<IAction>();
    }

    void ClearRedoStack() {
        foreach (var act in redoStack)
            act.OnDestroy();
        redoStack.Clear();
    }

    public IAction[] GetActionStack() {
        if (actionStack != null)
            return actionStack.ToArray();
        return new IAction[0];
    }

    public IAction[] GetRedoStack() {
        if (redoStack != null)
            return redoStack.ToArray();
        return new IAction[0];
    }
}
