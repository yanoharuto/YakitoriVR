using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private int mScore;
    private const int mCorrectAnswerScore = 30;
    private void Start()
    {
        mScore = 0;
    }
    public void OnDecideScore(bool Judge)
    {
        if (Judge)
        {
            
            mScore += mCorrectAnswerScore;
        }
        Debug.Log(mScore);
    }
}
