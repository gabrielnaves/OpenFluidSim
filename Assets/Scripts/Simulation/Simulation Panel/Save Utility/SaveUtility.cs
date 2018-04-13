using System.IO;
using UnityEngine;

public class SaveUtility : MonoBehaviour {

    static public SaveUtility instance { get; private set; }

    public string fileName = "savedFile.json";
    public string fileLocation = "SavedFiles/";

    SaveData data;

    void Awake() {
        instance = (SaveUtility)Singleton.Setup(this, instance);
    }

    public void SaveToFile() {
        CreateSaveDataContainer();
        WriteDataToFile();
    }

    void CreateSaveDataContainer() {
        var activeComponents = SimulationPanel.instance.GetActiveComponents();
        data = new SaveData() {
            components = new string[activeComponents.Length],
            positions = new Vector3[activeComponents.Length]
        };
        for (int i = 0; i < activeComponents.Length; ++i) {
            data.components[i] = activeComponents[i].name;
            data.positions[i] = activeComponents[i].transform.position;
        }
    }

    void WriteDataToFile() {
        StreamWriter file = new StreamWriter(fileLocation + fileName, false, System.Text.Encoding.UTF8);
        file.Write(JsonUtility.ToJson(data, true));
        file.Close();
    }
}
