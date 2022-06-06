using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameCommand : MonoBehaviour
{
    private Player Player;


    public StartGameCommand(Player player)
    {
        Player = player;
        
    }

    public void Execute(int diceCount)
    {
        Player._playerModel.DiceCount = diceCount;
        Player.playerPresenter.DiceCount(diceCount);
    }
}