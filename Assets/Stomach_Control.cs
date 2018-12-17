using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomach_Control : MonoBehaviour
{
    [SerializeField] private Collider2D m_AcidCollider;
    private List<Stomach_Item> stomachObjects = new List<Stomach_Item>();
    [SerializeField] private GameObject m_StartingPoint;
    [SerializeField] private GameObject m_stomachItemPrefab;
    [SerializeField] private AudioClip[] m_vomitingSounds;
    private AudioSource m_audioSource;
    private Player_HeadControl m_playerHead;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = this.GetComponentInParent<AudioSource>();
        m_playerHead = GetComponentInParent<Player_HeadControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D collidingObject)
    {
        //adding food
        if (collidingObject.gameObject.GetComponent<player_ThroatObject>())
        {
            Debug.Log("Creating a food item");
            CreateStomachItem(collidingObject.gameObject.GetComponent<player_ThroatObject>().getOriginalFoodItem());
            Destroy(collidingObject.gameObject);
        }

        //vomiting.
        if (collidingObject.gameObject.GetComponent<Stomach_Item>())
        {
            Debug.Log("VOMIT TRIGGERED");
            PlayVom();
        }
    }

    public void CreateStomachItem(FoodItem baseItem)
    {
        GameObject newObject = Instantiate(m_stomachItemPrefab);
        newObject.transform.localScale = new Vector2(0.8f, 0.8f);
        newObject.transform.position = m_StartingPoint.transform.position;
        newObject.transform.SetParent(m_StartingPoint.transform);
        newObject.GetComponent<Stomach_Item>().DropStomachFood(baseItem);
    }

    private void PlayVom ()
    {
        m_audioSource.PlayOneShot(m_vomitingSounds[Random.Range(0, m_vomitingSounds.Length)]);
        m_playerHead.StartVomiting();
    }
}
