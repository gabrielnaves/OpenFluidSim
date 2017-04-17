using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDotGenerator : MonoBehaviour {

    public ObjectPooler dotPooler;
    public float dotDistance = 0.1f;

    private Stack<GameObject> dotStack = new Stack<GameObject>();

	void Start() {
        GenerateBackgroundDots();
	}

    private void GenerateBackgroundDots() {
        Vector2 bottomLeftPos = new Vector2(-20, -10);
        Vector2 topRightPos = new Vector2(20, 10);
        for (float i = bottomLeftPos.y; i < topRightPos.y; i += dotDistance) {
            for (float j = bottomLeftPos.x ; j < topRightPos.x; j += dotDistance) {
                GameObject newDot = dotPooler.GetObject();
                newDot.SetActive(true);
                newDot.transform.position = new Vector2(j, i);
                newDot.transform.parent = transform;
                dotStack.Push(newDot);
            }
        }
    }
}
