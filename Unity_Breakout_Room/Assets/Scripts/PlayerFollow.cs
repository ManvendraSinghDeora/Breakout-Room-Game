using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float CameraMoveSpeed;
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + Offset, CameraMoveSpeed*Time.deltaTime);
    }
}
