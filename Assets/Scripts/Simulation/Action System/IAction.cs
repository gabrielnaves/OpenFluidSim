/// <summary>
/// Interface for every object that represents an action on the action stack
/// </summary>
public interface IAction {
    void UndoAction();
    void RedoAction();
}
