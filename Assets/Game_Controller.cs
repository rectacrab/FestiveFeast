using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] private float m_gameLength;
    [SerializeField] private Text m_gameTimerDisplay;
    private CanvasGroup m_topCanvas;
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
    [SerializeField] private AudioClip m_DrumRoll;
    private AudioSource m_audioSource;
    private List<FoodStorage> m_rankings = new List<FoodStorage>();
    private bool[] m_countDowns = new bool[3];
    [SerializeField] private AudioClip m_countDownSound;
    private int m_countdownTracker = 0;
    private bool m_checkForRestart;
    [SerializeField] private CanvasGroup m_pressACanvas;
    [SerializeField] private AudioClip m_applause;

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
        m_rankings.Clear();
        for (int p = 0; p < m_countDowns.Length; p++) { m_countDowns[p] = false; }
        m_topCanvas = m_gameTimerDisplay.GetComponentInParent<CanvasGroup>();
        m_audioSource = this.GetComponent<AudioSource>();
        m_checkForRestart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            m_gameTimeLeft -= 1 * Time.deltaTime;
            m_gameTimerDisplay.text = Mathf.RoundToInt(m_gameTimeLeft).ToString();
            //display the timer.
            if (m_gameTimeLeft <= 5)
            {
                m_topCanvas.alpha = 1;
                if (m_gameTimeLeft <= 3f && !m_countDowns[0] || m_gameTimeLeft <= 2f && !m_countDowns[1] || m_gameTimeLeft <= 1f && !m_countDowns[2])
                {
                    
                    m_audioSource.volume = 1f;
                    m_audioSource.PlayOneShot(m_countDownSound);
                    m_countDowns[m_countdownTracker] = true;
                    m_countdownTracker += 1;
                }
            }
            else { m_topCanvas.alpha = 0f; }

            //change rounds.
            if (m_gameTimeLeft <= 0)
            {
                WipeTable();
                AdvanceRound();
                for (int p = 0; p < m_countDowns.Length; p++) { m_countDowns[p] = false; }
                m_countdownTracker = 0;
            }
        }
        else
        {
            if (m_checkForRestart)
            {
                if (Input.GetButtonDown("Go_P1") || Input.GetButtonDown("Go_P2") || Input.GetButtonDown("Go_P3") || Input.GetButtonDown("Go_P4"))
                {
                    RestartLevel();
                }
            }
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
        StartShitting();
    }

    public void StartShitting ()
    {
        //get winner/loser
        for (int p = 0; p < m_poopMakers.Length; p++)
        {
            m_playerFoodStorage[p].GetTotalCalories();
            m_rankings.Add(m_playerFoodStorage[p]);
        }

        m_rankings.OrderByDescending(x => x.m_totalCalories);
        Debug.Log("rankings count: " + m_rankings.Count);

        for (int p = 0; p < m_poopMakers.Length; p++)
        {
            Debug.Log("Attempting to reach: " + m_rankings[p].m_playerIndex);
            m_poopMakers[m_rankings[p].m_playerIndex-1].CreatePoo(m_rankings[p].GetTotalCalories(), p, m_rankings[p].m_playerIndex);
        }

        //play drum roll.
        m_audioSource.volume = 0.4f;
        m_audioSource.PlayOneShot(m_DrumRoll);

        //return after set period of time to players.
        //Invoke("ReturnToPlayers", 7f);
    }

    //return to player view.
    public void ReturnToPlayers ()
    {
        
        Invoke("moveCameraBacktoPlayers", 3f);
    }
    private void moveCameraBacktoPlayers ()
    {
        m_camPos.GoToPlayers();
        //display UI
        m_pressACanvas.alpha = 1f;
        //check for restart.
        m_checkForRestart = true;
        //play claps.
        m_audioSource.PlayOneShot(m_applause);
    }

    //restart the game
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
