using UnityEngine;
using UnityEngine.UI;

public class ContactConfigWindow : MonoBehaviour {

    public GameObject baseButton;
    public ContentsManager contentsManager;

    [ViewOnly] public Contact contact;

    public void CloseContactWindow(Coil target) {
        contact.correspondingCoil = target;
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
    }

    void GenerateButtons() {
        Coil[] coils = SimulationPanel.instance.GetActiveCoils();
        foreach (var coil in coils) {
            GameObject newButton = Instantiate(baseButton);
            newButton.transform.GetChild(0).GetComponent<Text>().text = coil.coilName;
            contentsManager.AddToContents(newButton);
            newButton.GetComponent<Button>().onClick.AddListener(() => {
                CloseContactWindow(coil);
            });
            newButton.SetActive(true);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelOperation();
    }
}
