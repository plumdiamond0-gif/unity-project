using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    #region References
    [Header("References")]
    public PlayerMovement player;
    public PlayerAttack playerAttack;
    #endregion

    #region Camera Settings
    private Vector3 _offset;
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;
    #endregion

    #region Unity Lifecycle
    private void Start()
    {
        _offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        _targetRotation = Vector3.Lerp(
            _targetRotation,
            Vector3.zero,
            playerAttack.currentWeaponData.returnSpeed * Time.deltaTime
        );

        _currentRotation = Vector3.Slerp(
            _currentRotation,
            _targetRotation,
            playerAttack.currentWeaponData.snappiness * Time.deltaTime
        );
    }

    private void LateUpdate()
    {
        float clampedX = Mathf.Clamp(player.xRotation, -80f, 0f);

        Quaternion horizontal = player.transform.rotation;
        Quaternion vertical = Quaternion.Euler(clampedX, 0, 0);

        Vector3 rotatedOffset = horizontal * vertical * _offset;

        transform.position = player.transform.position + rotatedOffset;

        transform.LookAt(player.transform.position + Vector3.up * 1f);
        transform.localRotation*= Quaternion.Euler(_currentRotation);
    }
    #endregion

    #region Public Methods
    public void FireRecoil(float recoilX, float recoilY, float recoilZ)
    {
        _targetRotation += new Vector3(
            -recoilX,
            Random.Range(-recoilY, recoilY),
            Random.Range(-recoilZ, recoilZ)
        );
    }
    #endregion
}