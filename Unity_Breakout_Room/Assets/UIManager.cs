using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image _healthBar;



    // Start is called before the first frame update
    void Start()
    {

        _healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateHealth();
    }
    void UpdateHealth()
    {
        _healthBar.fillAmount = PlayerStats.Instance.Health / 100;
    }
}
