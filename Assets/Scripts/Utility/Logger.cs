using System.IO;
using UnityEngine;

/// <summary>
/// MonoBehaviour for creating system text logs.
/// </summary>
/// The Logger class creates each log file using "program_log_" + local timestamp.
/// Every log file is created inside "Logs/" folder.
/// Log files are created if the System Logger object is in the current scene,
/// but Log messages will not be stored if the Logger instance is disabled.
public class Logger : MonoBehaviour {

    public string logFileName = "program_log_";
    public string logFileFolder = "Logs/";

    static Logger instance;

    StreamWriter logFile;

    void Awake() {
        InstanceSetup();
        if (instance == this) {
            GenerateLogFileNameWithTimestamp();
            OpenLogFile();
        }
    }

    void InstanceSetup() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void GenerateLogFileNameWithTimestamp() {
        logFileName += System.DateTime.Now;
        logFileName = logFileName.Replace("/", "-");
        logFileName = logFileName.Replace(":", "-");
        logFileName = logFileName.Replace(" ", "_");
        logFileName += ".txt";
    }

    void OpenLogFile() {
        logFile = new StreamWriter(logFileFolder + logFileName, false, System.Text.Encoding.UTF8);
    }

    void OnDestroy() {
        if (logFile != null)
            logFile.Close();
    }

    void OnEnable() {
        if (instance == this) {
            Application.logMessageReceived += HandleLog;
            Debug.Log("Enabling logger");
        }
    }

    void OnDisable() {
        if (instance == this) {
            Debug.Log("Disabling logger");
            Application.logMessageReceived -= HandleLog;
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        logFile.Write("[" + type + "]: " + logString + "\n");
        logFile.Write(stackTrace);
    }
}
