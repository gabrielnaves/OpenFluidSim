using System.Collections.Generic;
using UnityEngine;

public class ComponentLibrary : MonoBehaviour {

    static public ComponentLibrary instance { get; private set; }

    public GameObject[] componentPrefabs;
    public Dictionary<string, GameObject> nameToPrefab { get; private set; }

    void Awake() {
        instance = (ComponentLibrary)Singleton.Setup(this, instance);
        BuildComponentTable();
    }

    void BuildComponentTable() {
        nameToPrefab = new Dictionary<string, GameObject>();
        foreach (var prefab in componentPrefabs)
            nameToPrefab[prefab.name] = prefab;
    }
}
