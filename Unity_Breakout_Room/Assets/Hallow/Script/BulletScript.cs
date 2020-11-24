using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
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
        PlayerStats.Instance.Health -= Damage;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider _collider)
    {

    }
}
