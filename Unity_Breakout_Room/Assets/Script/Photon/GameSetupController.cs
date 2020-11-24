using Photon.Pun;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VehicleBehaviour;

public class GameSetupController : MonoBehaviourPunCallbacks
{

    GameObject v_CurrentPlayer;
    public Transform SpawnArea_CenterPoint;
    public PlayerFollow playerFollow;
    float SpawnArea_Radius =130;
    public GameObject PlayerPrefab;
    void Awake()
    {
        CreatePlayer();
    }
    private void CreatePlayer()
    {
       

        v_CurrentPlayer = PhotonNetwork.Instantiate(PlayerPrefab.name, RandomPointinArea(SpawnArea_CenterPoint.position,Vector3.up,SpawnArea_Radius),Quaternion.identity);
        playerFollow.Target = v_CurrentPlayer.transform;
        v_CurrentPlayer.GetComponent<Rigidbody>().useGravity = true;
        v_CurrentPlayer.GetComponent<WheelVehicle>().enabled = true;
        v_CurrentPlayer.GetComponent<EngineSoundManager>().enabled = true;
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(SpawnArea_CenterPoint.position, 130);
    }

    private Vector3 RandomPointinArea(Vector3 position, Vector3 normal, float radius)
    {
        Vector3 randomPoint;

        do
        {
            randomPoint = Vector3.Cross(Random.insideUnitSphere, normal);
        } while (randomPoint == Vector3.zero);

        randomPoint.Normalize();
        randomPoint *= radius;
        randomPoint += position;

        return randomPoint;
    }

}

