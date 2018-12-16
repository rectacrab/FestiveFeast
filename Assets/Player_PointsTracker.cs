using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_PointsTracker : MonoBehaviour
{
    [SerializeField] private Text m_scoreField;
    private int m_playerScore;

    // Start is called before the first frame update
    void Start()
    {
        m_playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //add some score
    public void AddScore (int amount)
    {
        m_playerScore += amount;
        m_scoreField.text = m_playerScore.ToString();
    }

    //get the score.
    public int GetScore ()
    {
        return m_playerScore;
    }
}
