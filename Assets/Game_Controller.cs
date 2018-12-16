using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] private float m_gameLength;
    [SerializeField] private Text m_gameTimerDisplay;
    private bool gameActive = false;
    private float m_gameTimeLeft;
    [SerializeField] private GameObject[] m_playerObjects;
    private Player_PointsTracker[] m_playerPoints = new Player_PointsTracker[4];
    private PlayerInfo[] m_playerInfo = new PlayerInfo[4];

    // Start is called before the first frame update
    void Start()
    {
        m_gameTimeLeft = m_gameLength;
        gameActive = true;
        for (int p=0; p<m_playerObjects.Length; p++)
        {
            m_playerPoints[p] = m_playerObjects[p].GetComponent<Player_PointsTracker>();
            m_playerInfo[p] = m_playerObjects[p].GetComponent<PlayerInfo>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            m_gameTimeLeft -= 1 * Time.deltaTime;
            m_gameTimerDisplay.text = m_gameTimeLeft.ToString("F2");
        }
        else
        {

        }
    }
}
