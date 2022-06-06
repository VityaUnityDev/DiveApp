using System;
using System.Collections.Generic;

public static class GameInfo
{
    public static List<Player> Players = new List<Player>();
    public static int CountWinner;
    public static int Bet;
    public static int Result;
    public static bool finishGame = false;
    public static bool MadeBet;

    public static event Action<Player> Winner;
    public static event Action<int> Fees;

    public static void OnGetWinner(Player player)
    {
        Winner?.Invoke(player);
    } 
    public static void OnGetFees(int count)
    {
        Fees?.Invoke(count);
    }
    
    



}
