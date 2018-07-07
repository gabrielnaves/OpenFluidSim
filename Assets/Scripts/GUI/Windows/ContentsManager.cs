using UnityEngine;
using UnityEngine.UI;

public class ContentsManager : MonoBehaviour {

    RectTransform rectTransform;

    public void AddToContents(GameObject target) {
        target.GetComponent<RectTransform>().SetParent(rectTransform);
        target.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
}
