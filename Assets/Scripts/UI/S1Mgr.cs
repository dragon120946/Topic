using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;

public class S1Mgr : GameManager
{
    public List<GameObject> energyFlowerList;
    public GameObject energyFlowers;
    public CinemachineVirtualCamera playerCine;
    public CinemachineVirtualCamera chipCine;
    public Animator animator;
    [NonSerialized] public bool butterfly = false;
    [NonSerialized] public bool sandBug = false;
    void Start()
    {
        Base_Start();
        GameDb.key = 0;
        txtDialogue.transform.parent.parent.gameObject.SetActive(true);
        txtTutor.transform.parent.gameObject.SetActive(false);
        playerCine.enabled = true;
        chipCine.enabled = false;
    }
    void FixedUpdate()
    {
        Base_FixedUpdate();

    }

    void Update()
    {
        Base_Update();
        timer += Time.deltaTime;
        if (timer >= nextTime || skip)
        {
            timer = 0;
            
            if (GameDb.index == 1)
            {
                txtDialogue.text = "你是我們森林裡最後的希望了！拜託你幫幫忙！小水滴！";
                animator.SetInteger("Index", 0);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 2)
            {
                txtDialogue.text = "現在神樹森林陷入了危機，在神樹的東西方兩邊有冰封雪山跟赤焰洞窟，";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 3)
            {
                txtDialogue.text = "他們著覬覦神樹大人的能量泉源——寶珠。";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 4)
            {
                txtDialogue.text = "神樹大人會在每五百年進入一次休眠期，保護森林的結界會變薄弱，";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 5)
            {
                txtDialogue.text = "所以冰封雪山跟赤焰洞窟兩方就是趁這段時間跑來搶走了寶珠。";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 6)
            {
                txtDialogue.text = "神樹大人只好強制結束休眠期，並且用盡剩餘的力量，在神樹之泉的泉水中注入神力於是你就誕生了！";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 7)
            {
                txtDialogue.text = "所幸我們還保留最後一塊碎片！但是它被一陣風給吹走了......";
                playerCine.enabled = false;
                chipCine.enabled = true;
                GameDb.index++;
                return;
            }
            if (GameDb.index == 8)
            {
                txtDialogue.text = "依風向來看很有可能還留在森林裡面！拜託你了，把它找回來吧！";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 9)
            {
                txtDialogue.text = "現在小水滴的冒險正式開始了，我會隨時陪在你身邊喔！";
                playerCine.enabled = true;
                chipCine.enabled = false;
                animator.SetInteger("Index", 1);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 10)
            {
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                txtTutor.transform.parent.gameObject.SetActive(true);
                GameDb.index++;
                return;
            }

            if (GameDb.index == 12)
            {
                txtDialogue.text = "所以見到他時記得趕快跑走或是用水球攻擊他。";
                animator.SetInteger("Index", 0);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 13)
            {
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 15)
            {
                txtDialogue.text = "如果沒有躲開他的話可能會摔進沙坑裡的，小水滴要小心謹慎喔。";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 16)
            {
                txtDialogue.text = "那裡有一個枯木樁！跳到那上面的話就安全了！";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 17)
            {
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                GameDb.index++;
                return;
            }
            
            if (GameDb.index == 19)
            {
                txtDialogue.text = "接下來就是前往赤焰洞窟了，下一個地方會更危險，記得小心謹慎。";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 20)
            {
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                GameDb.index++;
                return;
            }
        }
        if (GameDb.index == 0)
        {
            txtDialogue.text = "雖然很突然，但是，我必須要告訴你很重要的事情...！";
            animator.SetInteger("Index", 3);
            GameDb.index++;
        }
        if (GameDb.index == 11 && butterfly)
        {
            txtDialogue.text = "呼......好危險喔！剛剛那個是千葉蝶，牠們的習性就是看到水或花蜜就會想吸食，";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            animator.SetInteger("Index", 3);
            GameDb.index++;
        }
        if (GameDb.index == 14 && sandBug)
        {
            txtDialogue.text = "那個是沙枯蟲，他會從沙子裡面鑽出來攻擊任何經過的生物，";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            GameDb.index++;
        }
        if (GameDb.index == 18 && GameDb.key == 1)
        {
            txtDialogue.text = "太好了！終於達成任務的第一步了！";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            GameDb.index++;
        }
        txtTutor.text = "獲得寶珠碎片(" + GameDb.key + "/1)";
        if (GameDb.hp <= 0)
        {
            Dead();
        }
    }

    public void TutorSkip(InputAction.CallbackContext context)
    {
        GameDb.level = 2;
        SceneManager.LoadScene("Loading");
    }
}
