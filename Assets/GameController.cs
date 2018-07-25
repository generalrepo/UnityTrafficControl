using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoyT.AStar;
using System;

public class GameController : MonoBehaviour
{

    public List<Vector3Int> spawnPoints;
    public List<Vector3Int> despawnPoints;
    public Car car;
    public Vector3 spawnValues;

    private BoundsInt cellBounds;
    private DateTime lastSpawn;

    // Use this for initialization
    void Start()
    {
        GameObject respawnTilemapObject = GameObject.FindWithTag("Respawn");
        GameObject despawnTilemapObject = GameObject.FindWithTag("Finish");
        Tilemap respawnTilemap = respawnTilemapObject.GetComponent<Tilemap>();
        Tilemap despawnTilemap = despawnTilemapObject.GetComponent<Tilemap>();
        this.cellBounds = respawnTilemap.cellBounds;
        for (int x = cellBounds.xMin; x < cellBounds.xMax; x++)
        {
            for (int y = cellBounds.yMin; y < cellBounds.yMax; y++)
            {
                Vector3Int vec = new Vector3Int(x, y, 0);
                TileBase respawnTile = respawnTilemap.GetTile(vec);
                if (respawnTile != null)
                {
                    spawnPoints.Add(vec);
                }
                TileBase despawnTile = despawnTilemap.GetTile(vec);
                if (despawnTile != null)
                {
                    despawnPoints.Add(vec);
                }
            }
        }
        SpawnCars();
        SpawnCars();
        SpawnCars();

        lastSpawn = DateTime.Now;
    }

    void SpawnCars()
    {
        Vector3 spawnPosition = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        spawnPosition.x += 0.5f;
        spawnPosition.y += 0.5f;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(car, spawnPosition, spawnRotation);
        Vector3Int destination = despawnPoints[UnityEngine.Random.Range(0, despawnPoints.Count)];
        car.SetDestination(new Position(destination.x, destination.y));
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now - this.lastSpawn > TimeSpan.FromSeconds(5))
        {
            SpawnCars();
            this.lastSpawn = DateTime.Now;
        }
    }
}
