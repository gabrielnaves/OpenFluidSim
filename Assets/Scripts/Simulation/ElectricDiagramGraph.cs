using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDiagramGraph : MonoBehaviour {

    public static ElectricDiagramGraph instance { get; private set; }

    void Awake() {
        instance = (ElectricDiagramGraph)Singleton.Setup(this, instance);
    }

    public void BuildGraph() {

    }
}
