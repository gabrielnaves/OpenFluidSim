using System;
using UnityEngine;

static public class Smoothing {

    static public float SmoothStart(float t, int power) {
        t = Mathf.Clamp01(t);
        for (int i = 1; i < power; ++i) t *= t;
        return t;
    }

    static public float SmoothStop(float t, int power) {
        t = 1 - Mathf.Clamp01(t);
        for (int i = 1; i < power; ++i) t *= t;
        return 1 - t;
    }

    static public float SmoothStep(float t, int power) {
        return Crossfade(SmoothStart(t, power), SmoothStop(t, power), t);
    }

    static public float Crossfade(float a, float b, float t) {
        return a + t * (b - a);
    }
}
