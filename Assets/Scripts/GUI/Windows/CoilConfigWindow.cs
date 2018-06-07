using UnityEngine;
using UnityEngine.UI;

public class CoilConfigWindow : MonoBehaviour {

    public Dropdown typeDropdown;
    public InputField delayInputField;
    public GameObject delayConfiguration;

    [ViewOnly] public Coil coil;

    public void SetCoilType(int value) {
        coil.UpdateType((Coil.Type)value);
        delayConfiguration.SetActive(coil.type != Coil.Type.normal);
    }

    public void SetCoilDelay(string valueStr) {
        float value = 0f;
        float.TryParse(valueStr, out value);
        coil.UpdateDelay(Mathf.Abs(value));
        delayInputField.text = coil.delay.ToString();
    }

    public void CloseWindow() {
        Destroy(gameObject);
    }

    void Start() {
        SelectedObjects.instance.ClearSelection();
        EditorInput.instance.gameObject.SetActive(false);
        ComponentListBar.instance.Disable();
        Taskbar.instance.Disable();
        SetupInitialValues();
    }

    void SetupInitialValues() {
        typeDropdown.value = (int)coil.type;
        delayInputField.text = coil.delay.ToString();
        delayConfiguration.SetActive(coil.type != Coil.Type.normal);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseWindow();
    }

    void OnDestroy() {
        EditorInput.instance.gameObject.SetActive(true);
        ComponentListBar.instance.Enable();
        Taskbar.instance.Enable();
    }
}
