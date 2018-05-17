using UnityEngine;
using UnityEngine.UI;

public class SolenoidConfigWindow : MonoBehaviour {

    public GameObject baseButton;
    public ContentsManager contentsManager;
    public Text title;

    [ViewOnly] public PneumaticSolenoid pneumaticSolenoid;
    [ViewOnly] public ElectricSolenoid[] electricSolenoids;

    public void CloseContactWindow(ElectricSolenoid target) {
        pneumaticSolenoid.correlatedSolenoid = target;
        SimulationInput.instance.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    public void CancelOperation() {
        SimulationInput.instance.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    void Start() {
        SelectedObjects.instance.ClearSelection();
        SimulationInput.instance.gameObject.SetActive(false);
        GenerateButtons();
        GenerateTitle();
    }

    void GenerateButtons() {
        foreach (var solenoid in electricSolenoids) {
            GameObject newButton = Instantiate(baseButton);
            newButton.transform.GetChild(0).GetComponent<Text>().text = solenoid.nameStr;
            contentsManager.AddToContents(newButton);
            newButton.GetComponent<Button>().onClick.AddListener(() => {
                CloseContactWindow(solenoid);
            });
            newButton.SetActive(true);
        }
    }

    void GenerateTitle() {
        title.text = "Selecione um solenoide abaixo:";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelOperation();
    }
}
