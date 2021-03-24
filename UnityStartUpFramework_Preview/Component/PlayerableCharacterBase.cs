using UnityEngine;
using UnityEngine.UIElements;

public class PlayerableCharacterBase : CharacterBase
{
    [SerializeField] protected bool m_UseControlRotation = true;
    [SerializeField] protected bool m_UseDesiredRotation = true;
    [SerializeField] protected Vector3 m_RotationRate = new Vector3(0.0f, 10.0f, 0.0f);

    [SerializeField] protected bool m_UseControlRotationPitch;
    [SerializeField] protected bool m_UseControlRotationYaw = true;
    [SerializeField] protected bool m_UseControlRotationRoll;

    public PlayerControllerBase playerController { get; private set; }


    public virtual void OnControllerConnected(PlayerControllerBase connectedController)
    {
        playerController = connectedController;
    }

    protected virtual void Update()
    {
        ApplyControlRotation();
    }

    private void ApplyControlRotation()
    {
        if (!m_UseControlRotation) return;
        if (!playerController) return;

        var controlRotation = playerController.GetControlRotationToEuler();
        var targetRotation = transform.eulerAngles;

        targetRotation.x = m_UseControlRotationPitch ? controlRotation.x : targetRotation.x;
        targetRotation.y = m_UseControlRotationYaw ? controlRotation.y : targetRotation.y;
        targetRotation.z = m_UseControlRotationRoll ? controlRotation.z : targetRotation.z;

        var newEulerAngle = targetRotation;

        if (m_UseDesiredRotation)
        {
            newEulerAngle.x = Mathf.LerpAngle(
                transform.eulerAngles.x, targetRotation.x,
                m_RotationRate.x * Time.deltaTime);

            newEulerAngle.y = Mathf.LerpAngle(
                transform.eulerAngles.y, targetRotation.y,
                m_RotationRate.y * Time.deltaTime);

            newEulerAngle.z = Mathf.LerpAngle(
                transform.eulerAngles.z, targetRotation.z,
                m_RotationRate.z * Time.deltaTime);
        }

        transform.eulerAngles = newEulerAngle;
    }
}
