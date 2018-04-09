using UnityEngine;

/// <summary>
/// Substitutes the script's gameObject for the selected prefab
/// </summary>
/// The substitution is only done on "Start" time, so the target prefab
/// can be set by some other script during "Awake" time, if so desired.
/// This script can be used to mimic prefab nesting behaviour, as Unity
/// has no support for nested prefab. This way a prefab can be a child
/// of another prefab while maintaining its "individuality" as a prefab.
public class SubstituteForPrefab : MonoBehaviour {

    public GameObject targetPrefab;

	void Start() {
        GameObject obj = Instantiate(targetPrefab);
        obj.transform.parent = transform.parent;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        Destroy(gameObject);
	}
}
