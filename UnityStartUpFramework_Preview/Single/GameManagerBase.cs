using System.Collections.Generic;
using UnityEngine;

//게임 전체를 관리할 객체를 만들기 위한 클래스
//관리할 대상의 종류에 따라 SubManager 객체를 가지며 관리를 세분화 한다.
public abstract class GameManagerBase : MonoBehaviour
{
    // 게임 시작 시 SceneChanged 가
    [SerializeField] private bool _BeginSceneChangedMethodCalling = true;

    // 사용되는 GameManagerBase 객체를 나타냅니다.
    private static GameManagerBase _GameManager;

    private List<IManagerClass> _ManagerClasses;

    public static GameManagerBase GetGameManager()
    {
        if (!_GameManager)
        {
            _GameManager = GameObject.Find("GameManager").GetComponent<GameManagerBase>();

            _GameManager._ManagerClasses = new List<IManagerClass>();

            // 하위 매니저 클래스 초기화
            _GameManager.InitializeManagerClasses();

            if (_GameManager._BeginSceneChangedMethodCalling)
                _GameManager.SceneChanged(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        }

        return _GameManager;
    }

    // 서브 매니저 클래스를 등록합니다.
    /// - 등록한 매니저 컴포넌트를 소유하는 오브젝트는 GameManagerBase 컴포넌트를 갖는
    ///   오브젝트 하위로 등록되어야합니다.
    protected T RegisterManagerClass<T>() where T : IManagerClass
    {
        // GameManager 오브젝트 하위에서 찾습니다.
        T manager = transform.GetComponentInChildren<T>();
        /// - GetComponentInChildren<T>() : T 형식의 컴포넌트를 자신과 자식 오브젝트에서 찾습니다.

        // 서브 매니저 객체 초기화
        manager.InitializeManagerClass();

        // 리스트에 서브 매니저 객체 등록
        _ManagerClasses.Add(manager);

        return manager;
    }

    // 등록된 서브 매니저 객체를 얻습니다.
    public static T GetManagerClass<T>() where T : class, IManagerClass
    {
        IManagerClass managerClass = GetGameManager()._ManagerClasses.Find(
            (IManagerClass type) => type.GetType() == typeof(T));

        return managerClass as T;
    }

    protected virtual void Awake()
    {
        gameObject.name = "GameManager";
        
        // 게임 매니저 중복 생성 방지
        if (GetGameManager() != this && GetGameManager() != null)
        { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    // GameManager 객체를 초기화합니다.
    /// - RegisterManagerClass 로 매니저 클래스를 등록합니다.
    protected virtual void InitializeManagerClasses()
    {
        RegisterManagerClass<ResourceManager>();
        RegisterManagerClass<UnityStartupFramework.SceneManager>();
        RegisterManagerClass<PlayerManager>();
    }

    // 씬 변경을 시작할 때 호출되어야 합니다.
    public void SceneLoadStarted()
    {
        foreach (var manager in _ManagerClasses)
            manager.OnSceneLoadStarted();
    }

    public void SceneChanged(string newSceneName)
    {
        foreach (var manager in _ManagerClasses)
            manager.OnSceneChanged(newSceneName);
    }


}
