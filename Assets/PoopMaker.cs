using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoopMaker : MonoBehaviour
{
    [SerializeField] private GameObject m_majorPoo;
    [SerializeField] private GameObject m_minorPoo;
    [SerializeField] private float timeGap = 0.5f;
    [SerializeField] private float m_poopTorque;
    [SerializeField] private float m_poopThrust;
    private float timeStagger;
    private int m_poopCount;
    private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_fartClips;
    private int m_placing;
    private float m_calories;
    private Vector3 m_pooSize;
    [SerializeField] private Text m_topBarText;
    [SerializeField] private Text m_scoreField;
    private CanvasGroup m_topCanvas;
    private CanvasGroup m_bottomCanvas;
    private float m_displayedScore;
    private int m_playerIndex;


    // Start is called before the first frame update
    void Start()
    {
        m_poopCount = 0;
        timeStagger = 0f;
        m_placing = -1;
        m_audioSource = this.GetComponent<AudioSource>();
        m_topCanvas = m_topBarText.GetComponentInParent<CanvasGroup>();
        m_bottomCanvas = m_scoreField.GetComponentInParent<CanvasGroup>();
        m_displayedScore = 0f;
        m_playerIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //create the poo by triggering it after a delay.
    public void CreatePoo (float calories, int placing, int playerIndex)
    {
        m_playerIndex = playerIndex;
        m_placing = placing; m_calories = calories;
        m_pooSize = new Vector3(Mathf.Max(1, calories / 1000f), Mathf.Max(1, calories / 1000f));
        Invoke("DelayedPoo", 4.3f - m_placing);
    }
    //make the poos.
    private void DelayedPoo ()
    {
        m_audioSource.PlayOneShot(m_fartClips[m_placing]);
        //biggest
        GameObject newObj = Instantiate(m_majorPoo);
        newObj.SetActive(true);
        newObj.transform.position = this.transform.position;
        newObj.transform.SetParent(this.transform);
        newObj.GetComponent<Rigidbody2D>().AddTorque(Random.Range(m_poopTorque * -1, m_poopTorque));
        newObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(m_poopThrust * -1, m_poopThrust), 0));

        m_poopCount = Mathf.RoundToInt(m_calories / 200f);
        for (int p = 0; p < m_poopCount; p++)
        {
            Invoke("MakeMiniPoo", p * timeGap);
        }

        //show scores.
        m_bottomCanvas.alpha =1f;
        StartCoroutine(TallyScore(Mathf.Max(m_calories / 1000f, 0.1f)));

        if (m_placing == 1)
        {
            Invoke("DisplayWinner", 3f);
        }
    }
    //small poos.
    private void MakeMiniPoo ()
    {
        GameObject newObj = Instantiate(m_minorPoo);
        newObj.transform.position = this.transform.position;
        newObj.transform.SetParent(this.transform);
        newObj.SetActive(true);
        newObj.GetComponent<Rigidbody2D>().AddTorque(Random.Range(m_poopTorque * -2, m_poopTorque*2));
        newObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(m_poopThrust * -1, m_poopThrust), 0));
    }

    IEnumerator TallyScore (float score)
    {
        while (m_displayedScore < score)
        {
            m_displayedScore += 0.1f;
            m_scoreField.text = m_displayedScore.ToString("F2") + " lbs.";
        }
        yield return null;
    }

    //DISPLAY winner text.
    private void DisplayWinner ()
    {
        m_topCanvas.alpha = 1;
        m_topBarText.text = "Player " + m_playerIndex + " has made the most poo!";
        //start 3s count down to returning to player view.
        GameObject.FindWithTag("GameController").GetComponent<Game_Controller>().ReturnToPlayers();        
    }
  
}
