using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayer : MonoBehaviour
{
   

    public void Execute(Player _player)
    {
        Debug.Log(_player);
        GameInfo.Players.Remove(_player);
    }
}
