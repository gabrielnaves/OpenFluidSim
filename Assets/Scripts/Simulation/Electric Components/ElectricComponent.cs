using UnityEngine;

public abstract class ElectricComponent : MonoBehaviour {

    [ViewOnly] public bool input;
    public abstract bool GetOutput();
}
