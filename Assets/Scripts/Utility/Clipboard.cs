using UnityEngine;

public static class Clipboard {

    public static string GetClipboard() {
        return GUIUtility.systemCopyBuffer;
    }

    public static void SetClipboard(string value) {
        GUIUtility.systemCopyBuffer = value;
    }

    public static void EmptyClipboard() {
        GUIUtility.systemCopyBuffer = "";
    }
}
