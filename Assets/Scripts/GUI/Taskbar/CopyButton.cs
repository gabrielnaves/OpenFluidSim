using UnityEngine;

public class CopyButton : TaskbarButton {

    protected override bool ShouldShowButton() {
        return SelectedObjects.instance.HasObject();
    }

    public void CopyComponents() {
        Clipboard.SetClipboard(SaveUtility.instance.GetSelectedComponentsSaveString());
    }
}
