using UnityEngine;
using UnityEngine.UI;

public class RedoButton : MonoBehaviour {

    private bool active;

    void Update() {
        if (ActionStack.instance.redoStack.Count == 0)
            DeactivateButton();
        else
            ActivateButton();
        if (active)
            CheckForKeyboardShortcut();
    }

    private void DeactivateButton() {
        GetComponent<Image>().color = Color.gray;
        GetComponent<Button>().enabled = false;
        active = false;
    }

    private void ActivateButton() {
        GetComponent<Image>().color = Color.white;
        GetComponent<Button>().enabled = true;
        active = true;
    }
    private void CheckForKeyboardShortcut() {
#if DEVEL
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Y))
            GetComponent<Button>().onClick.Invoke();
#endif
#if LAUNCH
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y))
            GetComponent<Button>().onClick.Invoke();
#endif
    }
}
