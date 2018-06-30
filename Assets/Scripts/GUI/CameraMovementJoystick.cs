using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementJoystick : MonoBehaviour {

    public float speedMultiplier;
    public Color colorPressed;

    MouseInputArea mouseInputArea;
    SpriteRenderer ball, up, down, left, right;

    void Awake() {
        mouseInputArea = GetComponent<MouseInputArea>();
        ball = transform.GetChild(0).GetComponent<SpriteRenderer>();
        up = transform.GetChild(1).GetComponent<SpriteRenderer>();
        down = transform.GetChild(2).GetComponent<SpriteRenderer>();
        left = transform.GetChild(3).GetComponent<SpriteRenderer>();
        right = transform.GetChild(4).GetComponent<SpriteRenderer>();
    }

	void FixedUpdate () {
        SetColor(ball, mouseInputArea.mouseButton);
		if (mouseInputArea.mouseButton) {
            Vector2 offset = mouseInputArea.mousePosition - (Vector2)transform.position;
            float verticalOffset = Mathf.Abs(ball.transform.position.y - transform.position.y);
            offset.y += verticalOffset;
            SetColor(up, offset.y > 0);
            SetColor(down, offset.y < 0);
            SetColor(left, offset.x < 0);
            SetColor(right, offset.x > 0);
            MoveCamera(offset);
        }
        else {
            SetColor(up, false);
            SetColor(down, false);
            SetColor(left, false);
            SetColor(right, false);
        }
	}

    void SetColor(SpriteRenderer renderer, bool pressed) {
        renderer.color = pressed ? colorPressed : Color.black;
    }

    void MoveCamera(Vector3 offset) {
        Camera.main.transform.position = Camera.main.transform.position + (offset * speedMultiplier);
    }
}
