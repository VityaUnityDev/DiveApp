using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOutMoneyCommand : MonoBehaviour
{
    public void Execute()
    {
        if (GameInfo.MadeBet == false)
        {
            for (int i = 1; i < GameInfo.Players.Count; i++)
            {
                if (GameInfo.Players[i]._playerModel.IsWinner)
                {
                    GameInfo.Players[i]._playerModel.SetWinner(GameInfo.Result);
                    GameInfo.Players[i]._playerModel.IsWinner = false;
                    GameInfo.Players[i].playerPresenter.ChangeMoney(GameInfo.Players[i]._playerModel.CurrentMoney);
                    
                }
                else
                {
                    GameInfo.Players[i]._playerModel.SetLoser(0);
                    GameInfo.Players[i].playerPresenter.ChangeMoney(GameInfo.Players[i]._playerModel.CurrentMoney);
                    if (GameInfo.Players[i]._playerModel.TheEnd)
                    {
                        GameInfo.Players[i].playerPresenter.EndGame();
                        GameInfo.Players.RemoveAt(i);
                        i--;
                        if (GameInfo.Players.Count == 1)
                        {
                            var player = GameInfo.Players[0];
                            GameInfo.OnGetWinner(player);
                            GameInfo.Players.Clear();
                           
                        }
                    }
                }
            }

            GameInfo.finishGame = true;
        }

        else
        {
            for (int i = 0; i < GameInfo.Players.Count; i++)
            {
                if (GameInfo.Players[i]._playerModel.IsWinner)
                {
                    GameInfo.Players[i]._playerModel.SetWinner(GameInfo.Result);
                    GameInfo.Players[i]._playerModel.IsWinner = false;
                    GameInfo.Players[i].playerPresenter.ChangeMoney(GameInfo.Players[i]._playerModel.CurrentMoney);
                }
                else
                {
                    GameInfo.Players[i]._playerModel.SetLoser(0);
                    GameInfo.Players[i].playerPresenter.ChangeMoney(GameInfo.Players[i]._playerModel.CurrentMoney);
                    if (GameInfo.Players[i]._playerModel.TheEnd)
                    {
                        GameInfo.Players[i].playerPresenter.EndGame();
                        GameInfo.Players.RemoveAt(i);
                        i--;
                     
                    }
                }
            }

            GameInfo.finishGame = true;
        }
        
        if (GameInfo.Players.Count == 1)
        {
            var player = GameInfo.Players[0];
            GameInfo.OnGetWinner(player);
            GameInfo.Players[0].playerPresenter.EndGame();
            GameInfo.Players.Clear();
        }
    }
}