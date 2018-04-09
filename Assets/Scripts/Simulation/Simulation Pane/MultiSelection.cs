using UnityEngine;

public class MultiSelection : MonoBehaviour {

    public GameObject selectionBox;

    static public MultiSelection instance { get; private set; }

    public bool selecting { get; private set; }

    Collider2D boxCollider;
    SpriteRenderer boxSpriteRenderer;
    Vector3 startingPosition;

    void Awake() {
        instance = (MultiSelection)Singleton.Setup(this, instance);
        boxCollider = selectionBox.GetComponent<Collider2D>();
        boxSpriteRenderer = selectionBox.GetComponent<SpriteRenderer>();
    }

    void Start() {
        selectionBox.SetActive(false);
    }

	void LateUpdate() {
        CheckIfSelecting();
        if (selecting) {
            ResizeSelectionBox();
            SelectComponentsInsideSelectionBox();
        }
	}

    void CheckIfSelecting() {
        if (!selecting && ClickedOutsideAComponent()) {
            selecting = true;
            selectionBox.SetActive(true);
            startingPosition = SimulationInput.instance.mousePosition;
        }
        if (selecting && (Input.GetMouseButtonUp(0)) || SimulationInput.instance.GetEscapeKeyDown()) {
            selecting = false;
            selectionBox.SetActive(false);
        }
    }

    bool ClickedOutsideAComponent() {
        if (SimulationInput.instance.mouseButtonDown) {
            var activeComponents = SimulationPane.instance.GetActiveComponents();
            foreach (var obj in activeComponents) {
                var collider = obj.GetComponent<Collider2D>();
                if (collider.OverlapPoint(SimulationInput.instance.mousePosition))
                    return false;
            }
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

    void SelectComponentsInsideSelectionBox() {
        var activeComponents = SimulationPane.instance.GetActiveComponents();
        foreach(var obj in activeComponents) {
            var col = obj.GetComponent<Collider2D>();
            if (col.IsTouching(boxCollider))
                obj.GetComponent<BasicComponentEditing>().isSelected = true;
            else
                obj.GetComponent<BasicComponentEditing>().isSelected = false;
        }
    }
}
