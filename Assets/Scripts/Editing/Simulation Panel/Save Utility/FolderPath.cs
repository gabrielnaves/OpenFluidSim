using UnityEngine;

static public class FolderPath {

	static public string GetFolder() {
#if !UNITY_WINDOWS && !UNITY_EDITOR
        return Application.persistentDataPath + "SavedFiles/";
#else
        return "SavedFiles/";
#endif
    }
}
