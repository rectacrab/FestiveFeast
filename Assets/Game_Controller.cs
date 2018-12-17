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
    [SerializeField] private int m_servingRounds = 3;
    private int m_currentRound = 0;
    [SerializeField] private GameObject[] m_foodRounds;
    [SerializeField] private GameObject[] m_FoodSpawningPoints;
    [SerializeField] private GameObject m_ViewBlocker;
    [SerializeField] private FoodWipe m_foodWipe;
    [SerializeField] private Camera_Positioner m_camPos;
    [SerializeField] private PoopMaker[] m_poopMakers;
    [SerializeField] private FoodStorage[] m_playerFoodStorage;

    // Start is called before the first frame update
    void Start()
    {
        m_currentRound = 0;
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
            if (m_gameTimeLeft <= 0)
            {
                WipeTable();
                AdvanceRound();
            }
        }
        else
        {

        }
    }

    //wipe the table clean.
    private void WipeTable ()
    {
        m_foodWipe.removeFood();
    }

    private void AdvanceRound ()
    {
        m_currentRound += 1;
        if (m_currentRound >= m_servingRounds)
        {
            EndGame();
        }
        else
        {
            m_gameTimeLeft = m_gameLength;
        }
    }

    //view poop.
    private void EndGame ()
    {
        gameActive = false;
        m_camPos.GoToPoop();
        
    }

    public void StartShitting ()
    {
        for (int p = 0; p < m_poopMakers.Length; p++)
        {
            m_poopMakers[p].CreatePoo(m_playerFoodStorage[p].GetTotalCalories());
        }
    }

    //return to player view.
    private void ReturnToPlayers ()
    {
        m_camPos.GoToPlayers();
    }
}
