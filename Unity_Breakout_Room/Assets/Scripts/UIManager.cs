using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using VehicleBehaviour;
using TMPro;
public class UIManager : MonoBehaviourPun
{
    public static UIManager Instance;
    public Image _healthBar;
    public Image _boostbar;
    public Image _boostbarbg;
    public GameObject _respawn;

    public TextMeshProUGUI Timer;
    public TextMeshProUGUI Killtext;

    GameObject Player;
    // Start is called before the first frame update

    public float timeRemaining = 5;
    public bool timerIsRunning = false;

    float totalkills;
    void Awake()
    {
        if(Instance==null)
        Instance = this;
    }
    void Start()
    {
        totalkills = 0;
        timerIsRunning = true;
        _healthBar.fillAmount = 1;
        _boostbar.fillAmount = 1;
        _boostbarbg.fillAmount = 1;
        initilize();
    }
    void initilize()
    {
        GameObject[] _player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject _obj in _player)
        {
            if (_obj.GetPhotonView().IsMine)
            {
                Player = _obj;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(Player==null)
        {
            initilize();

        }
        UpdateBoostandHealth();
        INgameTimer();
    }
    void UpdateBoostandHealth()
    {
        _healthBar.fillAmount = PlayerStats.Instance.Health / 100;
        _boostbar.fillAmount =  Player.GetComponent<WheelVehicle>().Boost / Player.GetComponent<WheelVehicle>().MaxBoost;
        _boostbarbg.fillAmount = Player.GetComponent<WheelVehicle>().Boost / Player.GetComponent<WheelVehicle>().MaxBoost;
    }

    void INgameTimer()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                
                //EndGame();
            }
        }

    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void DisplayKills(float kils)
    {
        totalkills += kils;
        Killtext.text = "Kills : " + totalkills;
    }
}
