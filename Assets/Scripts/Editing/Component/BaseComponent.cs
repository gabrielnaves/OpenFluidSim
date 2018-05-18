using UnityEngine;

/// <summary>
/// Provides selection, movement, rotation and deletion for electric/pneumatic components
/// </summary>
/// This script reunites the implementation for each editing action on
/// a single MonoBehaviour. This method results in a cleaner game object
/// for electric and pneumatic components.
public class BaseComponent : MonoBehaviour, ISelectable, IDraggable {

    new Collider2D collider;
    ComponentMove componentMove;
    SpriteRenderer[] spritesInHierarchy;
    Color[] originalColors;

    bool ISelectable.RequestedSelect() {
        return EditorInput.instance.singleClick && collider.OverlapPoint(EditorInput.instance.mousePosition);
    }

    bool ISelectable.IsInsideSelectionBox(Collider2D selectionBox) {
        return selectionBox.IsTouching(collider);
    }

    void ISelectable.OnSelect() {
        foreach (var sprite in spritesInHierarchy)
            sprite.color = new Color32(191, 186, 255, 255);
    }

    void ISelectable.OnDeselect() {
        for (int i = 0; i < spritesInHierarchy.Length; ++i)
            spritesInHierarchy[i].color = originalColors[i];
    }

    bool IDraggable.RequestedDrag() {
        return EditorInput.instance.mouseDragStart && collider.OverlapPoint(EditorInput.instance.startingDragPoint);
    }

    void IDraggable.StartDragging() {
        if (!SelectedObjects.instance.IsSelected(this)) {
            SelectedObjects.instance.ClearSelection();
            SelectedObjects.instance.SelectObject(this);
        }
        componentMove.StartMoving();
    }

    void IDraggable.StopDragging() {
        componentMove.StopMoving();
    }

    void Awake() {
        collider = GetComponent<Collider2D>();
        componentMove = new ComponentMove();
        spritesInHierarchy = GetComponentsInChildren<SpriteRenderer>();

        originalColors = new Color[spritesInHierarchy.Length];
        for (int i = 0; i < spritesInHierarchy.Length; ++i)
            originalColors[i] = spritesInHierarchy[i].color;
    }
    
    void Update() {
        componentMove.Update();
    }

    void OnEnable() {
        SimulationPanel.instance.AddComponent(this);
        SimulationPanel.instance.AddSelectable(this);
        SimulationPanel.instance.AddDraggable(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveComponent(this);
        SimulationPanel.instance.RemoveSelectable(this);
        SimulationPanel.instance.RemoveDraggable(this);
    }
}
