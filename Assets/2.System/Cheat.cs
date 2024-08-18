using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    public ItemData itemData;
    private void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            Player.Instance.CheatGodkey();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Player.Instance.GetStates().Lv += 1;
            Player.Instance.GetStates().Exp = 0 ;
            Player.Instance.LvUp();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Player.Instance.GetStates().Lv -= 1;
            Player.Instance.GetStates().Exp = 0;
            Player.Instance.LvUp();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            Player.Instance.GetStates().Hp = Player.Instance.GetStates().MaxHp;
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            DropWeaponItem();
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            DropItem();
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            GameManager.Instance.Progress = 80;
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            if(UIManager.Instance.Panel.gameObject.activeSelf)
            {
                UIManager.Instance.Panel.gameObject.SetActive(false);
            }
            else
                UIManager.Instance.Panel.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.SceneSave.Scene(0);
        }
    }

    public void DropWeaponItem()
    {
        ObjectPool.Instance.Pooling(transform.position, Quaternion.identity, itemData.Items[Random.Range(3, 5)].gameObject);
    }

    public void DropItem()
    {
        ObjectPool.Instance.Pooling(transform.position, Quaternion.identity, itemData.Items[Random.Range(0, 3)].gameObject);
    }
}
