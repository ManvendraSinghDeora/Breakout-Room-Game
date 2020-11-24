using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletScript : MonoBehaviourPun
{
    #region Public Declarations

    public int Damage;
    public float Speed;
    private Vector3 Direction;
    private Rigidbody rb;
    #endregion


    public void Setup(Vector3 dir)
    {
        Direction = dir;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = Direction * Speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision _collision)
    {
        if (!photonView)
        {
            _collision.gameObject.GetComponent<PlayerStats>().Health -= Damage;
            Destroy(this.gameObject);
        }
        if (_collision.gameObject.tag == "Environment")
        {   
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider _collider)
    {

    }
}
