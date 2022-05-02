using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] PlayerView _playerView;
    private PlayerModel _playerModel;

    public PlayerPresenter(PlayerView view, PlayerModel model)
    {
        _playerView = view;
        _playerModel = model;
    }
    
    public void Enable()
    {
        _playerModel.Lose += Lose;
        _playerModel.ChangeMoney += ChangeHealth;
    }

    public void Start()
    {
    }

    private void ChangeHealth(int  health)
    {
     //   _playerView.Changehealth(health);
    }

    private void Lose()
    {
     
      
    }

    public void Disable()
    {
        _playerModel.Lose -= Lose;
        _playerModel.ChangeMoney -= ChangeHealth;
    }

   
}
