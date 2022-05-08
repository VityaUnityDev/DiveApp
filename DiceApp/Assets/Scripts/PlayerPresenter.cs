using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] PlayerView _playerView;
    [SerializeField] private GameManager _gameManager;

    private PlayerModel _playerModel;

    public PlayerPresenter(PlayerView view, GameManager model)
    {
        _playerView = view;
        _gameManager = model;
    }
    
    public void Enable()
    {
        // _playerModel.Lose += Lose;
        // _playerModel.ChangeMoney += ChangeHealth;
    }

    public void Start()
    {
        _playerView.OnMadeBet += MadeBet;
        _gameManager.DiceCount += DiceNumber;
        _playerModel.ChangeCurrentMoney += CurrentMoney;

    }

    private void  CurrentMoney(int  currentMoney)
    {
        _playerView.InfoAboutMoney(currentMoney);
    }

    private void MadeBet(int count)
    {
        _gameManager.MakeBet(count);
        GameInfo.Bet = count;
    }

    private void DiceNumber(int count)
    {
        _playerView.InfoAboutDice(count);
    }
    
    

    public void Disable()
    {
       
    }

    private void OnDestroy()
    {
        _playerView.OnMadeBet -= MadeBet;
        _gameManager.DiceCount -= DiceNumber;
        _playerModel.ChangeCurrentMoney -= CurrentMoney;
    }
}
