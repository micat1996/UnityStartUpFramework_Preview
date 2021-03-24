
// 플레이어와 관련된 내용을 관리하는 클래스
public sealed partial class PlayerManager :
    ManagerClassBase<PlayerManager>
{
    public PlayerControllerBase playerController { get; private set; }

    public void CreatePlayerController(PlayerControllerBase controllerPrefab,
        PlayerableCharacterBase playerableCharacterPrefabs)
    {
        playerController = (controllerPrefab == null) ?
            null : Instantiate(controllerPrefab);

        playerController?.CreatePlayerableCharater(playerableCharacterPrefabs);
    }
}
