using System.Collections.Generic;
using UnityEngine;

public class ComponentLibrary : MonoBehaviour {

    static public ComponentLibrary instance { get; private set; }

    public GameObject[] electricPrefabs;
    public GameObject[] pneumaticPrefabs;
    public GameObject[] hydraulicPrefabs;
    public Dictionary<string, GameObject> nameToPrefab { get; private set; }

    void Awake() {
        instance = (ComponentLibrary)Singleton.Setup(this, instance);
        BuildComponentTable();
    }

    void BuildComponentTable() {
        nameToPrefab = new Dictionary<string, GameObject>();
        AddComponentsFrom(electricPrefabs);
        AddComponentsFrom(pneumaticPrefabs);
        AddComponentsFrom(hydraulicPrefabs);
    }

    void AddComponentsFrom(GameObject[] prefabList) {
        foreach (var prefab in prefabList) {
            if (!nameToPrefab.ContainsKey(prefab.name))
                nameToPrefab[prefab.name] = prefab;
        }
    }
}
