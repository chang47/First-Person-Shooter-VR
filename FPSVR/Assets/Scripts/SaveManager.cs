using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string _highScoreKey = "highscore";

    public void SaveHighScore(float score)
    {
        PlayerPrefs.SetFloat(_highScoreKey, score);
    }

    public float LoadHighScore()
    {
        if (PlayerPrefs.HasKey(_highScoreKey))
        {
            return PlayerPrefs.GetFloat(_highScoreKey);
        }
        return 99999999999;
    }
}
