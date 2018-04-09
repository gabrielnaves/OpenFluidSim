using System.Collections.Generic;
using UnityEngine;

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

    List<GameObject> guiComponents = new List<GameObject>();
    RectTransform rectTransform;

    public void AddGUIComponent(GameObject component) {
        guiComponents.Add(component);
        component.GetComponent<RectTransform>().SetParent(rectTransform);
        UpdateWidth();
    }

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start() {
        UpdateWidth();
    }

    void UpdateWidth() {
        float width = 20;
        foreach (RectTransform component in rectTransform)
            width += 20 + component.rect.width;
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }
}
