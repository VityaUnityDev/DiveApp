using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakeBetCommand : AbstractCommand
{
    private MoneyState _moneyState;

    public override void Execute()
    {
        GameInfo.finishGame = false;

        for (int i = 0; i < GameInfo.PlayersInCurrentGame.Values.Count; i++)
        {
            var player = GameInfo.PlayersInCurrentGame.ElementAt(i).Value;
            player._playerModel.MakeBet();

            switch (player._playerModel.CurrentState)
            {
                case MoneyState.PlayCurrentGame:
                    UpdateMoney(player);
                    break;
                case MoneyState.DontPlayerInCurrentGame:
                    RemovePlayerFromCurrentGame(player);
                    i--;
                    break;
                case MoneyState.GameOver:
                    RemovePlayerFromGame(player);
                    i--;
                    break;
            }
        }
    }

    private void RemovePlayerFromCurrentGame(Player player) => GameInfo.PlayersInCurrentGame.Remove(player.Name);

    private void RemovePlayerFromGame(Player player)
    {
        player.playerPresenter.EndGame();
        GameInfo.PlayersInCurrentGame.Remove(player.Name);
        GameInfo.Players.Remove(player.Name);
    }

    private void UpdateMoney(Player player)
    {
        player._playerModel.CurrentMoney -= GameInfo.Bet;
        player.playerPresenter.ChangeMoney();
    }
}