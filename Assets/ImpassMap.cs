using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoyT.AStar;

public class ImpassMap : MonoBehaviour {
    
    private Tilemap ground;
    public RoyT.AStar.Grid grid = new RoyT.AStar.Grid(15, 15, float.PositiveInfinity);

    private bool initialized = false;

    void Start ()
    {
        this.Initialize();
    }

    public Position[] GetPath(Position start, Position end)
    {
        // 
        if (!this.initialized)
        {
            this.Initialize();
        }

        var cb = ground.cellBounds;
        Position[] path = grid.GetPath(
            new Position(
                start.X - cb.xMin,
                start.Y - cb.yMin),
            new Position(
                end.X - cb.xMin,
                end.Y - cb.yMin),
            MovementPatterns.LateralOnly);

        Position[] adjustedPath = new Position[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            adjustedPath[i] = new Position(
                path[i].X + cb.xMin,
                path[i].Y + cb.yMin);
        }

        return adjustedPath;
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void Initialize()
    {
        if (initialized)
        {
            return;
        }

        this.ground = this.GetComponent<Tilemap>();
        var cb = this.ground.cellBounds;

        for (int x = cb.xMin; x < cb.xMax; x++)
        {
            for (int y = cb.yMin; y < cb.yMax; y++)
            {
                TileBase tile = ground.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    grid.SetCellCost(new Position(x - cb.xMin, y - cb.yMin), 1.0f);
                }
            }
        }

        this.initialized = true;
    }
}
