public interface IAction {
    void UndoAction();
    void RedoAction();
    void OnDestroy();
}
