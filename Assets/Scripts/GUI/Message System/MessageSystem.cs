using UnityEngine;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour {

    public static MessageSystem instance { get; private set; }

    public GameObject messageObjectPrefab;

    GameObject previousMessage;

    void Awake() {
        instance = (MessageSystem)Singleton.Setup(this, instance);
    }

    public void GenerateMessage(string message) {
        var messageObject = Instantiate(messageObjectPrefab);
        messageObject.GetComponent<RectTransform>().SetParent(transform, false);
        messageObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        messageObject.GetComponentInChildren<Text>().text = message;
        messageObject.SetActive(true);
        if (previousMessage != null)
            Destroy(previousMessage);
        previousMessage = messageObject;
    }
}
