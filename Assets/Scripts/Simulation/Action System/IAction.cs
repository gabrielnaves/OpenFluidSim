public interface IAction {
    void DoAction();
    void UndoAction();
    void RedoAction();
    void OnDestroy();
}
