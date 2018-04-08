using UnityEngine;

/// <summary>
/// Implements component rotation 90 degrees (relative to previous orientation)
/// </summary>
public class ComponentRotate {

    int right = -1;
    int left = 1;

    GameObject gameObject;

    public ComponentRotate(GameObject gameObject) {
        this.gameObject = gameObject;
    }

	public void Update () {
        if (RequestedRotate(right))
            NewRotationAction(right);
        if (RequestedRotate(left))
            NewRotationAction(left);
	}

    bool RequestedRotate(int direction) {
        if (SelectedComponent.instance.component == gameObject) {
#if DEVEL
            return Input.GetKeyDown(KeyCode.R) &&
            (direction == right ? !Input.GetKey(KeyCode.LeftShift) : Input.GetKey(KeyCode.LeftShift));
#endif
#if LAUNCH
            return Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl) &&
                (direction == right ? !Input.GetKey(KeyCode.LeftShift) : Input.GetKey(KeyCode.LeftShift));
#endif
        }
        return false;
    }

    void NewRotationAction(int direction) {
        ActionStack.instance.PushAction(new RotateComponentAction(gameObject, 90f * direction));
    }
}
