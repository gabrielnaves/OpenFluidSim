using System.Collections.Generic;
using UnityEngine;

public class ActionStack : MonoBehaviour {

    static public ActionStack instance { get; private set; }

    public Stack<IAction> actionStack { get; private set; }
    public Stack<IAction> redoStack { get; private set; }

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

    void Awake() {
        instance = (ActionStack)Singleton.Setup(this, instance);
        actionStack = new Stack<IAction>();
        redoStack = new Stack<IAction>();
    }

    private void ClearRedoStack() {
        foreach (var act in redoStack)
            act.OnDestroy();
        redoStack.Clear();
    }
}
