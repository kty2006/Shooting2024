using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera Camera;
    public Transform PlayerViewPos;
    public Vector3 MaxLimit, MinLimit;
    public int flowCameraSpeed;
    #region Limit함수
    //Limit함수를 player도 사용하지만 player와 camera에서 사용하는 Limit함수,변수를
    //GameManager같은 클래스에서 작성하고 사용하기는 Limit이 다르게 작동하고 변수도 다른값이 필요하기 때문에
    //안되고 interface를 사용해서 함수만 묶고 interface를 상속받는것은 다른데에서 Limit함수만 다르게 조정할 일이 없는데 interface를 사용하는것이나
    //안하는 것이나 비슷하다고 생각하여 똑같은 함수를 다른 클래스 에서 작성함
    #endregion
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerViewPos.position, Time.deltaTime*flowCameraSpeed);
        Limit();
    }

    public void Limit()
    {
        float x = Mathf.Clamp(transform.position.x, MinLimit.x, MaxLimit.x);
        float z = Mathf.Clamp(transform.position.z, MinLimit.z, MaxLimit.z);
        transform.position = new Vector3(x, transform.position.y, z);
    }


    public void CameraFlag()
    {
        Camera.clearFlags = CameraClearFlags.SolidColor;
    }
}
