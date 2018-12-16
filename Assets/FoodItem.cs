using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField]
    private float m_foodQuantity;
    [SerializeField]
    private float m_calories;
    [SerializeField]
    private int m_health;
    private Rigidbody2D m_rb2d;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //return food amount.
    public float GetFoodAmount ()
    { return m_foodQuantity; }

    public void SetAsMouthItem (Transform newParent)
    {
        this.gameObject.layer = LayerMask.NameToLayer("MouthItem");
        this.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        Collider2D selfCollider = this.GetComponent<Collider2D>();
        selfCollider.enabled = false;
        selfCollider.enabled = true;
        this.transform.SetParent(newParent);
        m_rb2d.gravityScale = 0f;
        
    }

    public void SetAsFoodItem ()
    {
        this.gameObject.layer = LayerMask.NameToLayer("FoodItem");
        this.GetComponentInChildren<SpriteRenderer>().sortingOrder = 6;
        Collider2D selfCollider = this.GetComponent<Collider2D>();
        selfCollider.enabled = false;
        selfCollider.enabled = true;
        this.transform.SetParent(null);
        m_rb2d.gravityScale = 1f;
    }
    //chew your food.
    public bool ChewFood(int chewDamage)
    {
        this.m_health -= chewDamage;
        return m_health <= 0;
    }
}
