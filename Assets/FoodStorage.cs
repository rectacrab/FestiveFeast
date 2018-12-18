using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : MonoBehaviour
{
    private List<GameObject> m_swallowedFood = new List<GameObject>();
    private List<GameObject> m_digestedFood = new List<GameObject>();
    public float m_totalCalories;
    public int m_playerIndex; 
    // Start is called before the first frame update
    void Start()
    {
        m_playerIndex = this.GetComponentInParent<PlayerInfo>().GetPlayerIndex();
        m_totalCalories = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //stored after chewing.
    public void StoreChewedFood (GameObject newObject)
    {
        m_swallowedFood.Add(newObject);
        newObject.transform.position = this.transform.position;
        newObject.transform.SetParent(this.transform);
        newObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        newObject.GetComponent<Rigidbody2D>().isKinematic  = false;
    }

    //stored for scoring.
    public void StoreDigestedFood (FoodItem targetItem)
    {
        GameObject targetObject = m_swallowedFood.Find(item => item.GetComponent<FoodItem>() == targetItem);
        Debug.Log("found item: " + targetObject);
        m_swallowedFood.Remove(targetObject);
        m_digestedFood.Add(targetObject);
    }
    //vomiting process.
    public GameObject RemoveFoodFromStomach (FoodItem targetItem)
    {
        GameObject target = m_swallowedFood.Find(item => item.GetComponent<FoodItem>() == targetItem);
        m_swallowedFood.Remove(target);
        target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        target.GetComponent<Rigidbody2D>().gravityScale = 1f;
        target.transform.SetParent(null);
        return target;
    }

    //get the total calories.
    public float GetTotalCalories ()
    {
        float cals = 0f;
        foreach (GameObject foodObj in m_digestedFood)
        {
            cals += foodObj.GetComponent<FoodItem>().GetCalories();
        }
        Debug.Log("Total Calories is: " + cals);
        m_totalCalories = cals;
        return m_totalCalories;
    }
}
