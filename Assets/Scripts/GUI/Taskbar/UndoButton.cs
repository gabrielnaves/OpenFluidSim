using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the taskbar's undo button and checks for the undo keyboard shortcut
/// </summary>
/// The undo button will be inactive when undo operations cannot be executed.
public class UndoButton : MonoBehaviour {

    bool active;

    /// <summary>
    /// Also used by the undo Button component
    /// </summary>
    public void Undo() {
        ActionStack.instance.UndoAction();
    }

    void Update() {
        if (ActionStack.instance.ActionStackSize() == 0 || SimulationMode.instance.mode == SimulationMode.Mode.simulation)
            DeactivateButton();
        else
            ActivateButton();
        if (active)
            CheckForKeyboardShortcut();
    }

    void DeactivateButton() {
        GetComponent<Image>().color = Color.gray;
        GetComponent<Button>().enabled = false;
        active = false;
    }

    void ActivateButton() {
        GetComponent<Image>().color = Color.white;
        GetComponent<Button>().enabled = true;
        active = true;
    }

    void CheckForKeyboardShortcut() {
#if DEVEL
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z))
            Undo();
#endif
#if LAUNCH
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
            Undo();
#endif
    }
}
