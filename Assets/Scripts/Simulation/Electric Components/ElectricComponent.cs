using UnityEngine;

public abstract class ElectricComponent : MonoBehaviour {

    public virtual void Setup() { }
    public abstract void RespondToSignal(Connector source, float signal);
}
