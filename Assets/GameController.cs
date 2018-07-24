using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{

    public List<Vector3Int> spawnPoints;
    public GameObject car;
    public Vector3 spawnValues;

	// Use this for initialization
	void Start () {
        GameObject tilemapObject = GameObject.FindWithTag("House");
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
        SpawnCars();
	}

    void SpawnCars()
    {
        Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(car, spawnPosition, spawnRotation);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
