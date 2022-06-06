using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerModel _playerModel;
    public PlayerPresenter playerPresenter;
    public PlayerView playerView;

    public string Name;
    public int CurrentMoney;
    public int DiceCount;

    public Player(PlayerModel model, PlayerPresenter presenter)
    {
        _playerModel = model;
        Name = model.Name;
        CurrentMoney = model.CurrentMoney;
        playerPresenter = presenter;
        playerPresenter.PlayerName(Name);
        playerPresenter.ChangeMoney(CurrentMoney);

    }

    
}