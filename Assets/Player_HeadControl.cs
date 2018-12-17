using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HeadControl : MonoBehaviour
{
    private float m_xpos;
    private float m_ypos;
    [SerializeField]
    private float m_headAcceleration;
    private Rigidbody2D m_rb2d;
    private Player_MouthHole m_playerMouth;
    private Animator m_headAnimator;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = this.GetComponent<Rigidbody2D>();
        m_xpos = this.transform.position.x;
        m_ypos = this.transform.position.y;
        m_playerMouth = this.GetComponentInChildren<Player_MouthHole>();
        m_headAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate ()
    {
        if (this.transform.position.y > m_ypos+ m_headAcceleration || this.transform.position.y < m_ypos - m_headAcceleration || this.transform.position.x > m_xpos+m_headAcceleration || this.transform.position.x < m_xpos - m_headAcceleration)
        {
            MoveTowardsHome();
        }
    }
    
    private void MoveTowardsHome ()
    {
        m_rb2d.MovePosition(Vector2.Lerp(this.transform.position, new Vector2(m_xpos, m_ypos), m_headAcceleration));
    }

    //punched in head.
    private void OnCollisionEnter2D (Collision2D collider)
    {
        m_playerMouth.DropFoodItems();
    }

    public void EndVomiting ()
    {
        m_headAnimator.SetBool("isVomiting", false);
        m_playerMouth.SetVomiting(false);
    }

    //start vomiting.
    public void StartVomiting()
    {
        m_headAnimator.SetBool("isVomiting", true);
        m_playerMouth.SetVomiting(true);
    }
}
