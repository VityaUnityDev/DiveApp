using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject photo;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text currentMoney;
    [SerializeField] private TMP_Text currentDice;
    

    public void ChangeMoney(int count)
    {
        currentMoney.text = count.ToString();
    }

    public void DiceCount(int count)
    {
        currentDice.text = count.ToString();
    }

    public void Name(string player)
    {
        name.text = player.ToString(); 
    }

    public void EndGame()
    {
       name.gameObject.SetActive(false); 
       currentMoney.gameObject.SetActive(false); 
       currentDice.gameObject.SetActive(false);
    }
    
    public void ClearDice()
    {
        var update = 0;
        currentDice.text = update.ToString();
    }

  




}