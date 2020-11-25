using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Rocket : MonoBehaviourPun
{
    public GameObject explosion;
    public Transform Target;
    public float Speed;
    public float rotateSpeed = 200f;
    public float Damage;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target != null)
        {
            Vector3 dir = Target.position - rb.position;
            dir.Normalize();
            Vector3 rotateAmount = Vector3.Cross(transform.up, dir);
            rb.angularVelocity = rotateAmount * rotateSpeed;
            rb.velocity = transform.up * Speed;
        }
    }

    void OnCollisionEnter(Collision other)
    {

        Debug.Log("Rocket Hit : " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            if (!other.gameObject.GetPhotonView().IsMine)
            {
                photonView.RPC( "SendDamage",RpcTarget.All,other.gameObject.GetPhotonView().ViewID);
            }
        }
    }
   [PunRPC]
    void SendDamage(int i)
    {
        GameObject _temp = PhotonView.Find(i).gameObject;
        if (_temp.activeSelf)
        {
            _temp.transform.GetComponent<PlayerStats>().TakeDamage(Damage);
            //Instantiate(explosion, transform.position,Quaternion.identity);
        }else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }

}

