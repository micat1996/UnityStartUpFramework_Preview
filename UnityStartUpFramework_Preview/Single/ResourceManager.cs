
// 리소스와 관련된 내용을 관리하는 클래스
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed partial class ResourceManager :
    ManagerClassBase<ResourceManager>
{
    // 로드한 리소스를 모두 가지고 있을 Dictionary
    private Dictionary<string, Object> _LoadedResources = new Dictionary<string, Object>();
    private Object this[string resourceName] => _LoadedResources[resourceName];

    // json 파일을 저장하고 불러오기 위한 경로
    private string _JsonFolderPath;

    public override void InitializeManagerClass()
    {
        _JsonFolderPath = $"{Application.dataPath}/Resources/Json/";
    }

    // 특정한 형식으로 리소르를 로드하여 반환합니다.
    public T LoadResource<T>(string resourceName, string resourcePath = null)
        where T : class
    {
        // 이미 resourceName 이름으로 로드된 리소스가 존재한다면
        if (_LoadedResources.ContainsKey(resourceName))
            return this[resourceName] as T;

        else
        {
            //리소스 로드
            Object loadedResource = Resources.Load(resourcePath);

            // 만약 리소스 로드에 성공했다면
            if (loadedResource)
            {
                _LoadedResources.Add(resourceName, loadedResource);
                return loadedResource as T;
            }
            // 만약 리소스가 로드되지 않았다면
            else
            {
                // 에디터의 경우에만 에러 로그를 띄웁니다.
#if UNITY_EDITOR
                Debug.LogError($"{resourceName} is not loaded! (path : {resourcePath})");
#endif
                return null;
            }
        }
    }

    // Json 파일을 읽어 T 형식으로 반환합니다.
    public T LoadJson<T>(string filePath, out bool fileNotFound) where T : struct
    {
        string jsonData = null;

        try
        {
            jsonData = File.ReadAllText(_JsonFolderPath + filePath);
        }
        catch (DirectoryNotFoundException)
        {
            fileNotFound = true;
            return new T();
        }
        catch (FileNotFoundException)
        {
            fileNotFound = true;
            return new T();
        }

        fileNotFound = true;
        return JsonUtility.FromJson<T>(jsonData);
    }

    // Json 파일을 저장합니다.
    public void SaveJson<T>(T data, string filePath)
        where T : struct
    {
        Directory.CreateDirectory(_JsonFolderPath);

        string dataToString = JsonUtility.ToJson(data);
        File.WriteAllText(_JsonFolderPath + filePath, dataToString);
    }
}
