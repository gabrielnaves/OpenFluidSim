public class RedoButton : TaskbarButton {

    public void Redo() {
        ActionStack.instance.RedoAction();
    }

    protected override bool ShouldShowButton() {
        return ActionStack.instance.redoStackSize > 0;
    }
}
