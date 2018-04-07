using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour {

    public string logFileName = "program_log_";
    public string logFileFolder = "Logs/";

    StreamWriter logFile;

    void Awake() {
        logFileName += System.DateTime.Now;
        logFileName = logFileName.Replace("/", "-");
        logFileName = logFileName.Replace(":", "-");
        logFileName = logFileName.Replace(" ", "_");
        logFileName += ".txt";

        logFile = new StreamWriter(logFileFolder + logFileName, false, System.Text.Encoding.UTF8);
    }

    void OnDestroy() {
        logFile.Close();
    }

    void OnEnable() {
        Application.logMessageReceived += HandleLog;
        Debug.Log("Enabling logger");
    }

    void OnDisable() {
        Debug.Log("Disabling logger");
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        logFile.Write("[" + type + "]: " + logString + "\n");
        logFile.Write(stackTrace);
    }
}
