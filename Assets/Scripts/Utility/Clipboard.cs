public static class Clipboard {

    static string clipboard;

    public static string GetClipboard() {
        return clipboard;
    }

    public static void SetClipboard(string value) {
        clipboard = value;
    }

    public static void EmptyClipboard() {
        clipboard = "";
    }
}
