using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public GameObject GunPointer;
    public GameObject GunBackside;
    public GameObject BulletPrefab;


    private void Start()
    {
        if (GunBackside == null || GunPointer == null)
        {
            Debug.LogError("Define Gun Pointer and GunBackside");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var Bul = PhotonNetwork.Instantiate("Bullet", GunPointer.transform.position, Quaternion.identity);
            Bul.GetComponent<BulletScript>().Setup(directioncal());

        }
    }

    Vector3 directioncal()
    {
        return (GunPointer.transform.position - GunBackside.transform.position).normalized;
    }
   
}
