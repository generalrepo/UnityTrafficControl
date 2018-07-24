using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using RoyT.AStar;

public class Car : MonoBehaviour {

    float Speed = 0.02f;
    bool HasCollided = false;
    Vector2 target;
    Position[] path;
    int waypoint;

	// Use this for initialization
	void Start () {
        GameObject tileMap = GameObject.FindWithTag("RoadTile");
        ImpassMap impassMap = tileMap.GetComponent<ImpassMap>();
        path = impassMap.GetPath(new Position((int)transform.position.x, (int)transform.position.y), new Position(5, -1));
        waypoint = 0;
        target = PositionToVector2D(path[waypoint]);
    }

    Vector2 PositionToVector2D(Position loc)
    {
        return new Vector2(loc.X + 0.5f, loc.Y + 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

        if (path == null)
        {
            Start();
        }
        else if (!HasCollided)
        {
            // check if waypoint reached
            if (Vector2.Distance(target, transform.position) < 0.02)
            {
                waypoint++;
                if (waypoint < path.Length)
                {
                    // set to next waypoint
                    target = PositionToVector2D(path[waypoint]);
                }
                else
                {
                    // Remove Car
                }
                
            }

            Vector2 direction = new Vector2(
                target.x - transform.position.x,
                target.y - transform.position.y);
            
            Vector2 displacement = direction.normalized * Speed;

            transform.Rotate(0, 0, Vector2.Angle(Util.DegreeToVector2(transform.eulerAngles.z+90), direction));

            transform.position = new Vector3(
                transform.position.x + displacement.x,
                transform.position.y + displacement.y,
                transform.position.z);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        HasCollided = true;
    }
}
