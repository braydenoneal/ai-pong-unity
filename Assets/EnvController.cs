using UnityEngine;
using Unity.MLAgents;
using TMPro;

public class EnvController : MonoBehaviour
{
    public GameObject ball;
    public BallScript ballScript;
    public Agent leftPaddle;
    public Agent rightPaddle;
    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;

    public int leftScore = 0;
    public int rightScore = 0;

    void ResetEnvironment()
    {
        ballScript.Reset();
        leftPaddle.EndEpisode();
        rightPaddle.EndEpisode();
    }

    void FixedUpdate()
    {
        if (ball.transform.localPosition.x < 0)
        {
            leftPaddle.SetReward(-1.0f);
            rightPaddle.SetReward(1.0f);
            rightScore++;
            if (rightScoreText != null)
            {
                rightScoreText.text = rightScore.ToString();
            }
            ResetEnvironment();
        }
        else if (ball.transform.localPosition.x > 16)
        {
            leftPaddle.SetReward(1.0f);
            rightPaddle.SetReward(-1.0f);
            leftScore++;
            if (leftScoreText != null)
            {
                leftScoreText.text = leftScore.ToString();
            }
            ResetEnvironment();
        }
    }
}
