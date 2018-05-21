using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the "contents" game object inside the component list hierarchy
/// </summary>
/// Provides a function for adding the "GUI-version" of electric/pneumatic
/// components to the component list.
/// Automatically resizes the "contents" game object to accomodate
/// any amount of components inside the component list.
/// The component list is implemented using Unity's ScrollView.
/// Every "GUI component" is a Unity Button with a ComponentRef. 
public class ComponentListContents : MonoBehaviour {

    [ViewOnly] public List<GameObject> guiComponents = new List<GameObject>();
    RectTransform rectTransform;

    public void AddGUIComponent(GameObject component) {
        guiComponents.Add(component);
        component.GetComponent<RectTransform>().SetParent(rectTransform);
        UpdateWidth();
    }

    public void DisableAllButtons() {
        foreach (var component in guiComponents)
            component.GetComponent<Button>().interactable = false;
    }

    public void EnableAllButtons() {
        foreach (var component in guiComponents)
            component.GetComponent<Button>().interactable = true;
    }

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start() {
        foreach (RectTransform component in rectTransform)
            guiComponents.Add(component.gameObject);
        UpdateWidth();
    }

    void UpdateWidth() {
        float width = 20;
        foreach (RectTransform component in rectTransform)
            width += 20 + component.rect.width;
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }
}
