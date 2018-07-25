using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public bool isRacing;
    private int score = 0;
    Text txt;
    
    public void IncrementScore()
    {
        this.score++;
    }

    void Awake()
    {
        txt = GetComponent<Text>();

    }

    void Start()
    {
        isRacing = true;
        txt = GetComponent<Text>();
        this.score = 0;

    }

    void Update()
    {
        if (isRacing)
        {
            txt.text = score.ToString();
        }
    }
}
