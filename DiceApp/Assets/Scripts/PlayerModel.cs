using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
    public string Name { get; set; }
    public int diceCount { get;  set; }
    public int CurrentMoney { get; private set; }
    public bool IsWinner = false;

    public event Action<int> ChangeCurrentMoney;
    public event Action<PlayerModel> EndGame;
    public event Action Win;
    
    public event Action<int> ChangeMoney;


    public PlayerModel(string name, int money)
    {
        Name = name;
        CurrentMoney = money;
    }

    public void MakeBet(int bet)
    {
        CurrentMoney -= bet;
        Debug.Log(CurrentMoney);
    }

    public void SetLoser(int amount) // перерабоать под пкомментированное условие
    {
        if (CurrentMoney > 0)
        {
            CurrentMoney -= amount;
            Debug.Log($" {Name} {diceCount}  is loser number - Lose {amount} - currentMoney {CurrentMoney}");
            ChangeCurrentMoney?.Invoke(CurrentMoney);
        }
        else
        {
            EndGame?.Invoke(this);
            Debug.Log("I am loser and bye bye");
        }

    }

    
    public void SetWinner(int amount)
    {
        CurrentMoney += amount;
        Debug.Log($" {Name} {diceCount}  is winner number - Win { amount} - currentMoney {CurrentMoney}");
        ChangeCurrentMoney?.Invoke(CurrentMoney);
    }
    


}
