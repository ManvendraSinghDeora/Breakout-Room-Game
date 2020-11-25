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

    [SerializeField] PowerUps _powerstats;

    public bool isProtected;
    public bool hasrockets;
    public bool HasMines;

    float MaxHealth = 100;
    int noofrockets;
    public GameObject LockedNearestTarget;
    public GameObject temp;
    GameObject [] Players;
    public List<GameObject> AllEnemyPlayer;
    public Collider[] _col;
    bool foundtarget;
    public LayerMask PlayerLayer;
    public int ReSpawn_Time = 3;
    public float Last_Health;
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
        Rockets();

        if (Last_Health != Health)
        {
            //object[] Datavalues = new object[Health]; 
            Debug.Log("Gone in Health Change");
            photonView.RPC("RCP_Set_Health", RpcTarget.All, Health);
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
                    _col = Physics.OverlapSphere(transform.position, _powerstats.FireRadiusRockets,PlayerLayer);
                    if (_col.Length > 0)
                    {
                        for(int i=0;i<_col.Length;i++)
                        {

                            if(foundtarget)
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
        GameObject temp= PhotonNetwork.Instantiate(_powerstats._rockets.name, transform.position + new Vector3(0,10,0), Quaternion.identity);
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
           
            StartCoroutine(ReSpawn(ReSpawn_Time));
        }
    }

    IEnumerator ReSpawn(int sec)
    {
        Health = 0;
        GameSetupController gameSetupController = GameObject.FindObjectOfType<GameSetupController>();
        transform.GetComponent<WheelVehicle>().enabled = false;
        transform.GetComponent<EngineSoundManager>().enabled = false;
        transform.GetComponent<Rigidbody>().useGravity = false;
        GameObject[] _child = transform.GetComponentsInParent<GameObject>();
        foreach(GameObject obj in _child)
        {

            obj.SetActive(false);
        }
        gameSetupController.RandomPointinArea(gameSetupController.SpawnArea_CenterPoint.position, Vector3.up, gameSetupController.SpawnArea_Radius);
        UIManager.Instance._respawn.SetActive(true);
        for (int i = sec; i > 0; i--)
        {
            UIManager.Instance._respawn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Respawn in " + i;
            yield return new WaitForSeconds(1);
        }


        UIManager.Instance._respawn.SetActive(false);

        foreach (GameObject obj in _child)
        {
            obj.SetActive(true);
        }
        transform.GetComponent<WheelVehicle>().enabled = true;
        transform.GetComponent<EngineSoundManager>().enabled = true;
        transform.GetComponent<Rigidbody>().useGravity = true;
        Health = 100;


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
        }else
        {
            PlayerStats.Instance.Health = 100;
        }
    }

    public void InizitlizeValues()
    {
        FireRadiusRockets = _constant.RocketFireRange;
        NoOfRockets = _constant.NumberofRockets;
        DamagePerRocket = _constant.DamagePerRocket;
    }
}

