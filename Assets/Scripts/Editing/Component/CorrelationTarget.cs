using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CorrelationTarget : MonoBehaviour {

    public Text nameText;

    public string nameStr {
        get {
            return nameText.text;
        }
    }

    [ViewOnly] public List<CorrelatedObject> correlatedObjects = new List<CorrelatedObject>();

    public void AddCorrelatedObject(CorrelatedObject obj) {
        if (!correlatedObjects.Contains(obj))
            correlatedObjects.Add(obj);
    }

    public void RemoveCorrelatedObject(CorrelatedObject obj) {
        if (correlatedObjects.Contains(obj))
            correlatedObjects.Remove(obj);
    }
}
