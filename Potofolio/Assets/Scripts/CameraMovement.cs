using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerMovement player;
    public PlayerAttack playerAttack;
    private Vector3 offset;
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {

        targetRotation =
            Vector3.Lerp
            (targetRotation, Vector3.zero, 
            playerAttack.currentweapondata.returnSpeed
            *Time.deltaTime);
        currentRotation = 
            Vector3.Slerp
            (currentRotation, targetRotation, 
            playerAttack.currentweapondata.snappiness
            *Time.fixedDeltaTime);
        //fixedDeltaTime은 프레임률에 상관없이 고정된 시간 간격을 가져 컴사양에 상관 없이 반동이 따라붙는 강도가 동일하게 느껴지도록.
        //transform.localRotation = Quaternion.Euler(currentRotation);
        //그냥 rotation은 절대적 기준을 따라 '세상의 북쪽에서 위로 n도'라고 해석
        //localRotation은 부모를 기준으로 해 '플레이어가 지금 보고 있는 방향에 n도'라 해석

    }


    void LateUpdate()
    {
        //Vector3 mouseVec = new Vector3(player.xRotation, player.yRotation, 0);
        //Quaternion playerRotation = player.transform.rotation * mouseVec;
        //float clampedX = Mathf.Clamp(player.xRotation, -45f, 45f);

        Quaternion playerRotation = Quaternion.Euler(player.xRotation, player.yRotation, 0);



        Vector3 rotatedOffset = playerRotation * offset;
        //원래라면 화살표가 플레이어 뒤만 가리키는데 일케 하면 플레이어 회전에 따라 화살표도 돌아감

           transform.position = player.transform.position + rotatedOffset;
        
        //위에서 정해진 화살표 방향에 따라 플레이어 중심으로 카메라 회전
        transform.LookAt(player.transform.position + Vector3.up * 1f);
        transform.rotation *= Quaternion.Euler(currentRotation);
    

    }

    public void FireRecoil(float RecoilX, float RecoilY, float RecoilZ)
    {
        targetRotation += new Vector3(RecoilX,
            Random.Range(-RecoilY, RecoilY), Random.Range(-RecoilZ, RecoilZ));
    }
    /*
    void LateUpdate()

    {

        transform.position = player.transform.position + offset;

    }
    */
}
