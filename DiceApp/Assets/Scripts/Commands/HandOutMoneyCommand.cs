using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandOutMoneyCommand : AbstractCommand
{
    public override void Execute()
    {
        for (int i = 0; i < GameInfo.PlayersInCurrentGame.Values.Count; i++)
        {
            var player = GameInfo.PlayersInCurrentGame.ElementAt(i).Value;
            
            UpdatePlayerInfo(player);
            
            if (player._playerModel.IsPlayerFinishedGame)
            {
                RemovePlayerFromRoom(player);
                i--;
                RoomWasFinished();
            }
        }
        
        GameInfo.PlayersInCurrentGame.Clear();
        GameInfo.finishGame = true;
    }

    private void UpdatePlayerInfo(Player player)
    {
        var pl = player._playerModel;
        if (pl.IsWinner)
        {
           pl.SetWinner();
        }
        else
        {
          pl.SetLoser();
        }

        player.playerPresenter.ChangeMoney();
    }

    private void RoomWasFinished()
    {
        if (GameInfo.Players.Count == 1)
        {
            var player = GameInfo.Players.Values.ElementAt(0);
            GameInfo.OnGetWinner(player);
            player.playerPresenter.EndGame();
            GameInfo.PlayersInCurrentGame.Clear();
            GameInfo.Players.Clear();
            
        }
    }

    private void RemovePlayerFromRoom(Player player)
    {
        player.playerPresenter.EndGame();
        GameInfo.PlayersInCurrentGame.Remove(player.Name);
        GameInfo.Players.Remove(player.Name);
    }
}