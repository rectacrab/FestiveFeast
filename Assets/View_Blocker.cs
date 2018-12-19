using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Blocker : MonoBehaviour
{
    [SerializeField] private Vector3 m_targetPosition;
    [SerializeField] private float m_travelSpeed;
    [SerializeField] private float m_accuracy;
    [SerializeField] private float m_delayReveal;
    private Vector3 m_restPosition;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        m_restPosition = this.transform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            Debug.Log("Moving Plater!");
            this.transform.position = Vector3.Lerp(this.transform.position, m_targetPosition, m_travelSpeed);
            if (this.transform.position.x > m_targetPosition.x- m_accuracy && this.transform.position.x < m_targetPosition.x+m_accuracy)
            {
                Invoke("ReturnHome", m_delayReveal);
                GameObject.FindWithTag("GameController").GetComponent<Game_Controller>().TriggerFoodSpawn(this.transform.transform.position);
                isMoving = false;
            }
        }
    }

    //set moving to true.
    public void goToFoodLocation ()
    {
        isMoving = true;
        Debug.Log("Moiving to location: " + m_targetPosition.x);
    }

    //go back.
    public void ReturnHome ()
    {
        this.transform.position = m_restPosition;
        isMoving = false;
    }
}
