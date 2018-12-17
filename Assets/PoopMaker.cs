using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopMaker : MonoBehaviour
{
    [SerializeField] private GameObject m_majorPoo;
    [SerializeField] private GameObject m_minorPoo;
    [SerializeField] private float timeGap = 0.5f;
    [SerializeField] private float m_poopTorque;
    private float timeStagger;
    private int m_poopCount;

    // Start is called before the first frame update
    void Start()
    {
        m_poopCount = 0;
        timeStagger = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePoo (float calories)
    {
        //biggest
        GameObject newObj = Instantiate(m_majorPoo);
        newObj.transform.SetParent(this.transform);
        newObj.SetActive(true);
        newObj.transform.localScale = new Vector3(Mathf.Max(1, calories / 100f), Mathf.Max(1, calories / 100f));
        newObj.transform.position = this.transform.position;
        newObj.GetComponent<Rigidbody2D>().AddTorque(Random.Range(m_poopTorque * -1, m_poopTorque));

        m_poopCount = Mathf.RoundToInt(calories / 200f);
        for (int p =0; p < m_poopCount; p++)
        {
            Invoke("MakeMiniPoo", p * timeGap);
        }
    }

    private void MakeMiniPoo ()
    {
        GameObject newObj = Instantiate(m_minorPoo);
        newObj.transform.SetParent(this.transform);
        newObj.SetActive(true);
    }
}
