using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the "contents" game object inside the component list hierarchy
/// </summary>
/// Provides a function for adding the "GUI-version" of electric/pneumatic
/// components to the component list.
/// Automatically resizes the "contents" game object to accomodate
/// any amount of components inside the component list.
/// The component list is implemented using Unity's ScrollView.
/// Every "GUI component" is a Unity Button with a ComponentRef. 
public class ComponentListContents : MonoBehaviour {

    public GameObject[] electricGUIPrefabs;
    public GameObject[] pneumaticGUIPrefabs;
    public GameObject[] hydraulicGUIPrefabs;

    List<GameObject> electric = new List<GameObject>();
    List<GameObject> pneumatic = new List<GameObject>();
    List<GameObject> hydraulic = new List<GameObject>();

    RectTransform rectTransform;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start() {
        InstantiateGUIComponents(electricGUIPrefabs, electric);
        InstantiateGUIComponents(pneumaticGUIPrefabs, pneumatic);
        InstantiateGUIComponents(hydraulicGUIPrefabs, hydraulic);
    }

    void InstantiateGUIComponents(GameObject[] prefabs, List<GameObject> targetList) {
        foreach (var prefab in prefabs) {
            var component = Instantiate(prefab);
            component.GetComponent<RectTransform>().SetParent(rectTransform);
            component.GetComponent<RectTransform>().localScale = Vector3.one;
            component.GetComponent<RectTransform>().position = Vector3.zero;
            component.SetActive(false);
            targetList.Add(component);
        }
    }

    public void ShowElectricComponents() {
        ToggleComponents(electric, true);
        ToggleComponents(pneumatic, false);
        ToggleComponents(hydraulic, false);
    }

    public void ShowPneumaticComponents() {
        ToggleComponents(electric, false);
        ToggleComponents(pneumatic, true);
        ToggleComponents(hydraulic, false);
    }

    public void ShowHydraulicComponents() {
        ToggleComponents(electric, false);
        ToggleComponents(pneumatic, false);
        ToggleComponents(hydraulic, true);
    }

    void ToggleComponents(List<GameObject> components, bool state) {
        foreach (var component in components)
            component.SetActive(state);
    }
}
