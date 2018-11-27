using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    //player index.
    [SerializeField]
    private int m_playerIndex;

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
}
