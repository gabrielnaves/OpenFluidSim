using UnityEngine;
using UnityEngine.UI;

public class CorrelationConfigWindow : MonoBehaviour {

    public GameObject baseButton;
    public ContentsManager contentsManager;
    public Text title;

    [ViewOnly] public CorrelatedObject correlatedObject;
    [ViewOnly] public CorrelationTarget[] correlationTargets;

    public void CloseContactWindow(CorrelationTarget target) {
        if (correlatedObject.correlationTarget != null)
            correlatedObject.correlationTarget.RemoveCorrelatedObject(correlatedObject);

        correlatedObject.correlationTarget = target;
        target.AddCorrelatedObject(correlatedObject);

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
        foreach (var target in correlationTargets) {
            GameObject newButton = Instantiate(baseButton);
            newButton.transform.GetChild(0).GetComponent<Text>().text = target.nameStr;
            contentsManager.AddToContents(newButton);
            newButton.GetComponent<Button>().onClick.AddListener(() => {
                CloseContactWindow(target);
            });
            newButton.SetActive(true);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelOperation();
    }
}
