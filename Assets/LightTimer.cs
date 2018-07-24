using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LightTimer : MonoBehaviour
{
    public bool isRed;
    private string redLightTime;
    Text txt;


    void Awake()
    {
        txt = GetComponent<Text>();

    }

    void Start()
    {
        isRed = true;
        redLightTime = Time.time.ToString("#");
        txt = GetComponent<Text>();
    }

    void Update()
    {
        if (isRed && redLightTime != "0")
        {
            redLightTime = Time.timeSinceLevelLoad.ToString("#");
            txt.text = redLightTime;
        }
    }
}
