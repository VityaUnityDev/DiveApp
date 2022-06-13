using UnityEngine;

public class EditPlayerCommand : MonoBehaviour
{
    private Player _player;

    public EditPlayerCommand(Player player)
    {
        _player = player;
    }

    public void Execute(Sprite image)
    {
        _player.playerPresenter.InstallPhoto(image);
    }
}