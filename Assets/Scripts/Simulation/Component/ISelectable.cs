using UnityEngine;

public interface ISelectable {

    bool RequestedSelect();
    bool IsInsideSelectionBox(Collider2D selectionBox);
    void OnSelect();
    void OnDeselect();
}
