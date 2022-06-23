using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private TypeGame _typeGame;
    private int countPlayer;

    public Game(TypeGame TypeGame, int CountPlayer)
    {
        _typeGame = TypeGame;
        countPlayer = CountPlayer;
        CreateNewGame();
    }


  

    public void CreateNewGame()
    {
        switch (_typeGame) 
        {
            case TypeGame.MaxBet5:
                new CreateRoom(5, 1, countPlayer);
                break;
            case TypeGame.MaxBet10:
                new CreateRoom(10, 5, countPlayer);
                break;
        }
    }
}