using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public int speed;
    public int time;
    public float Power;
    public void Start()
    {
        StartCoroutine(Des());
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.fixedDeltaTime * speed);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player unit))
        {
            unit.GetStates().Hp -= Power;
            StartCoroutine(unit.GodTime(Color.clear, 1));
            CameraShake.Instance.Shake(0.25f, 0.6f);
            UIManager.Instance.HitCheck();
        }
    }


    IEnumerator Des()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
