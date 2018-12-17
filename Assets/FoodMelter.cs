using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMelter : MonoBehaviour
{
    [SerializeField] private float m_FoodMeltRate = 0.1f;
    [SerializeField] private Stomach_Control m_stomachControl;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //find all stuff you're touching.
    public void OnTriggerStay2D (Collider2D collidingObject)
    {
        if (collidingObject.gameObject.GetComponent<Stomach_Item>())
        {
            bool isReady = collidingObject.gameObject.GetComponent<Stomach_Item>().digestItem(m_FoodMeltRate);
            if (isReady) { m_stomachControl.FlagFoodReady(collidingObject.gameObject); }
        }
    }
    
}
