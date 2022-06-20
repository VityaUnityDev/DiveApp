using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> placesInTable;
    [SerializeField] private List<Sprite> playerIcons;
    [SerializeField] private PlayerView playerPrefab;


    private PlayerPresenter _playerPresenter;
    private PlayerModel _playerModel;

    public void StartGame()
    {
        int i = 0;
        foreach (var place in placesInTable.ToArray())
        {
            i++;
            var playerObject = Instantiate(playerPrefab, place.transform);

            if (i == 1)
            {
                CreatePlayer(playerObject, "Vitya", 10, 0);
            }
            else
            {
                CreatePlayer(playerObject, "player" + i, 10, 0);
            }
        }
    }

    private void CreatePlayer(PlayerView playerView, string playerName, int money, int diceCount)
    {
        _playerModel = new PlayerModel(playerName, money, diceCount);
        _playerPresenter = new PlayerPresenter(playerView, _playerModel);
        Player player = new Player(_playerModel, _playerPresenter);
        GameInfo.Players.Add(player._playerModel.Name, player);
        
        
        
        for (int i = 0; i < GameInfo.Players.Count; i++)
        {
            var pl = GameInfo.Players.ElementAt(i);
            EditPlayer(pl.Value, playerIcons[i]);
        }
    }

    public void DestroyPlayers()
    {
        foreach (var place in placesInTable)
        {
         var pl =   place.GetComponentInChildren<PlayerView>();
         Destroy(pl.gameObject);

        }
    }

    private void EditPlayer(Player player, Sprite image)
    {
        EditPlayerCommand editPlayerCommand = new EditPlayerCommand(player);
        editPlayerCommand.Execute(image);
    }
}