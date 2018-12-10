﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HeadControl : MonoBehaviour
{
    private float m_xpos;
    private float m_ypos;
    [SerializeField]
    private float m_headAcceleration;
    private Rigidbody2D m_rb2d;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = this.GetComponent<Rigidbody2D>();
        m_xpos = this.transform.position.x;
        m_ypos = this.transform.position.y;
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
}