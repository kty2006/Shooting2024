using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {

        }
        else if (Input.GetKey(KeyCode.F2))
        {

        }
        else if (Input.GetKey(KeyCode.F3))
        {

        }
        else if (Input.GetKey(KeyCode.F4))
        {

        }
        else if (Input.GetKey(KeyCode.F5))
        {

        }
        else if (Input.GetKey(KeyCode.F6))
        {

        }
        else if (Input.GetKey(KeyCode.F7))
        {
            GameManager.Instance.Progress = 120;
        }
        else if (Input.GetKey(KeyCode.F8))
        {

        }
        else if (Input.GetKey(KeyCode.P))
        {

        }
    }
}
