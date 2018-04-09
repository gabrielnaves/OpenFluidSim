using UnityEngine;

public class MechanicalContact : MonoBehaviour {

    void Start() {
        GetComponentInParent<ComponentConnections>().AddMechanicalContact(this);
    }
}
