using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the taskbar's redo button and checks for the redo keyboard shortcut
/// </summary>
/// The undo button will be inactive when undo operations cannot be executed.
public class RedoButton : MonoBehaviour {

    bool active;

    /// <summary>
    /// Also used by the redo Button component
    /// </summary>
    public void Redo() {
        ActionStack.instance.RedoAction();
    }

    void Update() {
        if (ActionStack.instance.RedoStackSize() == 0 || SimulationMode.instance.mode == SimulationMode.Mode.simulation)
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
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Y))
            Redo();
#endif
#if LAUNCH
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y))
            Redo();
#endif
    }
}
