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

            switch (player._playerModel.currentState)
            {
                case PlayerState.PlayCurrentGame: 
                    player.playerPresenter.ChangeMoney();
                    break;
                case PlayerState.DontPlayerInCurrentGame: 
                    RemovePlayerFromGame(player);
                    i--;
                    break; 
                case PlayerState.GameOver:   
                    RemovePlayerFromCurrentGame(player);
                    i--;
                    break;
                    
            }
            
            if (GameInfo.Players.Count == 1)
            {
                var pl = GameInfo.Players.ElementAt(0).Value;
                GameInfo.OnGetWinner(pl);
                GameInfo.Players.Clear();
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
}