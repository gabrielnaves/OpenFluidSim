using UnityEngine;
using UnityEngine.UI;

public abstract class CorrelationTarget : MonoBehaviour {

    public Text nameText;

    public string nameStr {
        get {
            return nameText.text;
        }
    }
}
