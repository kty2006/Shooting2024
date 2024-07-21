using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera Camera;
    public Transform PlayerViewPos;
    public Vector3 MaxLimit, MinLimit;
    public int flowCameraSpeed;
    #region Limit�Լ�
    //Limit�Լ��� player�� ��������� player�� camera���� ����ϴ� Limit�Լ�,������
    //GameManager���� Ŭ�������� �ۼ��ϰ� ����ϱ�� Limit�� �ٸ��� �۵��ϰ� ������ �ٸ����� �ʿ��ϱ� ������
    //�ȵǰ� interface�� ����ؼ� �Լ��� ���� interface�� ��ӹ޴°��� �ٸ������� Limit�Լ��� �ٸ��� ������ ���� ���µ� interface�� ����ϴ°��̳�
    //���ϴ� ���̳� ����ϴٰ� �����Ͽ� �Ȱ��� �Լ��� �ٸ� Ŭ���� ���� �ۼ���
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
