using UnityEngine;

public class ComponentRotate : MonoBehaviour {

    private int right = -1;
    private int left = 1;

	void Update () {
        if (RequestedRotate(right))
            NewRotationAction(right);
        if (RequestedRotate(left))
            NewRotationAction(left);
	}

    private bool RequestedRotate(int direction) {
        return SelectedComponent.instance.component == gameObject && Input.GetKeyDown(KeyCode.R) &&
            (direction == right ? !Input.GetKey(KeyCode.LeftShift) : Input.GetKey(KeyCode.LeftShift));
    }

    private void NewRotationAction(int direction) {
        var rotationAction = new RotateComponentAction();
        rotationAction.referencedObject = gameObject;
        rotationAction.rotationAmount = 90f * direction;
        rotationAction.DoAction();
        ActionStack.instance.PushAction(rotationAction);
    }
}
