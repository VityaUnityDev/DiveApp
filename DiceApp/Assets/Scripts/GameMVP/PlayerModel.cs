using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerModel
{
    public string Name { get; set; }
    public int DiceCount { get; set; }
    public float CurrentMoney { get; set; }
    public bool iAgreeWithBet { get; private set; }

    public bool IsWinner = false;
    public bool playGame = true;


    private float probability = 10;
    private float lastBet;
    public bool TheEnd = false;


    private int countPlayer;
    public event Action OnUpdatedMoney;


    public PlayerModel(string name, int money, int diceCount)
    {
        Name = name;
        CurrentMoney = money;
        DiceCount = diceCount;
    }

    public void MakeBet()
    {
        if (CurrentMoney >= GameInfo.Bet)
        {
            playGame = true;
            CurrentMoney -= GameInfo.Bet;
        }
        else
        {
            playGame = false;
        }
    }

    public void SolutionAboutGame()
    {
        iAgreeWithBet = true;

        countPlayer++;

        if (Math.Abs(lastBet - GameInfo.Bet) > 0 && CurrentMoney > GameInfo.Bet)
        {
            var a = Random.Range(0, 10);
            if (a < 8 || GameInfo.PlayersInCurrentGame.Count < 2 )
            {
                iAgreeWithBet = true;
            }
            else
            {
                iAgreeWithBet = false;
                GameInfo.Result -= GameInfo.Bet;
            }
        }
        
        lastBet = GameInfo.Bet;
    }

    public void SetLoser()
    {
        if (CurrentMoney <= 0)
        {
            TheEnd = true;
        }
    }


    public void SetWinner(float amount)
    {
        CurrentMoney += amount;
        Debug.Log($" {Name} {DiceCount}  is winner number - Win {amount} - currentMoney {CurrentMoney}");
        IsWinner = false;
    }
}