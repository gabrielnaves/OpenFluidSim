using UnityEngine;

public class MechanicalContact : MonoBehaviour {

    void Start() {
        GetComponentInParent<ComponentReferences>().AddMechanicalContact(this);
    }
}
