using System;
using System.Collections.Generic;

public static class GameInfo
{
    public static List<Player> Players = new List<Player>();
    public static int CountWinner;
    public static float Bet;
    public static float Result;
    public static bool finishGame = false;
    public static bool MadeBet;

    private static float bankFees = 0;

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
    }
}