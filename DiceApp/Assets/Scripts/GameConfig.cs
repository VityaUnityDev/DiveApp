using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameConfig),
    menuName = "/Configs/" + nameof(GameConfig))]
public class GameConfig : ScriptableObject
{
   // [SerializeField] private List<AlbumItemConfig> m_items;
    
    
        
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