using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MouthHole : MonoBehaviour
{
    private PlayerInfo m_playerInfo;
    private bool m_mouthOpen = false;
    [SerializeField]
    private float m_maxOpenSize = 0.55f;
    [SerializeField]
    private float m_topLipPosition;
    [SerializeField]
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
    [SerializeField] private GameObject m_swallowPrefab;
    [SerializeField] private GameObject m_swallowContainer;

    // Start is called before the first frame update
    void Awake ()
    {
        m_playerInfo = this.GetComponentInParent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate ()
    {
        //open your god damn mouth.
        m_lipTop.position = new Vector2(this.transform.parent.transform.position.x, this.transform.position.y + Mathf.Lerp(this.transform.position.y, m_topLipPosition + Mathf.Abs(Input.GetAxis(m_playerInfo.Get_Trigger())) * m_maxOpenSize, m_mouthSpeed));
        m_lipBottom.position = new Vector2(this.transform.parent.transform.position.x, m_botLipPosition - Mathf.Lerp(m_botLipPosition, Mathf.Abs(Input.GetAxis(m_playerInfo.Get_Trigger())) * m_maxOpenSize, m_mouthSpeed));
        SetMouthStatus();
    }


    //check whether mouth is open or closed.
    private void SetMouthStatus ()
    {
        if (m_lipTop.position.y > m_mouthOpenCloseCrossover)
        {
            if (m_mouthOpen == false)
            {
                Debug.Log("Mouth now open");
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
                else
                {
                    Debug.Log("CHEW YOUR FOOD!");
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
            if (isReady)
            {
                m_currentFoodInMouth -= m_mouthContainer.transform.GetChild(p).GetComponent<FoodItem>().GetFoodAmount();
                Destroy(m_mouthContainer.transform.GetChild(p).gameObject);
                //create swallow object.
                GameObject swolObject = Instantiate(m_swallowPrefab);
                swolObject.transform.position = this.transform.position;
                swolObject.transform.SetParent(m_swallowContainer.transform);
                swolObject.SetActive(true);
            }

        }
    }
}
