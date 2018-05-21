using UnityEngine;
using UnityEngine.UI;

public class CylinderConfigWindow : MonoBehaviour {

    public InputField percentageInputField;
    public Slider percentageSlider;
    public InputField movementTimeInputField;
    public Slider movementTimeSlider;

    [ViewOnly] public CylinderEditing cylinderEditing;

    public void CloseWindow() {
        EditorInput.instance.gameObject.SetActive(true);
        Destroy(gameObject);
        cylinderEditing.UpdateSprites();
    }

    public void UpdatePercentage(string valueStr) {
        int value = 0;
        if (int.TryParse(valueStr, out value)) {
            cylinderEditing.startingPercentage = value / 100f;
            percentageSlider.value = value;
        }
    }

    public void UpdatePercentageEnd(string valueStr) {
        int value = 0;
        int.TryParse(valueStr, out value);
        value = Mathf.Clamp(value, 0, 100);
        cylinderEditing.startingPercentage = value / 100f;
        percentageInputField.text = value.ToString();
        percentageSlider.value = value;
    }

    public void UpdatePercentage(float value) {
        cylinderEditing.startingPercentage = value / 100f;
        percentageInputField.text = value.ToString();
    }

    public void UpdateTime(string valueStr) {
        float value = 0f;
        if (float.TryParse(valueStr, out value)) {
            if (value >= movementTimeSlider.minValue && value <= movementTimeSlider.maxValue) {
                cylinderEditing.movementTime = value;
                movementTimeSlider.value = value;
            }
        }
    }

    public void UpdateTimeEnd(string valueStr) {
        float value = 0f;
        float.TryParse(valueStr, out value);
        value = Mathf.Clamp(value, movementTimeSlider.minValue, movementTimeSlider.maxValue);
        cylinderEditing.movementTime = value;
        movementTimeInputField.text = value.ToString();
        movementTimeSlider.value = value;
    }

    public void UpdateTime(float value) {
        cylinderEditing.movementTime = value;
        movementTimeInputField.text = value.ToString();
    }

    void Start() {
        SelectedObjects.instance.ClearSelection();
        EditorInput.instance.gameObject.SetActive(false);
        SetupInitialValues();
    }

    void SetupInitialValues() {
        int startingPercentage = (int)(cylinderEditing.startingPercentage * 100f);
        percentageInputField.text = startingPercentage.ToString();
        percentageSlider.value = startingPercentage;
        movementTimeInputField.text = cylinderEditing.movementTime.ToString();
        movementTimeSlider.value = cylinderEditing.movementTime;
    }

    void Update() {
        cylinderEditing.UpdateSprites();
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseWindow();
    }
}
