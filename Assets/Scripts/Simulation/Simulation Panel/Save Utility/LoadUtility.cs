using System.IO;
using UnityEngine;

public class LoadUtility : MonoBehaviour {

    static public LoadUtility instance { get; private set; }

    public string fileName = "savedFile.json";
    public string fileLocation = "SavedFiles/";

    SaveData data;

    void Awake() {
        instance = (LoadUtility)Singleton.Setup(this, instance);
    }

    public void LoadFromFile() {
        ReadDataContainer();
    }

    void ReadDataContainer() {
        StreamReader file = new StreamReader(fileLocation + fileName, System.Text.Encoding.UTF8);
        data = JsonUtility.FromJson<SaveData>(file.ReadToEnd());
        Debug.Log(JsonUtility.ToJson(data));
    }
}
