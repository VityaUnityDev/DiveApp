using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBetCommand : MonoBehaviour
{
    private Player Player;


    public MakeBetCommand(Player player)
    {
        Player = player;
    }

    public void Execute(bool isMadeBet, int bet)
    {
        GameInfo.finishGame = false;
        if (isMadeBet)
        {
            Player._playerModel.MakeBet(bet);
          
            for (int i = 0; i < GameInfo.Players.Count; i++)
            {
                if (GameInfo.Players[i]._playerModel.playGame)
                {
                    GameInfo.Players[i].playerPresenter.ChangeMoney( GameInfo.Players[i]._playerModel.CurrentMoney);
                    GameInfo.Players[i].playerPresenter.ClearInfoAboutDice();
                }
                else
                {
                    GameInfo.Players[i].playerPresenter.EndGame();
                    GameInfo.Players.RemoveAt(i);
                    i--;
                }
            }
            GameInfo.Bet = bet;
        }
    }
}