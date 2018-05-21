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

    public int actionStackSize {
        get {
            if (actionStack != null) return actionStack.Count;
            return 0;
        }
    }

    public int redoStackSize {
        get {
            if (redoStack != null) return redoStack.Count;
            return 0;
        }
    }

    void Awake() {
        instance = (ActionStack)Singleton.Setup(this, instance);
        actionStack = new Stack<IAction>();
        redoStack = new Stack<IAction>();
    }

    public void PushAction(IAction action) {
        action.DoAction();
        actionStack.Push(action);
        ClearRedoStack();
    }

    public void UndoAction() {
        if (actionStackSize > 0) {
            MessageSystem.instance.GenerateMessage("Undo " + actionStack.Peek().Name());
            actionStack.Peek().UndoAction();
            redoStack.Push(actionStack.Pop());
        }
    }

    public void RedoAction() {
        if (redoStackSize > 0) {
            MessageSystem.instance.GenerateMessage("Redo " + redoStack.Peek().Name());
            redoStack.Peek().RedoAction();
            actionStack.Push(redoStack.Pop());
        }
    }

    /// <summary>
    /// Clears both the action stack and redo stack. Used when clearing the entire simulation.
    /// </summary>
    public void WipeStacks() {
        redoStack.Clear();
        actionStack.Clear();
    }

    void ClearRedoStack() {
        foreach (var act in redoStack)
            act.OnDestroy();
        redoStack.Clear();
    }
}
