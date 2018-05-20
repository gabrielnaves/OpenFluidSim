public class NewCorrelationAction : IAction {

    CorrelatedObject correlatedObject;
    CorrelationTarget correlationTarget;
    CorrelationTarget previousCorrelationTarget;

    public NewCorrelationAction(CorrelatedObject correlatedObject, CorrelationTarget correlationTarget) {
        this.correlatedObject = correlatedObject;
        this.correlationTarget = correlationTarget;
        previousCorrelationTarget = correlatedObject.correlationTarget;
    }

    public void DoAction() {
        correlatedObject.correlationTarget = correlationTarget;
        correlationTarget.AddCorrelatedObject(correlatedObject);
        if (previousCorrelationTarget)
            previousCorrelationTarget.RemoveCorrelatedObject(correlatedObject);
    }

    public void UndoAction() {
        correlatedObject.correlationTarget = previousCorrelationTarget;
        correlationTarget.RemoveCorrelatedObject(correlatedObject);
        if (previousCorrelationTarget)
            previousCorrelationTarget.AddCorrelatedObject(correlatedObject);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}

    public string Name() {
        return "New correlation";
    }
}
