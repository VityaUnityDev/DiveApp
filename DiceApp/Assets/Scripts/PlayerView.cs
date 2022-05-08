using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Button Bet10;
    [SerializeField] private TMP_Text bank;

    [SerializeField] private List<TMP_Text> dices;
    [SerializeField] private List<TMP_Text> currentMoney;
    public event Action<int> OnMadeBet;

    private int enterDices = 0;
    private int enterMoney = 0;

    private void Start()
    {
        Bet10.onClick.AddListener(() => MakeBet10(10));
    }

    private void MakeBet10(int bet)
    {
        OnMadeBet?.Invoke(bet);
        var amount = bet * 4;
        bank.text = amount.ToString();
        ClearResult();
    }

    private void ClearResult()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            var a = 0;
            dices[i].text = a.ToString();
            enterDices = 0;
            enterMoney = 0;

        }
    }

    public void InfoAboutDice(int number)
    {
        enterDices++;
        dices[enterDices - 1].text = number.ToString();
    }

    public void InfoAboutMoney(int money)
    {
        enterMoney++;
        currentMoney[enterMoney - 1].text = money.ToString();
    }
}