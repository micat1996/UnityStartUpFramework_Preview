using UnityEngine;

// 서브 매니저 클래스의 기반 형태가 될 클래스
public abstract class ManagerClassBase<T> : MonoBehaviour,
	IManagerClass
	where T : class, IManagerClass
{
	// 찾은 서브 매니저 객체를 참조합니다.
	private static T _Instance;

	// T 형식의 서브 매니저 객체에 대한 읽기 전용 프로퍼티입니다.
	public static T Instance => _Instance ??
		(_Instance = GameManagerBase.GetManagerClass<T>());

	// 서브 매니저 클래스 초기화에 대한 내용을 이 곳에 작성합니다.
	public virtual void InitializeManagerClass() { }

	// 씬 변경 시작 시 호출되는 메서드
	public virtual void OnSceneLoadStarted() { }

	// 씬 변경 후 호출되는 메서드
	public virtual void OnSceneChanged(string newSceneName) { }

}