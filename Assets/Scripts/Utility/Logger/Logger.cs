using UnityEngine;

public static class Logger {

    public static void Log(string msg) {
#if UNITY_EDITOR
        Debug.Log(msg);
#endif
#if GAME_CONSOLE_IS_ENABLED && !UNITY_EDITOR
        GameConsole.Instance.AddLogInfoMsg(msg);
#endif
    }

    public static void LogWarning(string msg) {
#if UNITY_EDITOR
        Debug.LogWarning(msg);
#endif
#if GAME_CONSOLE_IS_ENABLED && !UNITY_EDITOR
        GameConsole.Instance.AddLogWarningMsg(msg);
#endif
    }

    public static void LogError(string msg) {
#if UNITY_EDITOR
        Debug.LogError(msg);
#endif
#if GAME_CONSOLE_IS_ENABLED && !UNITY_EDITOR
        GameConsole.Instance.AddLogErrorMsg(msg);
#endif
    }

    public static void Log(Object obj, string msg) {
        Log("[" + obj + "]" + msg);
    }

    public static void LogWarning(Object obj, string msg) {
        LogWarning("[" + obj + "]" + msg);
    }

    public static void LogError(Object obj, string msg) {
        LogError("[" + obj + "]" + msg);
    }

    public static void Log(Object obj, string method_name, string msg) {
        Log("[" + obj + "]" + method_name + ": " + msg);
    }

    public static void LogWarning(Object obj, string method_name, string msg) {
        LogWarning("[" + obj + "]" + method_name + ": " + msg);
    }

    public static void LogError(Object obj, string method_name, string msg) {
        LogError("[" + obj + "]" + method_name + ": " + msg);
    }
}
