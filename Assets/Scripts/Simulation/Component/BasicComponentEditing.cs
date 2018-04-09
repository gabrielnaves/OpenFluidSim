using UnityEngine;

/// <summary>
/// Provides selection, movement, rotation and deletion for electric/pneumatic components
/// </summary>
/// This script reunites the implementation for each editing action on
/// a single MonoBehaviour. This method results in a cleaner game object
/// for electric and pneumatic components.
public class BasicComponentEditing : MonoBehaviour {

    public bool isSelected;

    ComponentMove componentMove;
    ComponentRotate componentRotate;
    ComponentDelete componentDelete;

    void Awake() {
        componentMove = new ComponentMove(gameObject, GetComponent<BoxCollider2D>());
        componentRotate = new ComponentRotate(gameObject);
        componentDelete = new ComponentDelete(gameObject);
    }

    void OnDisable() {
        isSelected = false;
    }

    void Update() {
        if (isSelected) {
            GetComponent<SpriteRenderer>().color = new Color32(191, 186, 255, 255);
            componentMove.Update();
            componentRotate.Update();
            componentDelete.Update();
        }
        else
            GetComponent<SpriteRenderer>().color = Color.white;
    }
}
