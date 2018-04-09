using UnityEngine;

/// <summary>
/// Provides selection, movement, rotation and deletion for electric/pneumatic components
/// </summary>
/// This script reunites the implementation for each editing action on
/// a single MonoBehaviour. This method results in a cleaner game object
/// for electric and pneumatic components.
public class BasicComponentEditing : MonoBehaviour {

    public bool isSelected;

    ComponentSelect componentSelect;
    ComponentMove componentMove;
    ComponentRotate componentRotate;
    ComponentDelete componentDelete;

    void Awake() {
        componentSelect = new ComponentSelect(gameObject, GetComponent<BoxCollider2D>());
        componentMove = new ComponentMove(gameObject, GetComponent<BoxCollider2D>());
        componentRotate = new ComponentRotate(gameObject);
        componentDelete = new ComponentDelete(gameObject);
    }

    void OnDisable() {
        isSelected = false;
    }

    void Update() {
        componentSelect.Update();
        if (componentSelect.isSelected) {
            GetComponent<SpriteRenderer>().color = new Color32(191, 186, 255, 255);
            componentMove.Update();
            componentRotate.Update();
            componentDelete.Update();
        }
        else
            GetComponent<SpriteRenderer>().color = Color.white;
    }
}
