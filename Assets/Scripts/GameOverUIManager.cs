using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    private Button _button;
    private Text _text;

	void Start () {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(ClickPlayAgain);

	    _text = GetComponentInChildren<Text>();
	}

    public void ClickPlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetHighScoreText(string score, bool didWin)
    {
        print(_text.text);
        if (didWin)
        {
            _text.text = "You Win! \n" +
                         "High Score: " + score;
        }
        else
        {
            _text.text = "Game Over! \n" +
                         "High Score: " + score;
        }
        print(_text.text);
    }
}
