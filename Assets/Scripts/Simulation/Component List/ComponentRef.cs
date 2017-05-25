using UnityEngine;

public class ComponentRef : MonoBehaviour {

    public GameObject floatingComponent;

    public void SetFloatingComponent() {
        FloatingSelection.instance.AddComponent(Instantiate(floatingComponent));
    }
}
