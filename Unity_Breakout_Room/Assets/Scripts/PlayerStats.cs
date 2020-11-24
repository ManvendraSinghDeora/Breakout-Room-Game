using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
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
    List<GameObject> AllEnemyPlayer;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        Health = MaxHealth;
        Players = GameObject.FindGameObjectsWithTag("Player");
        for(int i=0;i<Players.Length ; i++)
        {
            if(Players[i]!=this.gameObject)
            {
                AllEnemyPlayer.Add(Players[i]);
            }
        }
        noofrockets = _powerstats.NoOfRockets;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rockets();
    }

    void Rockets()
    {
        if (hasrockets)
        {
            //Display Rockets in UI
            if (noofrockets > 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Collider[] _col = Physics.OverlapSphere(transform.position, _powerstats.FireRadiusRockets);
                    if (_col.Length > 0)
                    {
                        for(int i=0;i<_col.Length;i++)
                        {
                           if(_col[i].gameObject==AllEnemyPlayer[i])
                            {
                                LockedNearestTarget = AllEnemyPlayer[i];
                                FireRockets();
                                noofrockets -= 1;
                                break;
                            }
                        }
                    }
                }
            }else
            {
                noofrockets = _powerstats.NoOfRockets;
                hasrockets = false;
            }
        }
    }

    private void FireRockets()
    {
        GameObject temp= Instantiate(_powerstats._rockets, transform.position, Quaternion.identity);
        temp.GetComponent<Rigidbody>().AddForce(transform.up * 10, ForceMode.Impulse);
        temp.GetComponent<Rocket>().Target = LockedNearestTarget.transform;
        temp.GetComponent<Rocket>().Damage = _powerstats.DamagePerRocket;
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

}

