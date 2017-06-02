using UnityEngine;

public static class SimulationGrid {

    static public float cellSize = 0.06f;
    
    static public Vector2 FitToGrid(Vector2 point) {
        return GridCoordinatesToWorldSpace(PointToGridCoordinates(point));
    }

    static public Vector2 PointToGridCoordinates(Vector2 point) {
        return new Vector2(Mathf.Floor(point.x / cellSize), Mathf.Floor(point.y / cellSize));
    }

    static public Vector2 GridCoordinatesToWorldSpace(Vector2 gridPoint) {
        return gridPoint * cellSize;
    }
}
