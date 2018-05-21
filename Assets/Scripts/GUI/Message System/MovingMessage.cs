using UnityEngine;
using UnityEngine.UI;

public class MovingMessage : MonoBehaviour {

    public float speed = 0.2f;
    public float lifetime = 1f;

    RectTransform rectTransform;
    Text text;
    Color startingColor;
    float elapsedTime = 0f;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

    void Start() {
        startingColor = text.color;
    }

    void FixedUpdate() {
        elapsedTime += Time.fixedDeltaTime;
        UpdatePosition();
        UpdateColor();
        if (elapsedTime > lifetime)
            Destroy(gameObject);
    }

    void UpdatePosition() {
        Vector3 position = rectTransform.position;
        position.y += speed * Time.fixedDeltaTime;
        rectTransform.position = position;
    }

    void UpdateColor() {
        startingColor.a = Mathf.Lerp(1f, 0f, Smoothing.SmoothStart(elapsedTime / lifetime, 4));
        text.color = startingColor;
    }
}
