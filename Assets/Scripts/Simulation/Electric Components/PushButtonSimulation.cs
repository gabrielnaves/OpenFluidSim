using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonSimulation : ElectricComponent {

    public Sprite inactiveSprite;
    public Sprite activeSprite;
    public Contact.State startingState;
    [ViewOnly] public Contact.State state;

    ComponentReferences connections;
    SpriteRenderer spriteRenderer;
    new Collider2D collider;
    bool simulating;
    float receivedSignal;

    void Awake() {
        connections = GetComponent<ComponentReferences>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    public override void Setup() {
        state = startingState;
        spriteRenderer.sprite = inactiveSprite;
        simulating = true;
    }

    public override void Stop() {
        spriteRenderer.sprite = inactiveSprite;
        simulating = false;
    }

    public override void RespondToSignal(Connector source, float signal) {
        if (state == Contact.State.closed) {
            foreach (var connector in connections.connectorList)
                ElectricSimulationEngine.instance.SpreadSignal(connector, signal);
            receivedSignal = signal;
        }
    }

    public void Update() {
        if (SimulationMode.instance.mode == SimulationMode.Mode.simulation) {
            if (Input.GetMouseButtonDown(0)) {
                if (collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
                    state = (startingState == Contact.State.open ? Contact.State.closed : Contact.State.open);
                    spriteRenderer.sprite = activeSprite;
                }
            }
            else if (Input.GetMouseButtonUp(0)) {
                state = startingState;
                spriteRenderer.sprite = inactiveSprite;
            }
        }
    }

    void LateUpdate() {
        if (simulating) {
            if (receivedSignal > 0)
                spriteRenderer.color = Color.magenta;
            else if (receivedSignal < 0)
                spriteRenderer.color = Color.green;
            else
                spriteRenderer.color = Color.black;
            receivedSignal = 0;
        }
    }
}
