using System;
using System.Collections.Generic;
using System.Linq;

public static class GameInfo
{
    //обычный клас // публик класс для кадждого раунда //Базовый класс - > класс комнаты(подключать отключать чуваков, поставить кол-воб создавать раунды) -> классы раундов
    // awake
    public static Dictionary<string, Player> Players = new Dictionary<string, Player>();
    public static Dictionary<string, Player> PlayersInCurrentGame = new Dictionary<string, Player>();
    public static int CountWinner;
    public static int winnerNumber;
    public static float Bet;
    public static float Result;
    public static bool finishGame = false;
    

    public static float maxBetInGame;
    public static float minBetInGame;

    private static float bankFees = 0;


    // сделать instance ыsingalton

    public static event Action<Player> Winner;
    public static event Action<float> Fees;

    public static void OnGetWinner(Player player)
    {
        Winner?.Invoke(player);
    }

    public static void OnGetFees(float count)
    {
        bankFees += count;
        Fees?.Invoke(bankFees);
        MaxBetInGame();
        MinBetInGame();
    }

    private static void MaxBetInGame()
    {
        foreach (var pl in Players)
        {
            float max = 0;
            if (pl.Value._playerModel.CurrentMoney > max)
            {
                max = pl.Value._playerModel.CurrentMoney;
            }

            maxBetInGame = max;
        }
    }

    private static void MinBetInGame()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            float min = Players.Values.ElementAt(0)._playerModel.CurrentMoney;
            if (Players.Values.ElementAt(i)._playerModel.CurrentMoney < min)
            {
                min = Players.Values.ElementAt(i)._playerModel.CurrentMoney;
            }

            minBetInGame = min;
        }
    }


    public static void PlayerResult(Player player, int diceResult)
    {
        player._playerModel.DiceCount = diceResult;
        player.playerPresenter.DiceCount(diceResult);
    }
}