using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoyT.AStar;

public class GameController : MonoBehaviour
{

    public List<Vector3Int> spawnPoints;
    public List<Vector3Int> despawnPoints;
    public Car car;
    public Vector3 spawnValues;

    // Use this for initialization
    void Start()
    {
        GameObject respawnTilemapObject = GameObject.FindWithTag("Respawn");
        GameObject despawnTilemapObject = GameObject.FindWithTag("Finish");
        Tilemap respawnTilemap = respawnTilemapObject.GetComponent<Tilemap>();
        Tilemap despawnTilemap = despawnTilemapObject.GetComponent<Tilemap>();
        var cb = respawnTilemap.cellBounds;
        for (int x = cb.xMin; x < cb.xMax; x++)
        {
            for (int y = cb.yMin; y < cb.yMax; y++)
            {
                Vector3Int vec = new Vector3Int(x, y, 0);
                TileBase respawnTile = respawnTilemap.GetTile(vec);
                if (respawnTile != null)
                {
                    spawnPoints.Add(vec);
                }
                TileBase despawnTile = despawnTilemap.GetTile(vec);
                if (respawnTile != null)
                {
                    despawnPoints.Add(vec);
                }
            }
        }
        SpawnCars(cb);
        SpawnCars(cb);
        SpawnCars(cb);
    }

    void SpawnCars(BoundsInt cb)
    {
        Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];
        spawnPosition.x += 0.5f;
        spawnPosition.y += 0.5f;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(car, spawnPosition, spawnRotation);
        Vector3Int destination = despawnPoints[Random.Range(0, despawnPoints.Count)];
        car.SetDestination(new Position(destination.x, destination.y));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
