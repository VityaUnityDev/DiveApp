using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBetCommand : AbstractCommand
{
    public override void Execute() =>   GameInfo.Result = GameInfo.PlayersInCurrentGame.Count * GameInfo.Bet;
}
