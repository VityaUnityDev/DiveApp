using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Dice> _dices;


    private int result;
    private int countEnter;
    private int first;
    private int max;
    private int same;


    public void CountPlayerInGame()
    {
        CountPlayerInGameCommand countPlayerInGameCommand = new CountPlayerInGameCommand();
        countPlayerInGameCommand.Execute();

        MakeBet();
        CountBetInGame();
        RollDices();
    }

    private void CountBetInGame()
    {
        CountBetCommand countBetCommand = new CountBetCommand();
        countBetCommand.Execute();
    }

    private void MakeBet()
    {
        MakeBetCommand makeBetCommand = new MakeBetCommand();
        makeBetCommand.Execute();
    }

    private async void RollDices()
    {
        for (int i = 0; i < GameInfo.PlayersInCurrentGame.Count; i++)
        {
            int diceResult = 0;
            foreach (var dice in _dices)
            {
                var count = await dice.RollTheDice();
                diceResult += count;
            }

            var player = GameInfo.PlayersInCurrentGame.ElementAt(i);
            GameInfo.PlayerResult(player.Value, diceResult);
            await Task.Delay(1000);
            FindMaxDiceNumber(diceResult);
        }
    }


    private void FindMaxDiceNumber(int res)
    {
        countEnter++;
        if (countEnter == 1)
        {
            first = res;
            max = first;
        }
        else
        {
            if (first > res)
            {
                first = max;
            }
            else if (first == res)
            {
                same = res;
                max = same;
                first = max;
            }
            else if (first < res)
            {
                first = res;
                max = res;
            }
        }

        StopCountMaxNumber();
    }

    private void StopCountMaxNumber()
    {
        if (GameInfo.PlayersInCurrentGame.Count == countEnter)
        {
            GameInfo.winnerNumber = max;
            FinishGame();
            countEnter = 0;
        }
    }


    private void FinishGame()
    {
        EndGameCommand endGameCommand = new EndGameCommand();
        endGameCommand.Execute();
        CountMoney();
    }

    private void CountMoney()
    {
        CountMoneyCommand countMoneyCommand = new CountMoneyCommand();
        countMoneyCommand.Execute();
        HandOutMoney();
    }


    private void HandOutMoney()
    {
        HandOutMoneyCommand moneyCommand = new HandOutMoneyCommand();
        moneyCommand.Execute();
    }
}