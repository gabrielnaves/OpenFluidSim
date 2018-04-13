using UnityEngine;
using UnityEngine.UI;

public class ContactConfigWindow : MonoBehaviour {

    public GameObject baseButton;
    public ContentsManager contentsManager;
    public Text title;

    [ViewOnly] public Contact contact;
    [ViewOnly] public ContactEnabler[] enablers;

    public void CloseContactWindow(ContactEnabler target) {
        contact.correlatedContact = target;
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
        foreach (var enabler in enablers) {
            GameObject newButton = Instantiate(baseButton);
            newButton.transform.GetChild(0).GetComponent<Text>().text = enabler.nameStr;
            contentsManager.AddToContents(newButton);
            newButton.GetComponent<Button>().onClick.AddListener(() => {
                CloseContactWindow(enabler);
            });
            newButton.SetActive(true);
        }
    }

    void GenerateTitle() {
        if (contact.type == Contact.Type.coil)
            title.text = "Selecione uma contatora abaixo:";
        else
            title.text = "Selecione um sensor abaixo:";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelOperation();
    }
}
