using System.Collections.Generic;
using UnityEngine;

public class ActionStack : MonoBehaviour {

    private Stack<IAction> actionStack = new Stack<IAction>();
    private Stack<IAction> redoStack = new Stack<IAction>();

    public void PushAction(IAction action) {
        actionStack.Push(action);
        redoStack.Clear();
    }

    public void UndoAction() {
        actionStack.Peek().UndoAction();
        redoStack.Push(actionStack.Pop());
    }

    public void RedoAction() {
        redoStack.Peek().RedoAction();
        actionStack.Push(redoStack.Pop());
    }
}
