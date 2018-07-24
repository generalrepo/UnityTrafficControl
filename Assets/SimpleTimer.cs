using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleTimer : MonoBehaviour
{
    public bool isRacing;
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
        if (isRacing &&
            txt != null)
        {
            var testHoldValue = Time.time.ToString("#");
            txt.text = testHoldValue;
        }
    }
}
