using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomach_Control : MonoBehaviour
{
    [SerializeField] private Collider2D m_AcidCollider;
    private List<Stomach_Item> m_stomachObjects = new List<Stomach_Item>();
    [SerializeField] private GameObject m_StartingPoint;
    [SerializeField] private GameObject m_stomachItemPrefab;
    [SerializeField] private AudioClip[] m_vomitingSounds;
    [SerializeField] private GameObject m_headObject;
    private GameObject m_mouthLocation;
    private AudioSource m_audioSource;
    private Player_HeadControl m_playerHead;
    [SerializeField] private GameObject m_vomitPrefab;
    [SerializeField] private FoodStorage m_foodStorage;
    [SerializeField] private float m_vomitProjectionRange;
    private List<GameObject> m_ReadyDigestObjects = new List<GameObject>();
    [SerializeField] private GameObject m_ButtonReminder;
    private bool m_digestReady;
    private PlayerInfo m_playerInfo;
    [SerializeField] private AudioClip m_digestionNoise;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = this.GetComponentInParent<AudioSource>();
        m_playerHead = m_headObject.GetComponent<Player_HeadControl>();
        m_mouthLocation = m_headObject.transform.GetChild(1).gameObject;
        m_digestReady = false;
        m_playerInfo = this.GetComponentInParent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_digestReady)
        {
            if (Input.GetButtonDown(m_playerInfo.Get_PlayerGo()))
            {
                DigestFood();
            }
        }
    }

    void OnTriggerEnter2D (Collider2D collidingObject)
    {
        //adding food
        if (collidingObject.gameObject.GetComponent<player_ThroatObject>())
        {
            Debug.Log("Creating a food item");
            CreateStomachItem(collidingObject.gameObject.GetComponent<player_ThroatObject>().getOriginalFoodItem());
            //m_foodStorage.StoreChewedFood(collidingObject.gameObject);
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
        m_stomachObjects.Add(newObject.GetComponent<Stomach_Item>());
        if (!m_audioSource.isPlaying)
        {
            m_audioSource.clip = m_digestionNoise;
            m_audioSource.volume = 0.7f;
            m_audioSource.Play();
        }
    }

    private void PlayVom ()
    {
        if (m_audioSource.clip == m_digestionNoise)
        {
            m_audioSource.Stop();
            m_audioSource.clip = null;
            m_audioSource.volume = 1f;
        }
        m_audioSource.PlayOneShot(m_vomitingSounds[Random.Range(0, m_vomitingSounds.Length)]);
        m_playerHead.StartVomiting();
        VomitFood();

    }


    //flag an object as ready.
    public void FlagFoodReady (GameObject objectReady)
    {
        if (!m_ReadyDigestObjects.Contains(objectReady))
        {
            m_ReadyDigestObjects.Add(objectReady);
            //flag UI.
            m_ButtonReminder.SetActive(true);
            m_digestReady = true;
        }

    }

    //digest objects.
    public void DigestFood ()
    {
        foreach (GameObject obj in m_ReadyDigestObjects)
        {
            m_stomachObjects.Remove(m_stomachObjects.Find(target => target.gameObject == obj));
            m_foodStorage.StoreDigestedFood(obj.GetComponent<Stomach_Item>().GetFoodItem());
            Destroy(obj);
        }
        m_ReadyDigestObjects.Clear();
        m_ButtonReminder.SetActive(false);
        m_digestReady = false;
        
        //stop playing digest noise as stomach may be empty.
        if (m_stomachObjects.Count <= 0)
        {
            m_audioSource.Stop();
        }
    }

    //method to actually re-create food items on the table.
    public void VomitFood ()
    {
        Debug.Log("Vomit Food triggered, recreating objects");
        foreach (Stomach_Item item in m_stomachObjects)
        {
            GameObject returningObj = m_foodStorage.RemoveFoodFromStomach(item.GetFoodItem());
            returningObj.transform.position = m_mouthLocation.transform.position;
            returningObj.GetComponent<FoodItem>().SetAsFoodItem();

            //impulse in direction.
            returningObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(m_vomitProjectionRange * -1, m_vomitProjectionRange), Random.Range(m_vomitProjectionRange * -1, m_vomitProjectionRange)));

            //kill stomach item
            Destroy(item.gameObject);
        }

        m_stomachObjects.Clear();
    }
}
