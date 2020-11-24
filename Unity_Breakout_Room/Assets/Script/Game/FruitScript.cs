using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hand") //Tag the Player Hand As Hand in inspector
        {
            Destroy(this.gameObject);
            Debug.Log("Player Hit The Item");
            Debug.Log("Need to Add Score and Need to Use RPC ");
            //Score++   Increase the Score Variable...
        }
        else if (collision.gameObject.tag == "Ground") //Tag the Ground  As Ground in inspector
        {
            Destroy(this.gameObject);

            //Score-- You can minus Score is you want
        }
    }
}
