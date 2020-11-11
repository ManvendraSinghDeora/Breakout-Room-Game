using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VehicleBehaviour;

public class BoostBar : MonoBehaviour
{
    Image _boostBar;
    public WheelVehicle _PlayerRef; 
    
    // Start is called before the first frame update
    void Start()
    {
        _boostBar = GetComponent<Image>();
        _boostBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _boostBar.fillAmount = _PlayerRef.Boost/_PlayerRef.MaxBoost;
    }
}
