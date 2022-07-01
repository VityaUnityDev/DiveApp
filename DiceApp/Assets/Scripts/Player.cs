using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerModel _playerModel;
    public PlayerPresenter playerPresenter;
    public PlayerView playerView;

    public string Name;

    public bool isRoll;

    private float time;

    public Player(PlayerModel model, PlayerPresenter presenter, PlayerView view)
    {
        _playerModel = model;
        Name = model.Name;
        playerPresenter = presenter;
        playerPresenter.PlayerName(Name);
        playerPresenter.ChangeMoney();
        playerView = view;
    }


  
}