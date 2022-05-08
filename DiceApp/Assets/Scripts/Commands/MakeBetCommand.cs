using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBetCommand : MonoBehaviour
{
    private PlayerModel _playerModel;


    public MakeBetCommand(PlayerModel model)
    {
        _playerModel = model;
    }

    public void Execute(bool isMadeBet, int bet)
    {
        if (isMadeBet)
        {
            _playerModel.MakeBet(bet);
        }

        
    }
}