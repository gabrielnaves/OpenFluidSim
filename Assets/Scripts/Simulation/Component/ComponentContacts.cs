using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Substitutes dummy contacts on component prefabs for mechanical contacts
/// </summary>
/// This script circumvents Unity's lack of support for nested prefabs,
/// that is, prefabs that are part of other prefabs, but still keep their
/// independence.
/// This way many component prefabs can make use of the single mechanical contact
/// prefab.
/// TODO: Merge this implementation with ComponentConnectors
public class ComponentContacts : MonoBehaviour {

    public List<GameObject> contactList { get; private set; }
    public GameObject mechanicalContactPrefab;

    private List<Vector3> contactLocalPositions = new List<Vector3>();

    void Awake() {
        GetContactPositions();
        RemoveDummyContacts();
        CreateMechanicalContacts();
    }

    private void GetContactPositions() {
        foreach (Transform child in transform)
            contactLocalPositions.Add(child.localPosition);
    }

    private void RemoveDummyContacts() {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void CreateMechanicalContacts() {
        contactList = new List<GameObject>();
        foreach (Vector3 localPositions in contactLocalPositions) {
            var newContact = Instantiate(mechanicalContactPrefab);
            newContact.transform.parent = transform;
            newContact.transform.localPosition = localPositions;
            contactList.Add(newContact);
        }
    }
}
