using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MouthHole : MonoBehaviour
{
    private PlayerInfo m_playerInfo;
    private bool m_mouthOpen = false;
    [SerializeField]
    private float m_maxOpenSize = 0.55f;
    private float m_topLipPosition;
    private float m_botLipPosition;
    [SerializeField]
    private float m_horizontalScale;
    [SerializeField]
    private float m_mouthSpeed = 0.1f;
    [SerializeField]
    private float m_mouthOpenCloseCrossover = 0.4f;
    [SerializeField]
    private Transform m_lipTop;
    [SerializeField]
    private Transform m_lipBottom;
    [SerializeField]
    private float m_MaxFoodInMouth = 5f;
    private float m_currentFoodInMouth = 0f;
    [SerializeField]
    private GameObject m_mouthContainer;
    [SerializeField]
    private float m_contactFoodEjectionVelocity;
    [SerializeField]
    private int m_chewDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_topLipPosition = m_lipTop.position.y;
        m_botLipPosition = m_lipBottom.position.y;
        m_playerInfo = this.GetComponentInParent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate ()
    {
        //open your god damn mouth.
        m_lipTop.transform.position = new Vector2(m_lipTop.position.x, m_lipTop.position.y);

        this.transform.localScale = new Vector2(m_horizontalScale, Mathf.Max(0.01f, Mathf.Lerp(this.transform.localScale.y, Mathf.Abs(Input.GetAxis(m_playerInfo.Get_Trigger())) * m_maxOpenSize, m_mouthSpeed)));
        SetMouthStatus();
    }


    //check whether mouth is open or closed.
    private void SetMouthStatus ()
    {
        if (this.transform.localScale.y > m_mouthOpenCloseCrossover*m_maxOpenSize)
        {
            if (m_mouthOpen == false)
            {
                
            }
            m_mouthOpen = true;
        }
        else
        {
            if (m_mouthOpen == true)
            {
                Debug.Log("chewing");
                ChewFood();
            }
            m_mouthOpen = false;
        }
    }

    //deposit food in mouth.
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.GetComponent<FoodItem>())
        {
            if (m_mouthOpen)
            {
                FoodItem itam = collision.gameObject.GetComponent<FoodItem>();
                if (m_currentFoodInMouth + itam.GetFoodAmount() <= m_MaxFoodInMouth)
                {
                    Debug.Log("Food goes in the mouth");
                    itam.SetAsMouthItem(m_mouthContainer.transform);
                    m_currentFoodInMouth += itam.GetFoodAmount();
                    itam.transform.position = m_mouthContainer.transform.position;
                    
                }
                
            }
            else
            {
                Debug.Log("Mouth Closed");
            }
        }
    }

    //drop food items when punched in the head.
    public void DropFoodItems ()
    {
        for (int p =0; p < m_mouthContainer.transform.childCount; p++)
        {
            FoodItem foods = m_mouthContainer.transform.GetChild(p).GetComponent<FoodItem>();
            foods.SetAsFoodItem();
            foods.gameObject.transform.SetParent(null);
            foods.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(m_contactFoodEjectionVelocity * -1, m_contactFoodEjectionVelocity), m_contactFoodEjectionVelocity));
        }
    }

    //method to start chewing the food.
    private void ChewFood ()
    {
        List<GameObject> destructionList = new List<GameObject>();
        for (int p = 0; p < m_mouthContainer.transform.childCount; p++)
        {
            bool isReady = m_mouthContainer.transform.GetChild(p).GetComponent<FoodItem>().ChewFood(m_chewDamage);
            Debug.Log("is ready to be swallowed: " + isReady);
            if (isReady) { Destroy(m_mouthContainer.transform.GetChild(p).gameObject); }
        }

        //kill it.
        foreach (GameObject obj in destructionList)
        {
            destructionList.Remove(obj);
            Destroy(obj);
        }
    }
}
