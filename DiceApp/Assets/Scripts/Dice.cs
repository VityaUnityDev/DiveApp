using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Dice : MonoBehaviour
{
    
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    [SerializeField] private GameManager _gameManager;

    public BoxCollider2D _collider2D;

    
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    private void OnMouseDown()
    {
        _gameManager.StartGame();
    }


    public async Task<int> RollTheDice()
    {
        int randomDiceSide = 0;
        int finalSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(1, 6);
            rend.sprite = diceSides[randomDiceSide];
            await Task.Delay(50);
        }

        finalSide = randomDiceSide + 1;
        return finalSide;
    }
}