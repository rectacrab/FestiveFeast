using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomach_Item : MonoBehaviour
{
    private FoodItem m_originalItem;
    private float m_digestionTime;
    private float m_currentDigestion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //drop this into the stomach area.
    public void DropStomachFood (FoodItem original)
    {
        Debug.Log("Original: " + original);
        m_originalItem = original;
        m_digestionTime = original.GetHealth();
        this.GetComponentInChildren<SpriteRenderer>().sprite = m_originalItem.GetSprite();
    }

    public bool digestItem (float digestionRate)
    {
        m_currentDigestion += digestionRate;
        if (m_currentDigestion >= m_digestionTime)
        {
            return true;
        }
        else
        {
            return false;  
        }
    }
}
