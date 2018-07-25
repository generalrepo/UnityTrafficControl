using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

    public string scoreText;
    public int score;

    // Use this for initialization
	void Start () {
        score = 0;
        UpdateScore();
	}
	
    public void AddScore(int NewScoreValue)
    {
        score += NewScoreValue;
        UpdateScore();
    }
	// Update is called once per frame
	void UpdateScore () {
        score = score + 5;
	}
}
