using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Globalization;

public class UI : MonoBehaviour
{
    // в будущем здесь может быть большое кол-во других ui значений

    [SerializeField]
    TextMeshProUGUI _txtEnergy;

    [SerializeField]
    TextMeshProUGUI _txtTime;

    private float _time = 60;
    private float _timeLeft = 0f;

    //private bool _fullEnergy = true;
    private DateTime _startRecovery;



    void Start()
    {
        _timeLeft = _time;
        _txtEnergy.text = PlayerData.energy.ToString();
    }

    public void UpdateUI()
    {
        // в будущем здесь может быть большое кол-во других ui значений
        if (PlayerData.energy != 100 && _timeLeft == 60)
        {
            Debug.Log("«апускаю таймер");

            StartCoroutine(StartTimer());
            //StartCoroutine(EnergyRecovery());
        }

        if (_timeLeft < 0)
        {
            PlayerData.energy += 1;
            _timeLeft = 60;
            StopCoroutine(StartTimer());

            Debug.Log("—топ таймер");
        }

        float minutes = Mathf.FloorToInt(_timeLeft / _time);
        float seconds = Mathf.FloorToInt(_timeLeft % _time);

        _txtEnergy.text = PlayerData.energy.ToString();
        _txtTime.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


    IEnumerator StartTimer()
    {
        while (_timeLeft > 0 && PlayerData.energy != 100)
        {
            _timeLeft -= Time.deltaTime;
            UpdateUI();
            yield return null;
        }
    }


}
