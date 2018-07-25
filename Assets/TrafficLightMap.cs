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

        const float approximateTrafficLight = 0.4f;
        const float step = 0.1f;

        for (float i = -approximateTrafficLight; i < approximateTrafficLight; i += step)
        {
            for (float j = -approximateTrafficLight; j < approximateTrafficLight; j += step)
            {
                Vector3Int trafficCell = trafficLights.WorldToCell(new Vector3(worldLocation.x + i, worldLocation.y + j, worldLocation.z));

                TileBase tileBase = trafficLights.GetTile(trafficCell);
                if (tileBase != null)
                {
                    return tileBase == this.redLight ? TrafficLightState.Red : TrafficLightState.Green;
                }
            }
        }

        /*
        TileBase tileBase = trafficLights.GetTile(trafficCell);
        if (tileBase != null)
        {
            return tileBase == this.redLight ? TrafficLightState.Red : TrafficLightState.Green;
        }
        */

        return null;
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
