using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviourPunCallbacks
{
    public Transform GunPointer;
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
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject Bul = PhotonNetwork.Instantiate(BulletPrefab.name, GunPointer.transform.position, Quaternion.identity);
                Bul.GetComponent<BulletScript>().Setup(directioncal());
            }
        }
    }



    Vector3 directioncal()
    {
        return (GunPointer.transform.position - GunBackside.transform.position).normalized;
    }
}
