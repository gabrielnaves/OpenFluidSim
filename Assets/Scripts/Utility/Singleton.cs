using UnityEngine;

static public class Singleton {
    public static MonoBehaviour Setup(MonoBehaviour source, MonoBehaviour destination) {
        if (destination == null)
            destination = source;
        else
            Debug.LogError("["+source+"Awake: Multiple instances of a singleton.");
        return destination;
    }
}
