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
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private TMP_Text bank;
    [SerializeField] private TMP_Text currentTimeText;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private TMP_Text bankPercent;
    [SerializeField] private TMP_Text countBet;

    [SerializeField] private Slider _slider;

    [SerializeField] private Button MaxBet5;
    [SerializeField] private Button MaxBet10;
    [SerializeField] private Button Bet10;

    [SerializeField] private Toggle autoBet;

    [SerializeField] private GameObject winnerPanel;
    [SerializeField] float startMinutes = 0.085f;


    public bool timerActive;
    public float currentTime;


    private void Awake()
    {
        MaxBet5.onClick.AddListener(() => MaxBetInGame(5));
        MaxBet10.onClick.AddListener(() => MaxBetInGame(10));
        Bet10.onClick.AddListener(() => Bet((int) _slider.value));
        _slider.onValueChanged.AddListener((v) => { countBet.text = _slider.value.ToString(); });

        GameInfo.Winner += FinishTable;
        GameInfo.Fees += UpdateFees;
    }

  
  

    private void FinishTable(Player player)
    {
        timerActive = false;
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
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
               CheckAvtoGame();
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

    private void CheckAvtoGame()
    {
        if (autoBet.isOn)
        {
            Bet((int)_slider.value);
        }
        else
        {
            Skip();
        }
    }



    private void Bet(int bet)
    {
        Start();
        GameInfo.MadeBet = true;
        var amount = bet * GameInfo.Players.Count;
        GameWasStarted(bet, amount);
    }

    private void Skip()
    {
        GameInfo.MadeBet = false;
        var randomBet = (int) Random.Range(_slider.minValue, _slider.maxValue);
        var amount = randomBet * (GameInfo.Players.Count - 1);
        GameWasStarted(randomBet, amount);
    }

    
    private void GameWasStarted(int bet, int amount)
    {
        StopTimer();
        Bet10.enabled = false;
        GameInfo.Bet = bet;
        _gameManager.CountPlayerInGame();
        bank.text = "Bank " + amount;
    }

    private void UpdateFees(float count) =>   bankPercent.text = "Bank fees - " + count;
    public void StartTimer() => timerActive = true;
    private void StopTimer() => timerActive = false;
    private void Start() =>   currentTime = startMinutes * 60;

    private void OnDestroy()
    {
        GameInfo.Winner -= FinishTable;
        GameInfo.Fees -= UpdateFees;
    }
}