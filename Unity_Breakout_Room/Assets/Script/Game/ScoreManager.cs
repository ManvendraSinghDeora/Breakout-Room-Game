using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class ScoreManager : MonoBehaviourPun
{
    public Text playerPoints;

    void Start()
    {
        //PhotonNetwork.player.SetScore(0);
        UpdateText();
    }

    // Use this for initialization
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            photonView.RPC("AddPoints", RpcTarget.AllBuffered);

        }

    }

    [PunRPC]
    public void AddPoints()
    {
        //Player.AddScore(10);
        UpdateText();
    }

    public void UpdateText()
    {
        //playerPoints.text = PhotonNetwork.player.GetScore().ToString();
    }
}
