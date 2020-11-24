using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using VehicleBehaviour;
public class UIManager : MonoBehaviourPun
{
    public Image _healthBar;
    public Image _boostbar;
    public Image _boostbarbg;


    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
      
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
    }
    void UpdateBoostandHealth()
    {
        _healthBar.fillAmount =Player.GetComponent<PlayerStats>().Health / 100;
        Debug.Log(Player.GetComponent<PlayerStats>().Health / 100);
        _boostbar.fillAmount = Player.GetComponent<WheelVehicle>().Boost / Player.GetComponent<WheelVehicle>().MaxBoost;
        _boostbarbg.fillAmount = Player.GetComponent<WheelVehicle>().Boost / Player.GetComponent<WheelVehicle>().MaxBoost;
    }

}
