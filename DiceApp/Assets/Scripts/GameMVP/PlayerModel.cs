using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerModel
{
    public string Name { get; }
    public int DiceCount { get; set; }
    public float CurrentMoney { get; set; }
    public bool IAgreeWithBet { get; set; }
    public bool IsPlayerFinishedGame { get; private set; }
    public bool IsWinner;
    
    public MoneyState CurrentState;
    public event Action OnUpdatedMoney;


    private float _lastBet;
    private int _countPlayer;
    private float _probability = 10;

    private PlayerMoneyState _playerMoneyState;


    public PlayerModel(string name, int money, int diceCount)
    {
        Name = name;
        CurrentMoney = money;
        DiceCount = diceCount;
    }

    public void MakeBet()
    {
        _playerMoneyState = new PlayerMoneyState(CurrentMoney);
        CurrentState = _playerMoneyState.MoneyState;
    }

    public void SolutionAboutGame()
    {
        if (Math.Abs(_lastBet - GameInfo.Bet) > 0 && CurrentState == MoneyState.PlayCurrentGame)
        {
            var a = Random.Range(0, 10);
            if (a < 8 || GameInfo.PlayersInCurrentGame.Count < 2)
            {
                IAgreeWithBet = true;
            }
            else
            {
                IAgreeWithBet = false;
                GameInfo.Result -= GameInfo.Bet;
            }
        }
        else
        {
            IAgreeWithBet = true;
        }

        _lastBet = GameInfo.Bet;
    }

    public void SetLoser()
    {
        if (CurrentMoney <= 0)
        {
            IsPlayerFinishedGame = true;
        }
    }


    public void SetWinner()
    {
        CurrentMoney += GameInfo.Result;
        Debug.Log($" {Name} {DiceCount}  is winner number - Win {GameInfo.Result} - currentMoney {CurrentMoney}");
        IsWinner = false;
    }
    
}