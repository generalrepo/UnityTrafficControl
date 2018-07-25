using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using RoyT.AStar;
using UnityEngine.Tilemaps;
using System;

public class Car : MonoBehaviour {

    public const float turningRate = 10f    ; // set this to 180 for immediate turns
    public const float MaxSpeed = 0.05f;
    public const float Acceration = 0.5f;
    public const float Deacceration = 0.5f;
    public const float WallDistance = 0.5f;

    Vector2 Velocity;
    Vector2 desiredDirection;
    float Speed = 0.02f;

    bool HasCollided = false;
    Position destination;
    Vector2 target;
    Position[] path;
    int waypoint;

	// Use this for initialization
	void Start ()
    {
        destination = new Position(3, 6);
    }

    public void SetDestination(Position position)
    {
    }

    private void GetPath()
    {
        GameObject tileMap = GameObject.FindWithTag("RoadTile");

        if (tileMap != null)
        {
            ImpassMap impassMap = tileMap.GetComponent<ImpassMap>();
            path = impassMap.GetPath(new Position((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f)), destination);
            waypoint = 0;
            target = PositionToVector2D(path[waypoint]);
        }
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
        if (Vector2.Distance(target, transform.position) < 0.1)
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
        TargetFollowing(dt);
        Turn(dt);
    }

    private void TargetFollowing(float dt)
    {
        desiredDirection = new Vector2(
            target.x - transform.position.x,
            target.y - transform.position.y);
    }

    private void Turn(float dt)
    {
        Vector2 currentDirection = Util.DegreeToVector2(transform.eulerAngles.z + 90);
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
    }

    private void MoveForward(float dt)
    {
        Vector2 currentDirection = Util.DegreeToVector2(transform.eulerAngles.z + 90);
        Velocity = currentDirection.normalized * Speed;

        const float stoppingDistanceFactor = 20.0f;
        var predictedPosition = new Vector3(
            transform.position.x + stoppingDistanceFactor * Velocity.x,
            transform.position.y + stoppingDistanceFactor * Velocity.y,
            transform.position.z);

        var trafficLights = GameObject.FindWithTag("TrafficLightTile").GetComponent<TrafficLightMap>();

        var lightState = trafficLights.GetTrafficLightStatus(predictedPosition);

        bool isHorizontal = Math.Abs(currentDirection.x) > Math.Abs(currentDirection.y);
        if (lightState == null
            || (!isHorizontal && lightState == TrafficLightMap.TrafficLightState.Green)
            || (isHorizontal && lightState == TrafficLightMap.TrafficLightState.Red))

        {
            transform.position = new Vector3(
                transform.position.x + Velocity.x,
                transform.position.y + Velocity.y,
                transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        HasCollided = true;
    }
}
