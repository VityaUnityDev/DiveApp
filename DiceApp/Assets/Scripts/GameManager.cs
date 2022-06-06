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
    [SerializeField] private List<Dice> _dices;
   // [SerializeField] private DiceSecond _secondDice;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private List<GameObject> placesInTable;
    [SerializeField] private GameObject playerPrefab;
    
    
    private PlayerPresenter _playerPresenter;
    private PlayerModel _playerModel;
    private int result;
    private int countEnter;
    private int first;
    private int max;
    private int same;


    public void StartGame()
    {
        int i = 0;
        foreach (var place in placesInTable.ToArray())
        {
            i++;
            var playerObject = Instantiate(playerPrefab, place.transform);
            var playerView = playerObject.GetComponent<PlayerView>();
            if (i == 1)
            {
                _playerModel = new PlayerModel("Vitya", 10, 0);
                _playerPresenter = new PlayerPresenter(playerView, _playerModel);
                Player mainPlayer = new Player(_playerModel, _playerPresenter);
                GameInfo.Players.Add(mainPlayer);
            }
            else
            {
                _playerModel = new PlayerModel("player" + i, 10, 0);
                _playerPresenter = new PlayerPresenter(playerView, _playerModel);
                Player player = new Player(_playerModel, _playerPresenter);
                GameInfo.Players.Add(player);
            }
        }
    }
    
    public void MakeBet(int count)
    {
        if (GameInfo.MadeBet == false)
        {
            for (int i = 1; i < GameInfo.Players.Count; i++)
            {
                MakeBetCommand makeBetCommand = new MakeBetCommand(GameInfo.Players[i]);
                makeBetCommand.Execute(true, count);
            }
        }
        else
        {
            for (int i = 0; i < GameInfo.Players.Count; i++)
            {
                MakeBetCommand makeBetCommand = new MakeBetCommand(GameInfo.Players[i]);
                makeBetCommand.Execute(true, count);
            }
        }

        RollDices();
    }

    private async void RollDices()
    {
        if (GameInfo.MadeBet == false)
        {
            for (int i = 1; i < GameInfo.Players.Count; i++)
            {
                foreach (var dice in _dices)
                {
                    var count = await  dice.RollTheDice();
                    result += count;

                }
                // var number = await _dice.RollTheDice();
                // var number2 = await _secondDice.RollTheDice();
                StartGameCommand startGameCommand = new StartGameCommand(GameInfo.Players[i]);
               // var result = number + number2;
             //   startGameCommand.Execute(result);
                await Task.Delay(1000);
               // FindMaxDiceNumber(result);
            }
        }
        else
        {
            for (int i = 0; i < GameInfo.Players.Count; i++)
            {
                // var number = await _dice.RollTheDice();
                // var number2 = await _secondDice.RollTheDice();
                StartGameCommand startGameCommand = new StartGameCommand(GameInfo.Players[i]);
             //   var result = number + number2;
               // startGameCommand.Execute(result);
                await Task.Delay(1000);
              //  FindMaxDiceNumber(result);
            }
        }
    }


    private void FindMaxDiceNumber(int result)
    {
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

        if (GameInfo.MadeBet)
        {
            if (GameInfo.Players.Count == countEnter)
            {
                FinishGame(max);
                countEnter = 0;
            }
        }
        else
        {
            if (GameInfo.Players.Count -1 == countEnter)
            {
                FinishGame(max);
                countEnter = 0;
            }
        }

      
    }

    private void FinishGame(int finishNumber)
    {
        Debug.Log(finishNumber);
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