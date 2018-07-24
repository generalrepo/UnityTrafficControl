﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using RoyT.AStar;

public class Car : MonoBehaviour {

    public const float turningRate = 5f    ; // set this to 180 for immediate turns
    public const float MaxSpeed = 0.05f;
    float Speed = 0.02f;
    bool HasCollided = false;
    Vector2 target;
    Vector2 currentDirection;
    Position[] path;
    int waypoint;

	// Use this for initialization
	void Start ()
    {
        currentDirection = Util.DegreeToVector2(transform.eulerAngles.z + 90);
    }

    private void GetPath()
    {
        GameObject tileMap = GameObject.FindWithTag("RoadTile");
        ImpassMap impassMap = tileMap.GetComponent<ImpassMap>();
        path = impassMap.GetPath(new Position((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f)), new Position(5, -1));
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
            GetPath();
        }
        else if (!HasCollided)
        {
            UpdateWaypoint();
            UpdateMovement(Time.deltaTime);
        }
	}

    private void UpdateWaypoint()
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
    }

    private void UpdateMovement(float dt)
    {
        Steering(dt);
        MoveForward(dt);
    }

    private void Steering(float dt)
    {
        Vector2 desiredDirection = new Vector2(
            target.x - transform.position.x,
            target.y - transform.position.y);
        var desiredTurningAngle = Vector2.SignedAngle(currentDirection, desiredDirection);
        float turningAngle = 0;
        if (desiredTurningAngle > 0)
        {
            turningAngle = Mathf.Min(desiredTurningAngle, turningRate);
        }
        else
        {
            turningAngle = Mathf.Max(desiredTurningAngle, -turningRate);
        }
        transform.Rotate(0, 0, turningAngle);
        currentDirection = Util.DegreeToVector2(transform.eulerAngles.z + 90);
    }

    private void MoveForward(float dt)
    {
        Vector2 displacement = currentDirection.normalized * Speed;
        transform.position = new Vector3(
            transform.position.x + displacement.x,
            transform.position.y + displacement.y,
            transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        HasCollided = true;
    }
}
