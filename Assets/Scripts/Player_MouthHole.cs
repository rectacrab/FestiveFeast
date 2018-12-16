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
    [SerializeField] private AudioClip[] m_ChewingSounds;
    private AudioSource m_audioSource;
    private ParticleSystem m_partSys;

    // Start is called before the first frame update
    void Awake ()
    {
        m_playerInfo = this.GetComponentInParent<PlayerInfo>();
        m_audioSource = this.GetComponentInParent<AudioSource>();
        m_partSys = this.GetComponent<ParticleSystem>();
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
        if (Mathf.Abs(Input.GetAxis(m_playerInfo.Get_Trigger())) > m_mouthOpenCloseCrossover)
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
                    itam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    itam.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
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
            foods.gameObject.transform.localScale = new Vector3(1, 1, 1);
            foods.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(m_contactFoodEjectionVelocity * -1, m_contactFoodEjectionVelocity), m_contactFoodEjectionVelocity));
        }
        m_currentFoodInMouth = 0f;
    }

    //method to start chewing the food.
    private void ChewFood ()
    {
        List<GameObject> destructionList = new List<GameObject>();
        //play audio if food present.
        if (m_mouthContainer.transform.childCount > 0)
        {
            m_audioSource.PlayOneShot(m_ChewingSounds[Random.Range(0, m_ChewingSounds.Length)]);
            m_partSys.Play();
        }
        //trawl throuhg mouth items.
        for (int p = 0; p < m_mouthContainer.transform.childCount; p++)
        {
            bool isReady = m_mouthContainer.transform.GetChild(p).GetComponent<FoodItem>().ChewFood(m_chewDamage);
            Debug.Log("is ready to be swallowed: " + isReady);
            if (isReady)
            {
                m_currentFoodInMouth -= m_mouthContainer.transform.GetChild(p).GetComponent<FoodItem>().GetFoodAmount();
                
                //create swallow object.
                GameObject swolObject = Instantiate(m_swallowPrefab);
                swolObject.transform.position = this.transform.position;
                swolObject.transform.SetParent(m_swallowContainer.transform);
                swolObject.SetActive(true);
                swolObject.GetComponent<player_ThroatObject>().setOriginalFoodItem(m_mouthContainer.transform.GetChild(p).gameObject.GetComponent<FoodItem>());

                //destroy mouth version
                Destroy(m_mouthContainer.transform.GetChild(p).gameObject);
            }

        }
    }
}
