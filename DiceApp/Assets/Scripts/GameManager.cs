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
    private int count = 0;
    [SerializeField] private Dice _dice;

    [SerializeField] private TMP_Text vityaDiceCount;
    [SerializeField] private List<TMP_Text> enemyDiceCount;
    [SerializeField] private TMP_Text vityaCurrentMoney;
    [SerializeField] private List<TMP_Text> enemyCurrentMoney;
    [SerializeField] private TMP_Text casinioAmount;

    [SerializeField] private int percentForCasino;
    private float CasinoAmount;

    public int Bet = 10;
    public int amount;


    private PlayerModel mainPlayer;
    private List<PlayerModel> opponents = new List<PlayerModel>();
    private PlayerPresenter _playerPresenter;
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    public event Action<PlayerModel> Player;

    public event Action<int> maxNumber;


    private int countEnter;
    private int first;
    private int max;
    private int same;


    private void Awake()
    {
        maxNumber += FinishGame;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                Debug.Log(1);
                _playerModel = new PlayerModel("vitya", 100);
                mainPlayer = _playerModel;
                // Player?.Invoke(mainPlayer);
            }
            else
            {
                Debug.Log(2);
                _playerModel = new PlayerModel("opponent" + i, 100);
                opponents.Add(_playerModel);

                //   Player?.Invoke(_playerModel);
            }
        }
    }

    private void Start()
    {
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        vityaCurrentMoney.text = mainPlayer.CurrentMoney.ToString();
        for (int i = 0; i < opponents.Count; i++)
        {
            enemyCurrentMoney[i].text = opponents[i].CurrentMoney.ToString();
        }
    }

    private void UpdateDiceCount()
    {
        vityaDiceCount.text = mainPlayer.diceCount.ToString();
        for (int i = 0; i < opponents.Count; i++)
        {
            enemyDiceCount[i].text = opponents[i].diceCount.ToString();
        }
    }

    private void MakeBet(PlayerModel model)
    {
        model.MakeBet(Bet);
        amount += Bet;
        Debug.Log(amount);
    }

    private void MakeNull()
    {
        mainPlayer.diceCount = 0;
        for (int i = 0; i < opponents.Count; i++)
        {
            opponents[i].diceCount = 0;
        }

        UpdateDiceCount();
    }


    public async void StartGame()
    {
        MakeNull();
        amount = 0;
        MakeBet(mainPlayer);
        mainPlayer.IsWinner = false;
        mainPlayer.diceCount = 0;
        countEnter = 0;
        first = 0;
        max = 0;
        same = 0;
        var a = await _dice.RollTheDice();
        Debug.Log($"{mainPlayer.Name} {a} ");
        mainPlayer.diceCount = a;
        vityaDiceCount.text = a.ToString();
        Result(mainPlayer.diceCount);
        await Task.Delay(1000);


        OpponentGame();
    }

    private async void OpponentGame()
    {
        for (int i = 0; i < opponents.Count; i++)
        {
            MakeBet(opponents[i]);
            opponents[i].IsWinner = false;
            opponents[i].diceCount = 0;
            var a = await _dice.RollTheDice();
            Debug.Log($"{opponents[i].Name} {a} ");
            opponents[i].diceCount = a;
            enemyDiceCount[i].text = a.ToString();
            Result(opponents[i].diceCount);
            await Task.Delay(1000);
        }

        UpdateMoney();
    }


    private void Result(int result)
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

        if (opponents.Count + 1 == countEnter)
        {
            Debug.Log("Max-" + max);
            maxNumber?.Invoke(max);
        }
    }

    private void FinishGame(int finishNumber)
    {
        int countWinner = 0;
        if (mainPlayer.diceCount == finishNumber)
        {
            mainPlayer.IsWinner = true;
            countWinner++;
        }

        for (int i = 0; i < opponents.Count; i++)
        {
            if (opponents[i].diceCount == finishNumber)
            {
                opponents[i].IsWinner = true;
                countWinner++;
            }
        }

        Debug.Log("Count winner" + countWinner);
        EndGame(countWinner);
    }

    private void EndGame(int countWinner)
    {
       
        var fees = amount * percentForCasino / 100;
        CasinoAmount += fees;
        casinioAmount.text = CasinoAmount.ToString();
        var result = amount - fees;

        if (countWinner > 1)
        {
            result = amount / countWinner;
        }

        
        if (mainPlayer.IsWinner)
        {
            mainPlayer.SetWinner(result);
        }
        else
        {
            mainPlayer.SetLoser(Bet);
        }

        for (int i = 0; i < opponents.Count; i++)
        {
            if (opponents[i].IsWinner)
            {
                opponents[i].SetWinner(result);
            }
            else
            {
                opponents[i].SetLoser(Bet);
            }
        }

        UpdateMoney();
    }


    private void OnDisable()
    {
        maxNumber -= FinishGame;
    }
}