using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBetCommand : MonoBehaviour
{
    public void Execute(int fromCountNumber)
    {
        GameInfo.finishGame = false;

        for (int i = fromCountNumber; i < GameInfo.Players.Count; i++)
        {
            GameInfo.Players[i]._playerModel.MakeBet(GameInfo.Bet);
            if (GameInfo.Players[i]._playerModel.playGame )
            {
                GameInfo.Players[i].playerPresenter.ChangeMoney(GameInfo.Players[i]._playerModel.CurrentMoney);
                GameInfo.Players[i].playerPresenter.ClearInfoAboutDice();
            }
            
            else
            {
                GameInfo.Players[i].playerPresenter.EndGame();
                GameInfo.Players.Remove(GameInfo.Players[i]);
                i--;
                Debug.Log("Player COunt" + GameInfo.Players.Count); // не унитожаются префабы посмотреть
            }
        }
        
        if( GameInfo.Players.Count <= 1 )
        {
            GameInfo.OnGetWinner(GameInfo.Players[0]);
            GameInfo.Players.Clear();
        }
    }
}