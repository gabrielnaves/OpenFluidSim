using System.Collections.Generic;
using UnityEngine;

public class ComponentLibrary : MonoBehaviour {

    public string[] componentNames;
    public GameObject[] componentPrefabs;

    public Dictionary<string, GameObject> nameToPrefab { get; private set; }

    void Awake() {
        if (componentNames.Length != componentPrefabs.Length)
            Debug.LogError("Component library configuration error");
        else
            BuildComponentTable();
    }

    void BuildComponentTable() {
        nameToPrefab = new Dictionary<string, GameObject>();
        for (int i = 0; i < componentNames.Length; ++i)
            nameToPrefab[componentNames[i]] = componentPrefabs[i];
    }
}
