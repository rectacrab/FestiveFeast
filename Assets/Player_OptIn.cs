using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_OptIn : MonoBehaviour
{
    [SerializeField] private GameObject m_joinPrompt;
    [SerializeField] private GameObject m_hasJoined;
    [SerializeField] private int m_playerTarget;
    private bool m_hasOptedIn = false;
    [SerializeField] private Text m_PlayerField;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerField.text = "Player " + m_playerTarget.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown(("Go_P"+ m_playerTarget.ToString())))
        {
            m_hasOptedIn = true;
            m_joinPrompt.SetActive(false);
            m_hasJoined.SetActive(true);
        }
    }
    //return whether this player has joined.
    public bool CheckIfJoined ()
    {
        return m_hasOptedIn;
    }
}
