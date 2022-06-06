using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Dice : MonoBehaviour
{
    
    [SerializeField] Sprite[] firstDice;
    private SpriteRenderer rend;
    [SerializeField] private GameManager _gameManager;

    public BoxCollider2D _collider2D;

    
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

    }
    
    public async Task<int> RollTheDice()
    {
        int randomDiceSide = 0;
        int finalSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(1, 6);
            rend.sprite = firstDice[randomDiceSide];
            await Task.Delay(50);
        }

        finalSide = randomDiceSide + 1;
        return finalSide;
    }
}