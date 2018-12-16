using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_Starter : MonoBehaviour
{
    private Image m_CircleCounter;
    [SerializeField] private Player_OptIn[] m_playerOptIns;
    private bool m_gameStart;
    [SerializeField] private float m_imageFillRate;
    // Start is called before the first frame update
    void Start()
    {
        m_CircleCounter = GetComponentInChildren<Image>();
        m_gameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gameStart)
        {
            bool allready = true;
            for (int p = 0; p < m_playerOptIns.Length; p++)
            {
                if (!m_playerOptIns[p].CheckIfJoined())
                {
                    allready = false;
                    break;
                }
            }

            if (allready) { m_gameStart = true; }
        }
        
        //kick it off.
        if (m_gameStart)
        {
            if (m_CircleCounter.fillAmount < 1)
            {
                m_CircleCounter.fillAmount += m_imageFillRate;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
        
    }
}
