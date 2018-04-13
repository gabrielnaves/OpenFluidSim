using UnityEngine;

public class LinkCanvasToMainCamera : MonoBehaviour {

    void Start() {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
