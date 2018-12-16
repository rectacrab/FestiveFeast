using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_ThroatObject : MonoBehaviour
{
    [SerializeField] private float m_travelSpeed;
    [SerializeField] private Vector3 m_endPosition;
    private Vector3 m_startPos;
    private FoodItem m_originalItem;
    // Start is called before the first frame update
    void Start()
    {
        m_startPos = this.transform.position;
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        renderer.color = this.transform.parent.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += new Vector3(0, m_travelSpeed*-1,0);
    }

    //set and store the food item.
    public void setOriginalFoodItem (FoodItem food)
    {
        m_originalItem = food;
    }
    public FoodItem getOriginalFoodItem ()
    {
        return m_originalItem;
    }
}
