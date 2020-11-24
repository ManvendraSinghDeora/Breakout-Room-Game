using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_autoDes());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator _autoDes()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
