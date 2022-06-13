using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    private PlayerView _playerView;
    private PlayerModel _playerModel;


    public PlayerPresenter(PlayerView view, PlayerModel model)
    {
        _playerView = view;
        _playerModel = model;
    }


    private void Start()
    {
        _playerModel.OnUpdatedMoney += ChangeMoney;
       
    }

    public void PlayerName(string name)
    {
        _playerView.Name(name);
    }

    public void ChangeMoney(float count)
    {
        _playerView.ChangeMoney(count);
    }

    public void DiceCount(int dice)
    {
        _playerView.DiceCount(dice);
    }

    public void EndGame()
    {
        _playerView.EndGame();
    }
    

    public void ClearInfoAboutDice()
    {
        _playerView.ClearDice();
    }

    public void InstallPhoto(Sprite image)
    {
        _playerView.InstallPhoto(image);
    }


    private void OnDestroy()
    {
        _playerModel.OnUpdatedMoney -= ChangeMoney;
    }
}