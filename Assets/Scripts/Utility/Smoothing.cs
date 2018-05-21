using UnityEngine;

static public class Smoothing {

    static public float SmoothStart(float x, int power) {
        x = Mathf.Clamp01(x);
        for (int i = 1; i < power; ++i) x *= x;
        return x;
    }

    static public float SmoothStop(float x, int power) {
        x = 1 - Mathf.Clamp01(x);
        for (int i = 1; i < power; ++i) x *= x;
        return 1 - x;
    }
}
