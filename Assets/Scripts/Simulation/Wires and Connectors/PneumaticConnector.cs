using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PneumaticConnector : MonoBehaviour {

    public List<PneumaticConnector> connectedObjects = new List<PneumaticConnector>();

    void Start() {
        GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    void Update() {

    }
}
