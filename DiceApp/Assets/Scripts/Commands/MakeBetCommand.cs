using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakeBetCommand : AbstractCommand
{
    private PlayerState _playerState;

    public override void Execute()
    {
        GameInfo.finishGame = false;

        for (int i = 0; i < GameInfo.PlayersInCurrentGame.Values.Count; i++)
        {
            var player = GameInfo.PlayersInCurrentGame.ElementAt(i).Value;
            player._playerModel.MakeBet();

            switch (player._playerModel.CurrentState)
            {
                case PlayerState.PlayCurrentGame:
                    UpdateMoney(player);
                    break;
                case PlayerState.DontPlayerInCurrentGame:
                    RemovePlayerFromCurrentGame(player);
                    i--;
                    break;
                case PlayerState.GameOver:
                    RemovePlayerFromGame(player);
                    i--;
                    break;
            }

          

            // if (GameInfo.PlayersInCurrentGame.ElementAt(i).Value._playerModel.playGame)
            // {
            //  
            // }
            //
            // else if (GameInfo.PlayersInCurrentGame.ElementAt(i).Value._playerModel.CurrentMoney < GameInfo.minBet)
            // {
            //   
            // }
            //
            // else
            // {
            //   
            // }
        }
    }

    private void RemovePlayerFromCurrentGame(Player player) => GameInfo.PlayersInCurrentGame.Remove(player.Name);

    private void RemovePlayerFromGame(Player player)
    {
        player.playerPresenter.EndGame();
        GameInfo.PlayersInCurrentGame.Remove(player.Name);
        GameInfo.Players.Remove(player.Name);
    }

    private void UpdateMoney(Player player) => player.playerPresenter.ChangeMoney();
    
   
}