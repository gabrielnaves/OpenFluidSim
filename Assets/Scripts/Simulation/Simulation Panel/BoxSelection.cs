using UnityEngine;

public class BoxSelection : MonoBehaviour {

    static public BoxSelection instance { get; private set; }

    public GameObject selectionBox;

    [ViewOnly]
    public bool isSelecting;

    Collider2D boxCollider;
    Vector3 startingPosition;

    public void StartSelecting() {
        isSelecting = true;
        selectionBox.SetActive(true);
        startingPosition = SimulationInput.instance.startingDragPoint;
    }

    public void StopSelecting() {
        isSelecting = false;
        selectionBox.SetActive(false);
    }

    void Awake() {
        instance = (BoxSelection)Singleton.Setup(this, instance);
        selectionBox.SetActive(false);
        boxCollider = selectionBox.GetComponent<Collider2D>();
    }

	void Update() {
        if (isSelecting) {
            ResizeSelectionBox();
            CheckForSelectableObjects();
        }
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

    void CheckForSelectableObjects() {
        SelectedObjects.instance.ClearSelection();
        foreach (var selectable in SimulationPanel.instance.GetActiveSelectables()) {
            if (selectable.IsInsideSelectionBox(boxCollider))
                SelectedObjects.instance.SelectComponent(selectable);
        }
    }
}
