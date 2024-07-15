using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform PlayerViewPos;
    public Vector3 MaxLimit, MinLimit;
    #region Limit�Լ�
    //Limit�Լ��� player�� ��������� player�� camera���� ����ϴ� Limit�Լ�,������
    //GameManager���� Ŭ�������� �ۼ��ϰ� ����ϱ�� Limit�� �ٸ��� �۵��ϰ� ������ �ٸ����� �ʿ��ϱ� ������
    //�ȵǰ� interface�� ����ؼ� �Լ��� ���� interface�� ��ӹ޴°��� �ٸ������� Limit�Լ��� �ٸ��� ������ ���� ���µ� interface�� ����ϴ°��̳�
    //���ϴ� ���̳� ����ϴٰ� �����Ͽ� �Ȱ��� �Լ��� �ٸ� Ŭ���� ���� �ۼ���
    #endregion
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerViewPos.position, Time.deltaTime);
        Limit();
    }

    public void Limit()
    {
        float x = Mathf.Clamp(transform.position.x, Player.Instance.MinLimit.x+ MinLimit.x, Player.Instance.MaxLimit.x- MaxLimit.x);
        float z = Mathf.Clamp(transform.position.z, Player.Instance.MinLimit.z - MinLimit.z, Player.Instance.MaxLimit.z - MaxLimit.z);
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
