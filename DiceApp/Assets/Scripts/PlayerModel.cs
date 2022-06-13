using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerModel
{
    public string Name { get; set; }
    public int DiceCount { get; set; }
    public float CurrentMoney { get; set; }
    public bool IsWinner = false;
    public bool playGame = true;


    private float probability = 10;
    private float lastBet;
    public bool TheEnd = false;

    public event Action<float> OnUpdatedMoney;


    public PlayerModel(string name, int money, int diceCount)
    {
        Name = name;
        CurrentMoney = money;
        DiceCount = diceCount;
    }

    public void MakeBet(float bet)
    {
        if (CurrentMoney >= bet)
        {
            playGame = true;
            CurrentMoney -= bet;
            LeaveGame(bet);
        }
        else
        {
            playGame = false;
            Debug.Log(CurrentMoney);
        }

    
    }

    private void LeaveGame(float bet)
    {
        if (lastBet != bet)
        {
            var a = Random.Range(0, 10) * probability;
            if (a > 80 )
            {
                playGame = false;
                GameInfo.Result -= bet;
                Debug.Log(GameInfo.Result);
                CurrentMoney += bet;
                Debug.Log(CurrentMoney);
            }
            else
            {
                playGame = true;
            }
        }

        lastBet = bet;
    }

    public void SetLoser(float amount)
    {
        if (CurrentMoney > 0)
        {
            Debug.Log($" {Name} {DiceCount}  is loser number - Lose {amount} - currentMoney {CurrentMoney}");
        }
        else if (CurrentMoney <= 0)
        {
            TheEnd = true;
            Debug.Log("I am loser and bye bye");
        }
    }


    public void SetWinner(float amount)
    {
        CurrentMoney += amount;
        Debug.Log($" {Name} {DiceCount}  is winner number - Win {amount} - currentMoney {CurrentMoney}");
    }
}