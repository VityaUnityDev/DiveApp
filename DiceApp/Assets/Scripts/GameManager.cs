using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Dice> dices;


    private int _result;
    private int _countEnter;
    private int _first;
    private int _max;
    private int _same;

    private CountPlayerInGameCommand _countPlayerInGameCommand;

    private CountBetCommand _countBetCommand;
    private MakeBetCommand _makeBetCommand;
    private EndGameCommand _endGameCommand;
    private CountMoneyCommand _countMoneyCommand;
    private HandOutMoneyCommand _moneyCommand;

    private void Awake()
    {
        _countPlayerInGameCommand = new CountPlayerInGameCommand();
        _countBetCommand = new CountBetCommand();
        _makeBetCommand = new MakeBetCommand();
        _endGameCommand = new EndGameCommand();
        _countMoneyCommand = new CountMoneyCommand();
        _moneyCommand = new HandOutMoneyCommand();
    }

    public void CountPlayerInGame()
    {
        _countPlayerInGameCommand.Execute();

        MakeBet();
        CountBetInGame();
        RollDices();
    }

    private void CountBetInGame() => _countBetCommand.Execute();

    private void MakeBet() => _makeBetCommand.Execute();


    private async void RollDices()
    {
        for (int i = 0; i < GameInfo.PlayersInCurrentGame.Count; i++)
        {
            int diceResult = 0;
            foreach (var dice in dices)
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


    private void FindMaxDiceNumber(int number)
    {
        _countEnter++;
        if (_countEnter == 1)
        {
            _first = number;
            _max = _first;
        }
        else
        {
            if (_first > number)
            {
                _first = _max;
            }
            else if (_first == number)
            {
                _same = number;
                _max = _same;
                _first = _max;
            }
            else if (_first < number)
            {
                _first = number;
                _max = number;
            }
        }

        StopCountMaxNumber();
    }

    private void StopCountMaxNumber()
    {
        if (GameInfo.PlayersInCurrentGame.Count == _countEnter)
        {
            GameInfo.winnerNumber = _max;
            FinishGame();
            _countEnter = 0;
        }
    }


    private void FinishGame()
    {
        _endGameCommand.Execute();
        CountMoney();
    }

    private void CountMoney()
    {
        _countMoneyCommand.Execute();
        HandOutMoney();
    }

    private void HandOutMoney() => _moneyCommand.Execute();
}