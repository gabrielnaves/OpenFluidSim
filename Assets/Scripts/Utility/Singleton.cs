using UnityEngine;

/// <summary>
/// Utility class for setting singletons
/// </summary>
/// The "singleton" referred to is created using automatic properties, and
/// having the setter be private to the desired class.
/// This implementation makes it easy to reference certain game objects through
/// an attached component, when said object is intended to be unique on scene.
/// 
/// Note that this implementation by itself does not enforce the traditional
/// singleton condition of "only one instance". You can still mess with it if
/// you really feel like it.
/// <example>
/// public class Foo : MonoBehaviour {
///     static public  Foo instance { get; private set; }
///     
///     void Awake() {
///         instance = (Foo)Singleton.Setup(this, instance);
///     }
/// }
/// </example>
static public class Singleton {

    static public Object Setup(Object source, Object destination) {
        if (destination == null)
            destination = source;
        else
            Debug.LogError("["+source+"]Awake: Multiple instances of a singleton.");
        return destination;
    }
}
