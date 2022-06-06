using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameView : MonoBehaviour
{
    [SerializeField] private Button Bet10;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text bank;
    [SerializeField] private TMP_Text currentTimeText;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text countBet;


    [SerializeField] private Button MaxBet5;
    [SerializeField] private Button MaxBet10;


    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private TimeController _timeController;

    [SerializeField] private TMP_Text bankPercent;

    public bool timetActive;
    public float currentTime;
    public float startMinutes = 0.085f;

    private void Awake()
    {
        MaxBet5.onClick.AddListener(() => MaxBetInGame(5));
        MaxBet10.onClick.AddListener(() => MaxBetInGame(10));

        GameInfo.Winner += FinishTable;
        GameInfo.Fees += UpdateFees;


        Bet10.onClick.AddListener(() => Bet((int) _slider.value));

        _slider.onValueChanged.AddListener((v) => { countBet.text = _slider.value.ToString(); });
    }

    private void Start()
    {
        currentTime = startMinutes * 60;
    }

    private void FinishTable(Player player)
    {
        timetActive = false;
        winnerPanel.SetActive(true);
        winnerText.text = player.Name + " Is winner";
      

    }

    private void MaxBetInGame(int maxValue)
    {
        
        _gameManager.StartGame();
        if (maxValue == 10)
        {
            _slider.minValue = maxValue / 2;
        }

        _slider.maxValue = maxValue;
    }


    private void Update()
    {
        Debug.Log("time active" + timetActive);
        if (timetActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Skip();
                Start();
                currentTime = startMinutes * 60;
            }
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = timeSpan.Seconds.ToString();

        if (GameInfo.finishGame && GameInfo.Players.Count > 0)
        {
            Bet10.enabled = true;
            StartTimer();
        }
    }


    public void StartTimer()
    {
        timetActive = true;
    }

    public void StopTimer()
    {
        timetActive = false;
    }

    private void Bet(int bet)
    {
        Start();
        StopTimer();
        GameInfo.MadeBet = true;
        Bet10.enabled = false;
        _gameManager.MakeBet(bet);
        var amount = bet * GameInfo.Players.Count;
        bank.text = "Bank " + amount;
    }

    public void Skip()
    {
        StopTimer();
        Bet10.enabled = false;
        GameInfo.MadeBet = false;
     
       var randomBet = (int)Random.Range(_slider.minValue, _slider.maxValue);
        _gameManager.MakeBet(randomBet);
        var amount = randomBet * (GameInfo.Players.Count - 1);
        bank.text = "Bank " + amount;
    }
    
    private void UpdateFees(int count)
    {
        bankPercent.text = "Bank fees -" + count;
    }
}