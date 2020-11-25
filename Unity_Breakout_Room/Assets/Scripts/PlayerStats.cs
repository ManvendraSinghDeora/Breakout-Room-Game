using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using VehicleBehaviour;
using TMPro;

public class PlayerStats : MonoBehaviourPun
{
    public static PlayerStats Instance;
    public float Health;

    public PowerUps _powerstats;

    public bool isProtected;
    public bool hasrockets;
    public bool HasMines;

    float MaxHealth = 100;
    int noofrockets;
    public GameObject LockedNearestTarget;
    public GameObject temp;
    GameObject[] Players;
    public List<GameObject> AllEnemyPlayer;
    public Collider[] _col;
    bool foundtarget;
    public LayerMask PlayerLayer;
    public int ReSpawn_Time = 3;
    public float Last_Health;
    public localMang _Local;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        Health = MaxHealth;
        Last_Health = Health;
        _powerstats.InizitlizeValues();

        noofrockets = _powerstats.NoOfRockets;
        FindallPlayers();
    }
    void Start()
    {
        if (_Local == null)
            _Local = GameObject.FindObjectOfType<localMang>();
    }

    void FindallPlayers()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        AllEnemyPlayer = new List<GameObject>();
        for (int i = 0; i < Players.Length; i++)
        {
            if (!Players[i].GetPhotonView().IsMine)
            {
                AllEnemyPlayer.Add(Players[i]);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (_Local == null)
        {
            _Local = GameObject.FindObjectOfType<localMang>();
        }
        Rockets();

        if (Last_Health != Health)
        {
            //object[] Datavalues = new object[Health]; 
            Debug.Log("Gone in Health Change");
            photonView.RPC("RCP_Set_Health", RpcTarget.All, Health);
        }

        if (Health <= 0)
        {
            if (photonView.IsMine)
            {
                _Local.Respawn();
            }
        }
    }

    [PunRPC]
    public void RCP_Set_Health(float value)
    {
        Last_Health = Health = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _powerstats.FireRadiusRockets);
    }

    public void Rockets()
    {
        if (hasrockets)
        {
            //Display Rockets in UI
            if (noofrockets > 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    foundtarget = false;
                    FindallPlayers();
                    _col = Physics.OverlapSphere(transform.position, _powerstats.FireRadiusRockets, PlayerLayer);
                    if (_col.Length > 0)
                    {
                        for (int i = 0; i < _col.Length; i++)
                        {

                            if (foundtarget)
                            {
                                Debug.Log("Target not found");
                                break;
                            }
                            Debug.Log("Target found");

                            for (int j = 0; j < AllEnemyPlayer.Count; j++)
                            {
                                Debug.Log("Checking for Target");
                                Debug.Log(_col[i].transform.parent.gameObject.transform.parent.gameObject.name);
                                if (_col[i].transform.parent.gameObject.transform.parent.gameObject == AllEnemyPlayer[j])
                                {
                                    Debug.Log("Target Locked and Fire");
                                    LockedNearestTarget = AllEnemyPlayer[j];
                                    FireRockets();
                                    foundtarget = true;
                                    noofrockets -= 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                noofrockets = _powerstats.NoOfRockets;
                hasrockets = false;
            }
        }
        else
        {
            return;
        }
    }

    private void FireRockets()
    {
        GameObject temp = PhotonNetwork.Instantiate("RocketPrefab", transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        temp.GetComponent<Rigidbody>().AddForce(transform.up * 10, ForceMode.Impulse);
        temp.GetComponent<Rocket>().Target = LockedNearestTarget.transform;
        temp.GetComponent<Rocket>().Damage = _powerstats.DamagePerRocket;
    }


    public void TakeDamage(float Damage)
    {
        float damgetake = Health - Damage;
        if (damgetake > 0)
        {
            Health = damgetake;
        }
        else
        {
            Health = 0;
           
        }
    }
    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("Collided to Spawner");
    //    if(col.gameObject.tag=="Spawner")
    //    {
    //        col.gameObject.GetComponent<PowerUpSpawner>()._CheckCollision(this.gameObject);
    //    }
    //}
}
[Serializable]
public class PowerUps 
{
    [Header("Repair Power Up Stat : ")]
    public float RepairHealth;
    [Header("Shield Power Up Stat : ")]
    [Tooltip("In Secs")]
    public int ShieldDuration;
    [Header("Rocket Power Up Stat : ")]
    public GameObject _rockets;
    public int FireRadiusRockets;
    public int NoOfRockets;
    public float DamagePerRocket;
    [Header("Mines/Spike Power Up Stat : ")]
    public GameObject _mines;
    public int NoOfMines;
    public int DamagePerMines;

    ConstantValues _constant = new ConstantValues();

    public void Rockets()
    {  
            PlayerStats.Instance.hasrockets = true;
    }
    public void Mines()
    {
       
            PlayerStats.Instance.HasMines = true;

    }
    public IEnumerator Shield()
    {
        
            PlayerStats.Instance.isProtected = true;
            yield return new WaitForSeconds(ShieldDuration);
            PlayerStats.Instance.isProtected = false;

    }
    public void Repair()
    {
            if (PlayerStats.Instance.Health < 100)
            {
                PlayerStats.Instance.Health += RepairHealth;
            }
            else
            {
                PlayerStats.Instance.Health = 100;
            }
    }

    public void InizitlizeValues()
    {
        RepairHealth = _constant.RepairHealth;
        FireRadiusRockets = _constant.RocketFireRange;
        NoOfRockets = _constant.NumberofRockets;
        DamagePerRocket = _constant.DamagePerRocket;
    }
}

