using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
	// 캐릭터를 나타낼 컬리전
	public Collider idCollider { get; protected set; }

	// 해당 객체가 어떤 피해를 입었을 경우 호출되는 대리자
	public System.Action<CharacterBase /*damageCauser*/, Component /*componentCauser*/, float /*damage*/> OnTakeAnyDamage;

	// 해당 객체에게 대미지를 가합니다.
	public void ApplyDamage(CharacterBase damageCauser, Component componentCauser, float damage)
	{
		OnTakeAnyDamage?.Invoke(damageCauser, componentCauser, damage);
	}

	protected virtual void Start()
	{
		// 캐릭터를 등록합니다.
		UnityStartupFramework.SceneManager.Instance.sceneInstance?.RegisterCharacter(this);
	}

	protected virtual void OnDestroy()
	{
		// 캐릭터 등록 해제
		UnityStartupFramework.SceneManager.Instance.sceneInstance?.UnRegisterCharacter(this);
	}


}