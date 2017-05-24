using System.Collections.Generic;
using UnityEngine;

public class ActionStack : MonoBehaviour {

    static public ActionStack instance { get; private set; }

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

    void Awake() {
        instance = (ActionStack)Singleton.Setup(this, instance);
    }
}
