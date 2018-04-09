using UnityEngine;

public class MultiSelection : MonoBehaviour {

    public GameObject selectionBox;

    static public MultiSelection instance { get; private set; }

    public bool isSelecting { get; private set; }

    Collider2D boxCollider;
    Vector3 startingPosition;

    public bool TouchingSelectionBox(Collider2D other) {
        return other.IsTouching(boxCollider);
    }

    void Awake() {
        instance = (MultiSelection)Singleton.Setup(this, instance);
        boxCollider = selectionBox.GetComponent<Collider2D>();
    }

    void Start() {
        selectionBox.SetActive(false);
    }

	void LateUpdate() {
        CheckIfSelecting();
        if (isSelecting)
            ResizeSelectionBox();
	}

    void CheckIfSelecting() {
        if (!isSelecting && ClickedOutsideAComponent()) {
            isSelecting = true;
            selectionBox.SetActive(true);
            startingPosition = SimulationInput.instance.mousePosition;
        }
        if (isSelecting && (Input.GetMouseButtonUp(0)) || SimulationInput.instance.GetEscapeKeyDown()) {
            isSelecting = false;
            selectionBox.SetActive(false);
        }
    }

    bool ClickedOutsideAComponent() {
        if (SimulationInput.instance.mouseButtonDown) {
            foreach (var col in SimulationPane.instance.componentsContainer.GetComponentsInChildren<Collider2D>())
                if (col.OverlapPoint(SimulationInput.instance.mousePosition))
                    return false;
            foreach (var col in SimulationPane.instance.wiresContainer.GetComponentsInChildren<Collider2D>())
                if (col.OverlapPoint(SimulationInput.instance.mousePosition))
                    return false;
            return true;
        }
        return false;
    }

    void ResizeSelectionBox() {
        var mousePosition = SimulationInput.instance.mousePosition;
        selectionBox.transform.position = Vector3.Lerp(startingPosition, mousePosition, 0.5f);
        selectionBox.transform.localScale = new Vector3(
            Mathf.Abs(mousePosition.x - startingPosition.x),
            Mathf.Abs(mousePosition.y - startingPosition.y),
            1
        );
    }
}
