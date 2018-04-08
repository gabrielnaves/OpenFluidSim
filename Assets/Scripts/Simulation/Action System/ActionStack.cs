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

    public Stack<IAction> actionStack { get; private set; }
    public Stack<IAction> redoStack { get; private set; }

    public void PushAction(IAction action) {
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

    void Awake() {
        instance = (ActionStack)Singleton.Setup(this, instance);
        actionStack = new Stack<IAction>();
        redoStack = new Stack<IAction>();
    }

    private void ClearRedoStack() {
        redoStack.Clear();
    }
}
