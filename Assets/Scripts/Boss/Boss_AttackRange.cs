using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//如果進入攻擊範圍，則不能遠距攻擊，如果超出則可遠距攻擊
public class Boss_AttackRange : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<Boss>().canFarAttack = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<Boss>().canFarAttack = true;
        }
    }
}
