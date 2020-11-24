using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerStats : MonoBehaviourPunCallbacks
{
    public static PlayerStats Instance;
    public int Health;

    [SerializeField] PowerUps _powerstats;

    public bool isProtected;
    public bool hasrockets;
    public bool HasMines;

    int MaxHealth = 100;
    int noofrockets;
    GameObject LockedNearestTarget;
    GameObject [] Players;
    public List<GameObject> AllEnemyPlayer;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        Health = MaxHealth;
        _powerstats.InizitlizeValues();
        Players = GameObject.FindGameObjectsWithTag("Player");
        noofrockets = _powerstats.NoOfRockets;
        FindallPlayers();
    }
    void Start()
    {

    }
    
    void FindallPlayers()
    {
        AllEnemyPlayer = new List<GameObject>();
        for (int i = 0; i < Players.Length; i++)
        {
            if (!photonView.IsMine)
            {
                AllEnemyPlayer.Add(Players[i]);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        Rockets();
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
                    FindallPlayers();
                    Collider[] _col = Physics.OverlapSphere(transform.position, _powerstats.FireRadiusRockets);
                    if (_col.Length > 0)
                    {
                        for(int i=0;i<_col.Length;i++)
                        {
                            for (int j = 0; j < AllEnemyPlayer.Count; j++)
                            {
                                if (_col[i].gameObject == AllEnemyPlayer[j])
                                {
                                    LockedNearestTarget = AllEnemyPlayer[j];
                                    FireRockets();
                                    noofrockets -= 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }else
            {
                noofrockets = _powerstats.NoOfRockets;
                hasrockets = false;
            }
        }else
        {
            return;
        }
    }

    private void FireRockets()
    {
        GameObject temp= PhotonNetwork.Instantiate("RocketPrefab", transform.position, Quaternion.identity);
        temp.GetComponent<Rigidbody>().AddForce(transform.up * 10, ForceMode.Impulse);
        temp.GetComponent<Rocket>().Target = LockedNearestTarget.transform;
        temp.GetComponent<Rocket>().Damage = _powerstats.DamagePerRocket;
    }


    public void TakeDamage(int Damage)
    {
        if (Health - Damage > 0)
        {        
                Health -= Damage;
        }else
        {
            //Death();
        }
    }
}
[Serializable]
public class PowerUps 
{
    [Header("Repair Power Up Stat : ")]
    public int RepairHealth;
    [Header("Shield Power Up Stat : ")]
    [Tooltip("In Secs")]
    public int ShieldDuration;
    [Header("Rocket Power Up Stat : ")]
    public GameObject _rockets;
    public int FireRadiusRockets;
    public int NoOfRockets;
    public int DamagePerRocket;
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
        }else
        {
            PlayerStats.Instance.Health = 100;
        }
    }

    public void InizitlizeValues()
    {
        FireRadiusRockets = _constant.RocketFireRange;
        NoOfRockets = _constant.NumberofRockets;
    }
}

