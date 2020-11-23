using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cliping : MonoBehaviour
{
    public float radius = 2;
    Transform _player;
    public LayerMask _BuildingLayer;
    void Start()
    {
        _player = GetComponent<PlayerFollow>().Target;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void FixedUpdate()
    {
        CLipFixing();
    }
    Collider LastHitCollider;
    
    void CLipFixing()
    {
        Vector3 Dir = _player.position - transform.position;
        if (Physics.Raycast(transform.position, Dir, out RaycastHit hit, 100, _BuildingLayer))
        {
            if (LastHitCollider != null)
            {
                if (LastHitCollider != hit.collider)
                {
                    LastHitCollider.GetComponent<MeshRenderer>().material.SetFloat("Vector1_B541F859", 1f);
                }
            }
            LastHitCollider = hit.collider;
            LastHitCollider.GetComponent<MeshRenderer>().material.SetFloat("Vector1_B541F859", 0.15f);
        }
        else
        {
            if (LastHitCollider != null)
            {
                LastHitCollider.GetComponent<MeshRenderer>().material.SetFloat("Vector1_B541F859", 1f);
                LastHitCollider = null;
            }
        }
    }

}
