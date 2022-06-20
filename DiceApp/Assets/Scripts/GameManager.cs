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

    private CountPlayerInGameCommand countPlayerInGameCommand;

    private CountBetCommand countBetCommand;
    private MakeBetCommand makeBetCommand;
    private EndGameCommand endGameCommand;
    private CountMoneyCommand countMoneyCommand;
    private HandOutMoneyCommand moneyCommand;

    private void Awake()
    {
        countPlayerInGameCommand = new CountPlayerInGameCommand();
        countBetCommand = new CountBetCommand();
        makeBetCommand = new MakeBetCommand();
        endGameCommand = new EndGameCommand();
        countMoneyCommand = new CountMoneyCommand();
        moneyCommand = new HandOutMoneyCommand();
    }

    public void CountPlayerInGame()
    {
        countPlayerInGameCommand.Execute();

        MakeBet();
        CountBetInGame();
        RollDices();
    }

    private void CountBetInGame() => countBetCommand.Execute();
    private void MakeBet() => makeBetCommand.Execute();

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
        endGameCommand.Execute();
        CountMoney();
    }

    private void CountMoney()
    {
        countMoneyCommand.Execute();
        HandOutMoney();
    }
    
    private void HandOutMoney() => moneyCommand.Execute();
}