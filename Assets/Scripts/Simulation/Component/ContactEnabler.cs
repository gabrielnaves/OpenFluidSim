using UnityEngine;
using UnityEngine.UI;

public class ContactEnabler : MonoBehaviour {

    public Text nameText;

    public string nameStr {
        get {
            return nameText.text;
        }
    }
}
