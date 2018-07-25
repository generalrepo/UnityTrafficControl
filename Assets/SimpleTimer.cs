using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleTimer : MonoBehaviour
{
    public bool isRacing;
    private string testValue;
    Text txt;


    void Awake()
    {
        txt = GetComponent<Text>();

    }

    void Start()
    {
        isRacing = true;
        txt = GetComponent<Text>();

    }

    void Update()
    {
        if (isRacing)
        {
            testValue = Time.timeSinceLevelLoad.ToString("#");
            txt.text = testValue;
        }
    }
}
