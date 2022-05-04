using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand
{
    private PlayerModel _playerModel;

    public event Action<PlayerModel> OnMakeMove;
    
    public MoveCommand(PlayerModel model)
    {
        _playerModel = model;
    }

    public void Execute()
    {
        // _playerModel.
    }
}
