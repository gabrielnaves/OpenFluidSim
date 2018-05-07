/// <summary>
/// Interface for every object that represents an action on the action stack
/// </summary>
/// Do and Redo are offered as separate functions to make it possible to have
/// different implementations for whatever reason.
/// OnDestroy refers to destroying the action object from the action stack,
/// and has nothing to do with MonoBehaviour's OnDestroy.
public interface IAction {

    /// <summary>
    /// Called when an action is pushed to the action stack
    /// </summary>
    void DoAction();
    void UndoAction();
    void RedoAction();

    /// <summary>
    /// Called just before the action object is destroyed
    /// </summary>
    void OnDestroy();

    string Name();
}
