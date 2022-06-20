using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerModel _playerModel;
    public PlayerPresenter playerPresenter;

    public string Name;

    public Player(PlayerModel model, PlayerPresenter presenter)
    {
        _playerModel = model;
        Name = model.Name;
        playerPresenter = presenter;
        playerPresenter.PlayerName(Name);
        playerPresenter.ChangeMoney();

    }
    
}