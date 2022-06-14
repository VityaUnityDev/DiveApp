using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakeBetCommand : AbstractCommand
{
    public override void Execute()
    {
        GameInfo.finishGame = false;

        for (int i = 0; i < GameInfo.PlayersInCurrentGame.Values.Count; i++)
        {
           
            GameInfo.PlayersInCurrentGame.ElementAt(i).Value._playerModel.MakeBet();
            if (GameInfo.PlayersInCurrentGame.ElementAt(i).Value._playerModel.playGame)
            {
                GameInfo.PlayersInCurrentGame.ElementAt(i).Value.playerPresenter.ChangeMoney();
            }

            else
            {
                GameInfo.PlayersInCurrentGame.ElementAt(i).Value.playerPresenter.EndGame();
                var pl = GameInfo.PlayersInCurrentGame.ElementAt(i).Key;
                GameInfo.PlayersInCurrentGame.Remove(pl);
                GameInfo.Players.Remove(pl);
                i--;
                Debug.Log("Player COunt" + GameInfo.Players.Count); // не унитожаются префабы посмотреть
            }
        }


        if (GameInfo.Players.Count <= 1) // если закончились деньги у всех
        {
            GameInfo.OnGetWinner(GameInfo.Players.ElementAt(0).Value);
            GameInfo.Players.Clear();
        }
    }
}