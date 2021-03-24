
// 서브 매니저 클래스에서 구현해야 하는 인터페이스입니다.
public interface IManagerClass
{
	// 서브 매니저 클래스가 초기화될 때 호출되는 메서드
	void InitializeManagerClass();

	// 씬 변경 시작 시 호출되도록 할 메서드
	void OnSceneLoadStarted();

	// 씬 변경 후 호출되도록 할 메서드
	void OnSceneChanged(string newSceneName);

}