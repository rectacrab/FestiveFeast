using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HandControl : MonoBehaviour
{
    [SerializeField] private bool m_LeftHand;
    private PlayerInfo m_playerInfo;
    [SerializeField]
    private float m_velocityMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        m_playerInfo = this.GetComponentInParent<PlayerInfo>();
        if (m_LeftHand) { }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate ()
    {
        //check for input for hand movement.
        switch (m_LeftHand)
        {
            case true:
                {
                    if (Mathf.Abs(Input.GetAxis(m_playerInfo.Get_LSHorizontal())) > 0.05f || Mathf.Abs(Input.GetAxis(m_playerInfo.Get_LSVertical())) > 0.05f)
                    {
                        MoveHand();
                    }
                }; break;
            case false:
            default:
                {
                    if (Mathf.Abs(Input.GetAxis(m_playerInfo.Get_RSHorizontal())) > 0.05f || Mathf.Abs(Input.GetAxis(m_playerInfo.Get_RSVertical())) > 0.05f)
                    {
                        MoveHand();
                    }
                }; break;
        }
    }

    //move the hand around on screen.
    private void MoveHand ()
    {
        switch (m_LeftHand)
        {
            case true:
                {
                    Vector2 posMod = new Vector2(this.transform.parent.position.x + Input.GetAxis(m_playerInfo.Get_LSHorizontal()) * m_velocityMagnitude, this.transform.parent.position.y + Input.GetAxis(m_playerInfo.Get_LSVertical()) * m_velocityMagnitude);
                    this.transform.position = posMod;
                }; break;
            case false:
            default:
                {
                    Vector2 posMod = new Vector2(this.transform.parent.position.x + Input.GetAxis(m_playerInfo.Get_RSHorizontal()) * m_velocityMagnitude, this.transform.parent.position.y + Input.GetAxis(m_playerInfo.Get_RSVertical()) * m_velocityMagnitude);
                    this.transform.position = posMod;
                }; break;
        }
    }

    /*
    //collision checking.
    public void OnTriggerEnter2D (Collider2D collider)
    {
        Debug.Log("triggering");
        if (collider.gameObject.GetComponent<Player_MouthHole>())
        {
            Debug.Log("Hitting the mouth hole");
        }
    }

    //why is this not working?
    public void OnCollisionEnter2D (Collision2D collider)
    {
        Debug.Log("Colliding");
        if (collider.gameObject.GetComponent<Player_MouthHole>())
        {
            Debug.Log("Hitting the mouth hole");
        }
    } */
}
