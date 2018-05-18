using System.Collections.Generic;
using UnityEngine;

public class MechanicalContact : MonoBehaviour {

    [ViewOnly] public List<MechanicalContact> otherContacts = new List<MechanicalContact>();

    public bool IsTouching() {
        return otherContacts.Count > 0;
    }

    void Start() {
        GetComponentInParent<ComponentReferences>().AddMechanicalContact(this);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        var contact = collision.GetComponent<MechanicalContact>();
        if (contact) {
            if (!otherContacts.Contains(contact))
                otherContacts.Add(contact);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        var contact = collision.GetComponent<MechanicalContact>();
        if (contact) {
            if (otherContacts.Contains(contact))
                otherContacts.Remove(contact);
        }
    }
}
