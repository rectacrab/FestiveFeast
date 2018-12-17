using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodWipe : MonoBehaviour
{
    [SerializeField] private float m_maxPosition;
    [SerializeField] private float m_speed;
    private Vector2 m_startingPos;
    private Rigidbody2D m_rb2d;
    private bool m_isWiping = false;

    // Start is called before the first frame update
    void Start()
    {
        m_startingPos = this.transform.position;
        m_rb2d = this.GetComponent<Rigidbody2D>();
        m_isWiping = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate ()
    {
        if (m_isWiping)
        {
            m_rb2d.velocity = new Vector2(m_speed, 0);
            if (this.transform.position.x >= m_maxPosition) { reset(); }
        }
    }

    public void removeFood ()
    {
        m_isWiping = true;
    }

    public void reset ()
    {
        this.transform.position = m_startingPos;
        m_rb2d.velocity = new Vector2(0, 0);
        m_isWiping = false;
    }
}
