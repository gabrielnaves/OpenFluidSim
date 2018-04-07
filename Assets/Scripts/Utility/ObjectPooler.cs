using UnityEngine;
using System.Collections.Generic;

 /// <summary> Implements a GameObject pool. </summary>
 /// The pool may or not have a maximum amount of objects.
 /// Objects in the pool will be placed as children of the pool, and will be kept deactivated.
public class ObjectPooler : MonoBehaviour {
	
	public GameObject pooledObject;
	public int initialAmount;
	public bool allowGrow = true; // Whether the pool's maximum object amount may increase.
	
	private Stack<GameObject> pooledObjects = new Stack<GameObject>();

    /// <summary> Returns an object if possible, null otherwise. </summary>
    /// This method will not activate the object before returning it.
    public GameObject GetObject() {
		if (pooledObjects.Count == 0 && !allowGrow)
			return null;
		if (pooledObjects.Count == 0 && allowGrow)
			return NewObject();
		return pooledObjects.Pop();
    }

    /// <summary> Returns an object to the pool, deactivating it. </summary>
    /// Returning an object to the pool does not guarantee that the maximum size will remain constant, should
	/// the *allowGrow* flag be *false*.
    /// Returning an object to the pool also places the object as child of the pool object.
    public void ReturnObject(GameObject obj) {
		obj.transform.parent = transform;
		obj.SetActive(false);
		pooledObjects.Push(obj);
	}

	void Start() {
		if (pooledObject == null)
			Debug.LogError("[ObjectPooler("+this+")Start: No pooled object given.");
		else
			InitPool();
	}
	
	private void InitPool() {
		for (int i = 0; i < initialAmount; ++i)
			pooledObjects.Push(NewObject());
	}
	
	private GameObject NewObject() {
		GameObject obj = Instantiate(pooledObject);
		obj.transform.parent = transform;
		obj.SetActive(false);
		return obj;
	}
}
