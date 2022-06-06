using System;
using UnityEngine;

public class PlayerModel
{
    public string Name { get; set; }
    public int DiceCount { get; set; }
    public int CurrentMoney { get; set; }
    public bool IsWinner = false;
    public bool playGame = true;

    public bool TheEnd = false;
    
    public event Action<int> OnUpdatedMoney;


    public PlayerModel(string name, int money, int diceCount)
    {
        Name = name;
        CurrentMoney = money;
        DiceCount = diceCount;
    }

    public void MakeBet(int bet)
    {
        if (CurrentMoney >= bet)
        {
            playGame = true;
            CurrentMoney -= bet;
            Debug.Log(CurrentMoney);
        }
        else
        {
            playGame = false;
        }

    
    }

    public void SetLoser(int amount) 
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


    public void SetWinner(int amount)
    {
        CurrentMoney += amount;
        Debug.Log($" {Name} {DiceCount}  is winner number - Win {amount} - currentMoney {CurrentMoney}");
    }
}