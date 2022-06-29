using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerModel
{
    public string Name { get; }
    public int DiceCount { get; set; }
    public float CurrentMoney { get;  set; }
    public bool IAgreeWithBet { get;  set; }
    public bool IsPlayerFinishedGame { get; private set; }
    public bool IsWinner;
    public PlayerState CurrentState;
    public event Action OnUpdatedMoney;


  
    private float _lastBet;
    private int _countPlayer;
    private float _probability = 10;
 


    public PlayerModel(string name, int money, int diceCount)
    {
        Name = name;
        CurrentMoney = money;
        DiceCount = diceCount;
    }

    public void MakeBet() =>   CurrentState = GetPlayerState();

    public void SolutionAboutGame()
    {
       // IAgreeWithBet = true;

        _countPlayer++;

        if (Math.Abs(_lastBet - GameInfo.Bet) > 0 && CurrentMoney > GameInfo.Bet)
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

    private PlayerState GetPlayerState()
    {
        if (IAgreeWithBet)
        {
            if (CurrentMoney > GameInfo.minBetInGame && CurrentMoney < GameInfo.Bet)
            {
                Debug.Log("---");
                return PlayerState.DontPlayerInCurrentGame;
            }

            else if (CurrentMoney >= GameInfo.Bet)
            {
                Debug.Log("+++");
                CurrentMoney -= GameInfo.Bet;
                return PlayerState.PlayCurrentGame;
            }

           else if (CurrentMoney < GameInfo.minBetInGame)
            {
                Debug.Log("+--");
                return PlayerState.GameOver;
            }
        }

        return PlayerState.None;
    }
    
}