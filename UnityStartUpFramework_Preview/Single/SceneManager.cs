
namespace UnityStartupFramework
{
	public sealed partial class SceneManager :
		ManagerClassBase<SceneManager>
	{
		// 현재 씬
		public string currentSceneName { get; private set; }

		// 로드할 씬
		public string nextSceneName { get; private set; } = "TitleScene";

		// 현재 사용중인 씬 인스턴스
		public SceneInstance sceneInstance { get; set; }

		public void LoadScene(string nextScene)
        {
			nextSceneName = nextScene;
			GameManagerBase.GetGameManager().SceneLoadStarted();
			UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
        }

        public override void OnSceneChanged(string newSceneName)
        {
            currentSceneName = newSceneName;
        }

    }
}
