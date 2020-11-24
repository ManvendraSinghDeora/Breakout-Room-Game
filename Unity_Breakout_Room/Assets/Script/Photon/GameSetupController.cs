using Photon.Pun;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetupController : MonoBehaviourPunCallbacks
{

    public List<Transform> spawnPostion;
    public List<GameObject> ScorePanle;
    public List<Color> colors;
    private int spawnIndex;
    private int count;
    GameObject temp;
    int range;
    void Start()
    {
        CreatePlayer();
    }
    private void CreatePlayer()
    {
        Time.timeScale = 1;
        //GameObject temp = PhotonNetwork.Instantiate("Player Prefab", spawnPostion[0].position, Quaternion.Euler(0, 180, 0));
        range = Random.Range(0, spawnPostion.Count);

        temp = PhotonNetwork.Instantiate("Player Prefab", spawnPostion[range].position, Quaternion.Euler(0, 180, 0));
        //temp.GetComponent<PhotonView>().RPC("ColorChage", RpcTarget.AllBuffered, null);

        #region
        /*
        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //{

        //    Debug.Log("PLAYER :" + PhotonNetwork.PlayerList[i].ActorNumber);
        //    switch (PhotonNetwork.CurrentRoom.Players.ElementAt(i).Key)
        //    {
        //        case 1:

        //            temp = PhotonNetwork.Instantiate("Player Prefab", spawnPostion[i].position, Quaternion.Euler(0, 180, 0));
        //            temp.GetComponentInChildren<SkinnedMeshRenderer>().material.color = colors[i];
        //            break;
        //        case 2:
        //            temp = PhotonNetwork.Instantiate("Player Prefab", spawnPostion[i].position, Quaternion.Euler(0, 180, 0));
        //            temp.GetComponentInChildren<SkinnedMeshRenderer>().material.color = colors[i];
        //            break;
        //        case 3:
        //            temp = PhotonNetwork.Instantiate("Player Prefab", spawnPostion[i].position, Quaternion.Euler(0, 180, 0));
        //            temp.GetComponentInChildren<SkinnedMeshRenderer>().material.color = colors[i];
        //            break;
        //        case 4:
        //            temp = PhotonNetwork.Instantiate("Player Prefab", spawnPostion[i].position, Quaternion.Euler(0, 180, 0));
        //            temp.GetComponentInChildren<SkinnedMeshRenderer>().material.color = colors[i];
        //            break;
        //    }
        //}

        var pv = PhotonNetwork.CurrentRoom.Players.ElementAt(0);
        Debug.Log("Vlue of PV :" + pv);
        */
        #endregion
    }


    //[PunRPC]
    //public void ColorChage()
    //{
    //    if (temp.GetComponent<PhotonView>().IsMine)
    //    {
    //        temp.GetComponentInChildren<SkinnedMeshRenderer>().material.color = colors[range];
    //    }
    //}
    public float timeRemaining = 60;
    public Text time;

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int value = (int)timeRemaining;
            time.text = value.ToString();

        }

        if (timeRemaining <=0)
        {
            Time.timeScale = 0;
        }

        if (count != PhotonNetwork.PlayerList.Length)
        {
            foreach (GameObject item in ScorePanle)
            {
                item.SetActive(false);
            }
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                ScorePanle[i].SetActive(true);
            }
            count = PhotonNetwork.PlayerList.Length;
        }
    }

    public void OnUserLeaveRoom()
    {
        if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
            SceneManager.LoadScene(0);
        }
    }

}
