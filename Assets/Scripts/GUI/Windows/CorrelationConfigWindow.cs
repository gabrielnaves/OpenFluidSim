using UnityEngine;
using UnityEngine.UI;

public class CorrelationConfigWindow : MonoBehaviour {

    public GameObject baseButton;
    public ContentsManager contentsManager;
    public Text title;

    [ViewOnly] public CorrelatedObject correlatedObject;
    [ViewOnly] public CorrelationTarget[] correlationTargets;

    public void CloseContactWindow(CorrelationTarget target) {
        if (correlatedObject.correlationTarget != target)
            ActionStack.instance.PushAction(new NewCorrelationAction(correlatedObject, target));
        Destroy(gameObject);
    }

    public void CancelOperation() {
        Destroy(gameObject);
    }

    void Start() {
        SelectedObjects.instance.ClearSelection();
        EditorInput.instance.gameObject.SetActive(false);
        ComponentListBar.instance.Disable();
        Taskbar.instance.Disable();
        CameraControlsGUI.instance.Disable();
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

    void OnDestroy() {
        EditorInput.instance.gameObject.SetActive(true);
        ComponentListBar.instance.Enable();
        Taskbar.instance.Enable();
        CameraControlsGUI.instance.Enable();
    }
}
