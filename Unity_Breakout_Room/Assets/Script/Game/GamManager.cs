using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : MonoBehaviour
{
    public static GamManager Instance;
    public GameObject[] SpwanPoint;
    public GameObject[] Fruits;  //Array for different type of fruits

    private void Awake()
    {
        Instance = this;  
    }
    void Start()
    {
        StartCoroutine(FruitDrop());
    }
 //Add Rigidbody and collider to the fruit Prefab in the inspector 
    IEnumerator FruitDrop()
    {
        int rand = Random.Range(0, Fruits.Length);
        for (int i = 0; i < SpwanPoint.Length; i++)
        {
            Instantiate(Fruits[rand], SpwanPoint[i].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(FruitDrop());
    }
}
