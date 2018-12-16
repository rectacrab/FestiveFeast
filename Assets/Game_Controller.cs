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

    // Start is called before the first frame update
    void Start()
    {
        m_gameTimeLeft = m_gameLength;
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            m_gameTimeLeft -= 1 * Time.deltaTime;
            m_gameTimerDisplay.text = m_gameTimeLeft.ToString("F2");
        }
    }
}
