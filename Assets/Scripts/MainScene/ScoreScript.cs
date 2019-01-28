using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public enum Score {
        AiScore, PlayerScore
    }

    public Text AiScoreTxt, playerScoreTxt;

    public UiManager uiManager;

    public int MaxScore;

    #region Scores

    private int aiScore, playerScore;

    private int AiScore {
        get { return aiScore; }
        set {
            aiScore = value;
            if (value == MaxScore) {
                uiManager.ShowRestartCanvas(true);
            }
        }
    }

    private int PlayerScore {
        get { return playerScore; }
        set {
            playerScore = value;
            if (value == MaxScore) {
                uiManager.ShowRestartCanvas(false);
            }
        }
    }

    #endregion

    public void Increment(Score whichScore) {
        if (whichScore == Score.AiScore)
        {
            AiScoreTxt.text = (++AiScore).ToString();
        }
        else {
            playerScoreTxt.text = (++PlayerScore).ToString();
        }
    }

    public void ResetScore() {
        AiScore = PlayerScore = 0;
        AiScoreTxt.text = playerScoreTxt.text = "0";
    }
	
}
