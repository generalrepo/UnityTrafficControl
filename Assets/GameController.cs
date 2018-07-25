﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{

    public List<Vector3Int> spawnPoints;
    public GameObject car;
    public Vector3 spawnValues;

    // Use this for initialization
    void Start()
    {
        GameObject tilemapObject = GameObject.FindWithTag("Respawn");
        Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
        var cb = tilemap.cellBounds;
        for (int x = cb.xMin; x < cb.xMax; x++)
        {
            for (int y = cb.yMin; y < cb.yMax; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    spawnPoints.Add(new Vector3Int(x, y, 0));
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
        Quaternion spawnRotation = new Quaternion();
        if (spawnPosition.x == cb.xMin)
        {
            spawnRotation.SetLookRotation(new Vector3(1, 0));
        }
        else if (spawnPosition.x == cb.xMax)
        {
            spawnRotation.SetLookRotation(new Vector3(-1, 0));
        }
        else if (spawnPosition.y == cb.yMin)
        {
            spawnRotation.SetLookRotation(new Vector3(0, 1));
        }
        else if (spawnPosition.y == cb.yMax)
        {
            spawnRotation.SetLookRotation(new Vector3(0, -1));
        }
        else
        {
            spawnRotation = Quaternion.identity;
        }
        Instantiate(car, spawnPosition, spawnRotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
