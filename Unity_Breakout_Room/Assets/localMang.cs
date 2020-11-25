using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class localMang : MonoBehaviourPun
{
    GameObject Player;
    bool running;
    void Start()
    {
        CheckforownPlayer();
    }
    void CheckforownPlayer()
    {
        if (Player == null)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in temp)
            {
                if (obj.GetPhotonView().IsMine)
                {
                    Player = obj;
                }
            }
        }
    }

    void Update()
    {
        CheckforownPlayer();
    }
    public void Respawn()
    {
        StartCoroutine(ReSpawnCO(3));
    }
    public IEnumerator ReSpawnCO(int sec)
    {
        running = true;
        GameSetupController gameSetupController = GameObject.FindObjectOfType<GameSetupController>();
        Player.SetActive(false);
        Vector3 newpos = gameSetupController.RandomPointinArea(gameSetupController.SpawnArea_CenterPoint.position, Vector3.up, gameSetupController.SpawnArea_Radius);
        Player.transform.position = newpos;
        UIManager.Instance._respawn.SetActive(true);
        for (int i = sec; i > 0; i--)
        {
            UIManager.Instance._respawn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Respawn in " + i;
            yield return new WaitForSeconds(1);
        }
        Player.SetActive(true);
        Player.GetComponent<PlayerStats>().Health = 100;
        UIManager.Instance._respawn.SetActive(false);

        running = false;
    }
}
