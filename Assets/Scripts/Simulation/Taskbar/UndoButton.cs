using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour {

    private bool active;

	void Update() {
        if (ActionStack.instance.actionStack.Count == 0)
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
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z))
            GetComponent<Button>().onClick.Invoke();
    }
}
