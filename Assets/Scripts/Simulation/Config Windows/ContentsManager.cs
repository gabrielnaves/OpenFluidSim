using UnityEngine;
using UnityEngine.UI;

public class ContentsManager : MonoBehaviour {

    RectTransform rectTransform;

    public void AddToContents(GameObject target) {
        target.GetComponent<RectTransform>().SetParent(rectTransform);
        target.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        ResizeContentsWindow();
    }

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void ResizeContentsWindow() {
        float height = 0;
        foreach (RectTransform component in rectTransform)
            height += component.rect.height;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }
}
