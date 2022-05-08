using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Dice _dice;
    [SerializeField] private CasinoFees _casinoFees;
    
    private PlayerModel _playerModel;

    public event Action<int> DiceCount;


    private int countEnter;
    private int first;
    private int max;
    private int same;


    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            _playerModel = new PlayerModel("opponent" + i, 100);
            Debug.Log("opponent" + i);
            GameInfo.Players.Add(_playerModel);
        }
    }


    public void MakeBet(int count)
    {
        for (int i = 0; i < GameInfo.Players.Count; i++)
        {
            MakeBetCommand makeBetCommand = new MakeBetCommand(GameInfo.Players[i]);
            makeBetCommand.Execute(true, count);
        }

        RollDices();
    }

    private async void RollDices()
    {
        for (int i = 0; i < GameInfo.Players.Count; i++)
        {
            
            var number = await _dice.RollTheDice();
            Debug.Log($"{GameInfo.Players[i].Name} {number} ");
            GameInfo.Players[i].diceCount = number;
            DiceCount?.Invoke(number);
            await Task.Delay(1000);
            FindMaxDiceNumber(GameInfo.Players[i].diceCount);
        }

       
    }


    private void FindMaxDiceNumber(int result)
    {
        Debug.Log(1);
        countEnter++;
        if (countEnter == 1)
        {
            first = result;
            max = first;
        }
        else
        {
            if (first > result)
            {
                first = max;
            }
            else if (first == result)
            {
                same = result;
                max = same;
                first = max;
            }
            else if (first < result)
            {
                first = result;
                max = result;
            }
        }

        if (GameInfo.Players.Count == countEnter)
        {
            FinishGame(max);
            countEnter = 0;
        }
    }

    private void FinishGame(int finishNumber)
    {
        EndGameCommand endGameCommand = new EndGameCommand();
        endGameCommand.Execute(finishNumber);
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