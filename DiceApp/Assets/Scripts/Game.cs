using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    
    public void CreateRoom(TypeGame _typeGame, int countPlayer)
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

    private void CreateGame()
    {
        
    }
}