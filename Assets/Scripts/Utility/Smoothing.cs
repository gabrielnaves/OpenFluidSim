static public class Smoothing {

    static public float SmoothStart(float t, int power) {
        return Pow(t, power);
    }

    static public float SmoothStop(float t, int power) {
        return Flip(Pow(Flip(t), power));
    }

    static public float SmoothStep(float t, int power) {
        return Crossfade(SmoothStart(t, power), SmoothStop(t, power), t);
    }

    static public float Crossfade(float a, float b, float t) {
        return a + t * (b - a);
    }

    static public float Flip(float t) {
        return 1 - t;
    }

    static public float Pow(float t, int power) {
        for (int i = 1; i < power; ++i) t *= t;
        return t;
    }
}
