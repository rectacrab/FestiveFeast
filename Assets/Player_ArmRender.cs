using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ArmRender : MonoBehaviour
{
    private LineRenderer m_lineRender;
    [SerializeField] private GameObject[] m_armjoints;

    // Start is called before the first frame update
    void Start()
    {
        m_lineRender = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_lineRender.SetPosition(0, this.transform.position);
        m_lineRender.SetPosition(1, m_armjoints[0].transform.position);
        m_lineRender.SetPosition(2, m_armjoints[1].transform.position);
        m_lineRender.SetPosition(3, m_armjoints[2].transform.position);
    }
}
