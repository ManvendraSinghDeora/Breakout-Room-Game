using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PowerUpSpawner : MonoBehaviourPun
{

    float CurrentPowerup;
    ConstantValues _cont = new ConstantValues();

    public GameObject current;
    void Start()
    {
        StartCoroutine(Spawner());
    }


    private void OnTriggerStay(Collider other)
    {
        _CheckCollision(other.gameObject);
    }

    public void _CheckCollision(GameObject other)
    {
            photonView.RPC("PowerupPicked", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
    }
    [PunRPC]
    public void PowerupPicked(int i)
    {
        Destroy(current);
        GameObject temp = PhotonView.Find(i).gameObject;
        PowerUpID(temp);
    }
    void PowerUpID(GameObject other)
    {
        if (CurrentPowerup == 1)
        {
            other.gameObject.GetComponent<PlayerStats>()._powerstats.Rockets();
        }
        if (CurrentPowerup == 2)
        {
            other.gameObject.GetComponent<PlayerStats>()._powerstats.Repair();
        }
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {

        int i = Random.Range(1, 10);
        yield return new WaitForSeconds(_cont.PowerUpSpawnerTime);
        if (i % 2 == 0)
        {
            current = PhotonNetwork.Instantiate("RocketPower", transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            CurrentPowerup = 1;
        }
        if (i % 2 != 0)
        {
            current = PhotonNetwork.Instantiate("HealthPower", transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            CurrentPowerup = 2;
        }

    }
}
