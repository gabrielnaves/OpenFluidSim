using UnityEngine;
using UnityEngine.UI;

public class ComponentListBar : MonoBehaviour {

    public static ComponentListBar instance { get; private set; }

    ComponentListContents componentListContents;

    void Awake() {
        instance = (ComponentListBar)Singleton.Setup(this, instance);
        componentListContents = GetComponentInChildren<ComponentListContents>();
    }

    public void Disable() {
        componentListContents.DisableAllButtons();
    }

    public void Enable() {
        componentListContents.EnableAllButtons();
    }
}
