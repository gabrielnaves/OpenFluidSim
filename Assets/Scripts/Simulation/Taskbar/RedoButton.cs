using UnityEngine;
using UnityEngine.UI;

public class RedoButton : MonoBehaviour {

    void Update() {
        if (ActionStack.instance.redoStack.Count == 0)
            DeactivateButton();
        else
            ActivateButton();
    }

    private void DeactivateButton() {
        GetComponent<Image>().color = Color.gray;
        GetComponent<Button>().enabled = false;
    }

    private void ActivateButton() {
        GetComponent<Image>().color = Color.white;
        GetComponent<Button>().enabled = true;
    }
}
