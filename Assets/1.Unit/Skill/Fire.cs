using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float time;
    public float Power;
    public void Start()
    {
        StartCoroutine(Des());
        
    }

    private void Update()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, 90, Vector3.forward, 500, LayerMask.GetMask("Unit"));
        foreach (var ray in hit)
        {
            if (ray.collider.gameObject.TryGetComponent(out Player unit))
            {
                unit.GetStates().Hp -= Power;
                CameraShake.Instance.Shake(0.25f, 0.6f);
                UIManager.Instance.HitCheck();
                StartCoroutine(unit.GodTime(Color.clear, 1));
            }
        }
    }
    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out Player unit))
    //    {
    //        unit.GetStates().Hp -= Power;
    //        CameraShake.Instance.Shake(0.25f, 0.6f);
    //        UIManager.Instance.HitCheck();
    //        StartCoroutine(unit.GodTime(Color.clear, 1));
    //    }
    //}

    public IEnumerator Des()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
