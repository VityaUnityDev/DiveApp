using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    [SerializeField] private int countPlayers;

    private Game _game;

    public void CreateGame(TypeGame _typeGame)
    {
        var objToSpawn = new GameObject("Cool GameObject made from Code"); 
        var a = objToSpawn.AddComponent<Game>();
       // a.CreateNewGame();
        
     //    _game = new Game(_typeGame, countPlayers);
     // //   var game = objToSpawn.AddComponent<Game>();
    
     //    game.CreateNewGame();

    }


    // public AlbumItemsConfig() 
    // {
    //     m_isAlbum = true;
    // }
    //
    // public List<AlbumItemConfig> AlbumItemConfigs => m_items;
    //
    //
    //
    //
    // public AlbumItemConfig GetConfig(int index)
    // {
    //     return m_items.First(x => x.OpenIndex == index);
    // }
    //     
    // public IEnumerable<AlbumItemConfig> GetAvailableConfigs(int index)
    // {
    //     return m_items.Where(item => item.OpenIndex >= index);
    // }
}