using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text Score;

    private string _time;
    private bool _gameOver;
    private float _score;

	void Start ()
	{
	    _time = "";
	    _gameOver = false;
	    _score = 9999999999;
	}

    void Update()
    {
        if (!_gameOver)
        {
            UpdateTime();
        }
    }

    private void UpdateTime()
    {
        _score = Time.time;
        _time = ScoreManager.GetScoreFormatting(Time.time);
        Score.text = _time;
    }

    public void GameOver()
    {
        _gameOver = true;
    }

    public float GetScore()
    {
        return _score;
    }

    // we can call this function anywhere we want, we don't need to have an instance of this class
    public static string GetScoreFormatting(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        float miliseconds = time * 100;
        miliseconds = miliseconds % 100;
        return string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, miliseconds);
    }
}
