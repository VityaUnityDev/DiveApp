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
    [SerializeField] private List<GameObject> placesInTable;
    [SerializeField] private List<Sprite> playerIcons;
    [SerializeField] private PlayerView playerPrefab;


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

            if (i == 1)
            {
                CreatePlayer(playerObject, "Vitya", 10, 0);
            }
            else
            {
                CreatePlayer(playerObject, "player" + i, 10, 0);
            }
        }
    }

    private void CreatePlayer(PlayerView playerView, string playerName, int money, int diceCount)
    {
        _playerModel = new PlayerModel(playerName, money, diceCount);
        _playerPresenter = new PlayerPresenter(playerView, _playerModel);
        Player player = new Player(_playerModel, _playerPresenter);
        GameInfo.Players.Add(player);
        EditPlayer(player, playerIcons[0]);
        playerIcons.RemoveAt(0);
    }

    public void CountPlayerInGame() // для mvp чтобы выкинуть главного игрока со стола 
    {
        if (GameInfo.MadeBet == false)
        {
            MakeBet(1);
            RollDices(1);
        }
        else
        {
            MakeBet(0);
            RollDices(0);
        }
    }

    private void MakeBet(int fromCountNumber) // передаю значение с которого начать отсчет
    {
        MakeBetCommand makeBetCommand = new MakeBetCommand();
        makeBetCommand.Execute(fromCountNumber);
    }

    private async void RollDices(int fromCountNumber)
    {
        for (int i = fromCountNumber; i < GameInfo.Players.Count; i++)
        {
            int diceResult = 0;
            foreach (var dice in _dices)
            {
                var count = await dice.RollTheDice();
                diceResult += count;
            }

            StartGameCommand startGameCommand = new StartGameCommand(GameInfo.Players[i]);
            startGameCommand.Execute(diceResult);
            await Task.Delay(1000);
            FindMaxDiceNumber(diceResult, fromCountNumber);
        }
    }


    private void FindMaxDiceNumber(int result, int fromCountNumber)
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

        if (GameInfo.Players.Count - fromCountNumber ==
            countEnter) // отнимем 0 или 1 тем самым узнаем сколько максимальных чиспел
        {
            FinishGame(max, fromCountNumber);
            countEnter = 0;
        }
    }


    private void EditPlayer(Player player, Sprite image)
    {
        EditPlayerCommand editPlayerCommand = new EditPlayerCommand(player);
        editPlayerCommand.Execute(image);
    }

    private void FinishGame(int finishNumber, int fromCountNumber)
    {
        EndGameCommand endGameCommand = new EndGameCommand();
        endGameCommand.Execute(finishNumber, fromCountNumber);
        CountMoney(fromCountNumber);
    }

    private void CountMoney(int fromCountNumber)
    {
        CountMoneyCommand countMoneyCommand = new CountMoneyCommand();
        countMoneyCommand.Execute(fromCountNumber);
        HandOutMoney(fromCountNumber);
    }


    private void HandOutMoney(int fromCountNumber)
    {
        HandOutMoneyCommand moneyCommand = new HandOutMoneyCommand();
        moneyCommand.Execute(fromCountNumber);
    }
}