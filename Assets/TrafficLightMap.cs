using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrafficLightMap : MonoBehaviour
{
    public enum TrafficLightState
    {
        Green,
        Yellow,
        Red,
    }

    public TileBase redLight;
    public TileBase greenLight;

    public TrafficLightState? GetTrafficLightStatus(Vector3 worldLocation)
    {
        var o = GameObject.FindWithTag("TrafficLightTile");
        var trafficLights = o.GetComponent<Tilemap>();
        Camera cam = Camera.main;

        Vector3 point = cam.ScreenToWorldPoint(worldLocation);

        Vector3Int cameraCell = trafficLights.WorldToCell(point);
        var tile = trafficLights.GetTile(cameraCell);

        if (tile == null)
        {
            return null;
        }

        return tile == this.redLight ? TrafficLightState.Red : TrafficLightState.Green;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        var o = GameObject.FindWithTag("TrafficLightTile");

        var trafficLights = o.GetComponent<Tilemap>();

        var trafficBoundaries = trafficLights.cellBounds;

        Vector3 point = new Vector3();
        Camera cam = Camera.main;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        Vector3Int cameraCell = trafficLights.WorldToCell(point);
        var tile = trafficLights.GetTile(cameraCell);

        if (tile != null)
        {
            trafficLights.SetTile(cameraCell, tile == this.redLight ? this.greenLight : this.redLight);
        }
    }
}
