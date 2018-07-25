using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public bool isRacing;
    private int testValue;
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
            testValue = testValue + 5;
            txt.text = testValue.ToString();
        }
    }
}
