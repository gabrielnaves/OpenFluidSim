public class UndoButton : TaskbarButton {

    public void Undo() {
        ActionStack.instance.UndoAction();
    }

    protected override bool ShouldShowButton() {
        return ActionStack.instance.actionStackSize > 0;
    }
}
