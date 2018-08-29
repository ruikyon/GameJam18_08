using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public static int level = 0, finalScore;

    private int score;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    public void AddScore(int point)
    {
        score += point;
    }

    public int GetScore()
    {
        return score;
    }
}
