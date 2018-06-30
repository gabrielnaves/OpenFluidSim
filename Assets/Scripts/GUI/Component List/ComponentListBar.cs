using System.Collections;
using UnityEngine;

public class ComponentListBar : MonoBehaviour {

    public static ComponentListBar instance { get; private set; }

    public float openingTime = 0.8f;
    public float barHeight = 101f;
    public CanvasGroup reverseTopBar;

    enum States { closed, opening, open, closing }
    States state = States.closed;

    RectTransform panelTransform;

    void Awake() {
        instance = (ComponentListBar)Singleton.Setup(this, instance);
        panelTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }

    void Start() {
        MovePanel(panelTransform.sizeDelta.y - barHeight);
    }

    public void Disable() {
        GetComponent<CanvasGroup>().interactable = false;
    }

    public void Enable() {
        GetComponent<CanvasGroup>().interactable = true;
    }

    public void OpenComponentList() {
        if (state == States.closed) {
            state = States.opening;
            StartCoroutine(Open());
            SelectedObjects.instance.ClearSelection();
            EditorInput.instance.gameObject.SetActive(false);
            Taskbar.instance.Disable();
            FloatingSelection.instance.RemoveCurrentComponent();
            CameraControlsGUI.instance.Disable();
        }
    }

    public void CloseComponentList() {
        if (state == States.open) {
            state = States.closing;
            StartCoroutine(Close());
            EditorInput.instance.gameObject.SetActive(true);
            Taskbar.instance.Enable();
            CameraControlsGUI.instance.Enable();
        }
    }

    IEnumerator Open() {
        reverseTopBar.interactable = true;
        float elapsedTime = 0;
        float startHeight = panelTransform.sizeDelta.y - barHeight;
        float endHeight = 0;
        while (elapsedTime < openingTime) {
            elapsedTime += Time.deltaTime;
            float percentage = Smoothing.SmoothStep(elapsedTime / openingTime, 2);
            MovePanel(Mathf.Lerp(startHeight, endHeight, percentage));
            reverseTopBar.alpha = Mathf.Lerp(0, 1, percentage);
            yield return null;
        }
        MovePanel(endHeight);
        state = States.open;
    }

    IEnumerator Close() {
        float elapsedTime = 0;
        float startHeight = 0;
        float endHeight = panelTransform.sizeDelta.y - barHeight;
        while (elapsedTime < openingTime) {
            elapsedTime += Time.deltaTime;
            float percentage = Smoothing.SmoothStep(elapsedTime / openingTime, 2);
            MovePanel(Mathf.Lerp(startHeight, endHeight, percentage));
            reverseTopBar.alpha = Mathf.Lerp(1, 0, percentage);
            yield return null;
        }
        MovePanel(endHeight);
        state = States.closed;
        reverseTopBar.interactable = false;
    }

    void MovePanel(float y) {
        var position = panelTransform.anchoredPosition;
        position.y = y;
        panelTransform.anchoredPosition = position;
    }
}
