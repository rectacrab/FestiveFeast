using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    //player index.
    [SerializeField]
    private int m_playerIndex;
    [SerializeField] private Color32 m_playerColour1;
    [SerializeField] private Color32 m_playerColour2;
    [SerializeField] private Color32 m_playerColourBase;
    [SerializeField] private SpriteRenderer[] m_playerhands;
    [SerializeField] private Material m_playerColourMaterial;
    [SerializeField] private LineRenderer[] m_armRenderers;
    [SerializeField] private SpriteRenderer m_bodySprite;
    [SerializeField] private SpriteRenderer m_headSprite;
    [SerializeField] private SpriteRenderer m_MouthTopSprite;
    [SerializeField] private SpriteRenderer m_MouthBotSprite;


    //private variables that store part of the control names.
    private string mControl_Trigger = "Triggers_P";
    private string mControl_RS_Horizontal = "RS_Horizontal_P";
    private string mControl_RS_Vertical = "RS_Vertical_P";
    private string mControl_LS_Vertical = "LS_Vertical_P";
    private string mControl_LS_Horizontal = "LS_Horizontal_P";
    private string mControl_Go = "Go_P";
    private string mControl_Stop = "Stop_P";

    // Start is called before the first frame update
    void Start()
    {
        if (m_playerhands != null)
        {
            m_playerhands[0].color = m_playerColour1;
            m_playerhands[1].color = m_playerColour2;
            m_bodySprite.color = m_playerColourBase;
            m_headSprite.color = m_playerColourBase;
            m_MouthTopSprite.color = m_playerColourBase;
            m_MouthBotSprite.color = m_playerColourBase;
        }
        foreach(LineRenderer render in m_armRenderers)
        {
            render.material = m_playerColourMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Get_Trigger()
    {
        return mControl_Trigger + m_playerIndex;
    }
    public string Get_RSHorizontal()
    {
        return mControl_RS_Horizontal + m_playerIndex;
    }
    public string Get_RSVertical()
    {
        return mControl_RS_Vertical + m_playerIndex;
    }
    public string Get_LSVertical()
    {
        return mControl_LS_Vertical + m_playerIndex;
    }
    public string Get_LSHorizontal()
    {
        return mControl_LS_Horizontal + m_playerIndex;
    }
    public string Get_PlayerGo()
    {
        return mControl_Go + m_playerIndex;
    }

    public Color32 GetBaseColour ()
    {
        return m_playerColourBase;
    }

    public int GetPlayerIndex ()
    {
        return m_playerIndex;
    }
}
