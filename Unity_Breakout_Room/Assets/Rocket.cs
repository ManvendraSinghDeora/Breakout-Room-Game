using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosion;
    public Transform Target;
    public float Speed;
    public float rotateSpeed = 200f;
    [HideInInspector]
    public int Damage;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = Target.position - rb.position;
        dir.Normalize();
        Vector3 rotateAmount = Vector3.Cross(transform.up, dir);
        rb.angularVelocity = rotateAmount*rotateSpeed;
        rb.velocity = transform.up * Speed;
    
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="Player")
        {
            PlayerStats.Instance.Health -= Damage;
            //Instantiate(explosion, transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
     
    }
}
