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

    Stack<IAction> actionStack;
    Stack<IAction> redoStack;

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
        return actionStack.Count;
    }

    public int RedoStackSize() {
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
}
