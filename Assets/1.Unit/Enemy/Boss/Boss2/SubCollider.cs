using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SubCollider : MonoBehaviour
{
    public float Power;
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Player player))
        {
            player.GetStates().Hp -= Power;
            StartCoroutine(player.GodTime(Color.clear, 1));
            CameraShake.Instance.Shake(0.25f, 0.6f);
            UIManager.Instance.HitCheck();
        }
    }
}
