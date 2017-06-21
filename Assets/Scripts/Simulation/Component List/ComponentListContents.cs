using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentListContents : MonoBehaviour {

    private List<GameObject> guiComponents = new List<GameObject>();
    private RectTransform rectTransform;

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
