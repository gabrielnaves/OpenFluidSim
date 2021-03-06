﻿using UnityEngine;

/// <summary>
/// The simulation grid that components snap to
/// </summary>
public static class SimulationGrid {

    static public float cellSize = 0.12f;
    
    static public Vector2 FitToGrid(Vector2 point) {
        return GridCoordinatesToWorldSpace(PointToGridCoordinates(point));
    }

    static public Vector2 PointToGridCoordinates(Vector2 point) {
        return new Vector2(Mathf.Round(point.x / cellSize), Mathf.Round(point.y / cellSize));
    }

    static public Vector2 GridCoordinatesToWorldSpace(Vector2 gridPoint) {
        return gridPoint * cellSize;
    }
}
