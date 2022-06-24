using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIView : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlayerGenerator _playerGenerator;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private GameConfig _gameConfig;

    [SerializeField] private TMP_Text bank;
    [SerializeField] private TMP_Text currentTimeText;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private TMP_Text bankPercent;
    [SerializeField] private TMP_Text countBet;
    [SerializeField] private TMP_Text infoGame;
    [SerializeField] private TMP_Text winner;

    [SerializeField] private Slider _slider;

    [SerializeField] private Button MaxBet5;
    [SerializeField] private Button MaxBet10;
    [SerializeField] private Button Bet10;

    [SerializeField] private Toggle autoBet;

    [SerializeField] private GameObject winnerPanel;


    
 


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
        winnerPanel.SetActive(true);
        winnerText.text = player.Name + " Is winner";
        _playerGenerator.DestroyPlayers();
      
    }

    private void MaxBetInGame(int maxValue)
    {
       
        GameInfo.maxBet = maxValue;
        _playerGenerator.StartGame();
        if (maxValue == 10)
        {
            _gameConfig.CreateGame(TypeGame.MaxBet10);
            _slider.minValue = maxValue / 2;
            GameInfo.minBet = (int)_slider.minValue;
        }
        else
        {
            _gameConfig.CreateGame(TypeGame.MaxBet5);
            GameInfo.minBet = 1;
        }

        _slider.maxValue = maxValue;
        _timeController.StartTimer();
        infoGame.gameObject.SetActive(false);
    }

    public void UpdateTimeText(TimeSpan timeSpan)
    {
        currentTimeText.text = timeSpan.Seconds.ToString();
    }


    private void Update()
    {
        if (GameInfo.finishGame && GameInfo.Players.Count > 0)
        {
            Bet10.enabled = true;
            _timeController.StartTimer();
            InfoGame(true);
        }
    }

    public void CheckAutoGame()
    {
        if (autoBet.isOn)
        {
            Bet((int) _slider.value);
        }
        else
        {
            Skip();
        }
    }


    private void Bet(int bet)
    {
        var mainPlayer = GameInfo.Players.ElementAt(0);
        if (mainPlayer.Value._playerModel.CurrentMoney >= bet)
        {
            GameInfo.PlayersInCurrentGame.Add(mainPlayer.Key, mainPlayer.Value);
            GameInfo.Bet = bet;
            GameWasStarted();
            winner.text = "";
           
            
        }
        else
        {
            if (GameInfo.Players.Count > 2)
            {
                Skip();
            }
        }

       
        
    }

    private void Skip()
    {
        var randomBet = (int) Random.Range(_slider.minValue, _slider.maxValue);
        GameInfo.Bet = randomBet;
        GameWasStarted();
        winner.text = "";
    }


    private void GameWasStarted()
    {
        _timeController.StopTimer();
        Bet10.enabled = false;
        _gameManager.CountPlayerInGame();
        bank.text = "Bank " + GameInfo.Result;
        _timeController.UpdateTime();
        InfoGame(false);
    }

    private void UpdateFees(float count)
    {
        bankPercent.text = "Bank fees - " + count;
    }

 


    private void OnDestroy()
    {
        GameInfo.Winner -= FinishTable;
        GameInfo.Fees -= UpdateFees;
    }


    private void InfoGame(bool showTimer)
    {
        currentTimeText.gameObject.SetActive(showTimer);
        infoGame.gameObject.SetActive(!showTimer);
    }
}