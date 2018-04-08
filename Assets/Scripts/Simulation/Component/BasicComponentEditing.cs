using UnityEngine;

public class BasicComponentEditing : MonoBehaviour {

    ComponentSelect componentSelect;
    ComponentMove componentMove;
    ComponentRotate componentRotate;
    ComponentDelete componentDelete;

    void Awake() {
        componentSelect = new ComponentSelect(gameObject, GetComponent<BoxCollider2D>(), GetComponent<SpriteRenderer>());
        componentMove = new ComponentMove(gameObject, GetComponent<BoxCollider2D>());
        componentRotate = new ComponentRotate(gameObject);
        componentDelete = new ComponentDelete(gameObject);
    }

    void Update() {
        componentSelect.Update();
        componentMove.Update();
        componentRotate.Update();
        componentDelete.Update();
    }
}
