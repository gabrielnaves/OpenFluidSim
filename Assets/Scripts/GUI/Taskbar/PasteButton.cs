using UnityEngine;

public class PasteButton : TaskbarButton {

    protected override bool ShouldShowButton() {
        return !Clipboard.IsEmpty();
    }

    public void PasteComponents() {
        LoadUtility.instance.AddFromClipboard(centerAroundMouse:false);
    }
}
