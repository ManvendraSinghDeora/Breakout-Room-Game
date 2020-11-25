using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Autodestroy : MonoBehaviourPun
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
