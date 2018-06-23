using UnityEngine;
using UnityEngine.UI;

public class ComponentListBar : MonoBehaviour {

    public static ComponentListBar instance { get; private set; }

    public Button[] buttons;

    bool[] originalState;

    void Awake() {
        instance = (ComponentListBar)Singleton.Setup(this, instance);
    }

    void Start() {
        originalState = new bool[buttons.Length];
    }

    public void Disable() {
        for (int i = 0; i < 0; ++i) {
            originalState[i] = buttons[i].interactable;
            buttons[i].interactable = false;
        }
    }

    public void Enable() {
        for (int i = 0; i < 0; ++i)
            buttons[i].interactable = originalState[i];
    }
}
