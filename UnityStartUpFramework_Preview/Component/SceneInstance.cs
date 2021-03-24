using System.Collections.Generic;
using UnityEngine;

// 씬을 나타낼 때 사용될 클래스
// 특정 씬에 대한 규칙을 정의합니다.
public class SceneInstance : MonoBehaviour
{
    [Header("플레이어 컨트롤러 프리팹을 전달합니다.")]
    [Tooltip("생성된 객체는 PlayerManager 형식 색체를 통해 얻을 수 있습니다.")]
    [SerializeField] protected PlayerControllerBase m_playerControllerPrefab;

    [Header("플레이어블 캐릭터 프리팹")]
    [Tooltip("생성된 객테를 PlayerControllerBase 형식 객체를 통해 얻을 수 있습니다.")]
    [SerializeField] protected PlayerableCharacterBase m_PlayerableCharacterPrefab;

    // 해당 씬에서 사용되는 캐릭터 객체들을 모두 저장합니다.
    public Dictionary<Collider, CharacterBase> allocatedCharacters { get; private set; } = 
        new Dictionary<Collider, CharacterBase>();

    protected virtual void Awake()
    {
        UnityStartupFramework.SceneManager.Instance.sceneInstance = this;

        PlayerManager.Instance.CreatePlayerController(
            m_playerControllerPrefab, m_PlayerableCharacterPrefab);
    }

    protected virtual void OnDestory()
    {
        UnityStartupFramework.SceneManager.Instance.sceneInstance = null;
    }

    // 씬에 캐릭터를 등록합니다.
    public void RegisterCharacter(CharacterBase newCharacter)
	{
        // 등록된 캐릭터라면 등록하지 않습니다.
        if (allocatedCharacters.ContainsKey(newCharacter.idCollider)) return;

        // 캐릭터를 등록합니다.
        allocatedCharacters.Add(newCharacter.idCollider, newCharacter);
    }

    // 씬에 등록된 캐릭터를 등록 해제합니다.
    public void UnRegisterCharacter(CharacterBase newCharacter)
	{
        // 등록되지 않은 캐릭터라면 등록 해제하지 않습니다.
        if (!allocatedCharacters.ContainsKey(newCharacter.idCollider)) return;

        // 등록 해제합니다.
        allocatedCharacters.Remove(newCharacter.idCollider);
    }

}
