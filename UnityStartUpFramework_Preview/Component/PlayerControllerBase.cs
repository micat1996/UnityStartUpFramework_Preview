
// 모든 플레이어 캐릭터에 적용될 내용을 정의합니다.
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerBase : MonoBehaviour
{
    [Header("기본으로 생성될 ScreenInstance")]
    [SerializeField] private ScreenInstance _ScreenInstancePrefab;

    [Header("EventSystem Prefab")]
    [SerializeField] private GameObject _EventSystem;

    public PlayerableCharacterBase playerableCharacter { get; private set; }
    public ScreenInstance screenInstance { get; private set; }

    protected virtual void CreateUICanvas()
    {
        screenInstance = Instantiate(_ScreenInstancePrefab);
        Instantiate(_EventSystem);
    }

    // 캐릭터를 생성합니다.
    public void CreatePlayerableCharater(PlayerableCharacterBase prefab)
    {
        playerableCharacter = (prefab == null) ? null : Instantiate(prefab);

        // 캐릭터와 연결
        playerableCharacter.OnControllerConnected(this);

        CreateUICanvas();
    }

    // 캐릭터를 비웁니다.
    public void ClearPlayerableCharacter() => 
        playerableCharacter = null;


    // 회전을 설정합니다.
    public void SetControlRotation(Vector3 eulerAngle) =>
        transform.eulerAngles = eulerAngle;
    public void SetControlRotation(Quaternion quaternion) =>
        transform.rotation = quaternion;

    public void AddPitch(float value)
    {
        var eulerAngle = GetControlRotationToEuler();
        eulerAngle.x += value;
        SetControlRotation(eulerAngle);
    }

    public void AddYawAngle(float value)
    {
        var eulerAngle = GetControlRotationToEuler();
        eulerAngle.y += value;
        SetControlRotation(eulerAngle);
    }

    public void AddRollAngle(float value)
    {
        var eulerAngle = GetControlRotationToEuler();
        eulerAngle.z += value;
        SetControlRotation(eulerAngle);
    }



    // 회전을 얻습니다.
    public Vector3 GetControlRotationToEuler() => transform.eulerAngles;
    public Quaternion GetControlRotationToQuaternion() => transform.rotation;


}
