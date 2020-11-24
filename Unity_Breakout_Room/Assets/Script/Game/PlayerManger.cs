using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManger : MonoBehaviour
{

    public Animator animator;
    public PhotonView photonView;
    public bool isPunch;
   void Punch()
    {
        if (photonView.IsMine)
        {
            animator.SetBool("Punch", true);
        }
    }


    public void SetPunch()
    {
        if (photonView.IsMine)
        {
            animator.SetBool("Punch", false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Punch();
        }
    }
}
