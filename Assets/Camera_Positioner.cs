using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Positioner : MonoBehaviour
{
    [SerializeField] private Vector3 m_PoopPosition;
    [SerializeField] private Vector3 m_PlayerPosition;
    [SerializeField] private float m_cameraSpeed;
    private bool m_gotoPoop;
    private bool m_returnHome;
    [SerializeField] private Game_Controller m_gameControl;
    private bool m_hasArrived;
    // Start is called before the first frame update
    void Start()
    {
        m_gotoPoop = false;
        m_returnHome = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gotoPoop)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, m_PoopPosition, m_cameraSpeed);
            if (this.transform.position.y <= m_PoopPosition.y)
            {
                m_gameControl.StartShitting();
                m_gotoPoop = false;
            }
        }
        else if (m_returnHome)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, m_PlayerPosition, m_cameraSpeed);
        }
    }

    public void GoToPoop ()
    {
        m_gotoPoop = true;
        m_returnHome = false;
    }

    public void GoToPlayers ()
    {
        m_gotoPoop = false;
        m_returnHome = true;
    }
}
