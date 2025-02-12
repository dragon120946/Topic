using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TrainMgr : GameManager
{
    public Button btnSkip;          //跳過按鈕
    public Image imgF;
    public Image imgArrow;
    public GameObject seed;
    public GameObject iceSprint;
    public GameObject fireSprint;
    public GameObject target;
    public GameObject energyFlower;
    public GameObject leaf;
    public GameObject leafBird;
    public GameObject aimCenter;
    public GameObject player;
    public GameObject waterHint;
    public GameObject sandHint;
    public GameObject waterWall;
    public GameObject sandWall;
    //public Barrel barrel;
    public Shoot2Test shoot2;

    [NonSerialized]
    public bool touchWaterhole;
    [NonSerialized]
    public bool isTargetDestroy;
    [NonSerialized]
    public bool touchSeed;
    [NonSerialized]
    public bool touchFlower;
    private bool isLeft = false;
    private bool isRight = false;
    private bool isJump = false;
    [SerializeField]private Animator animator;
    void Start()
    {
        Base_Start();
        animator.SetInteger("Index", 0);
        GameDb.touchIce = false;
        GameDb.touchSand = false;
        GameDb.tab = false;
        isTargetDestroy = false;
        touchWaterhole = false;
        touchSeed = false;
        touchFlower = false;
        waterWall.GetComponent<BoxCollider2D>().isTrigger = false;
        sandWall.GetComponent<BoxCollider2D>().isTrigger = false;
        txtDialogue.transform.parent.parent.gameObject.SetActive(true);
        leafBird.SetActive(true);
        imgArrow.gameObject.SetActive(false);
        txtTutor.transform.parent.gameObject.SetActive(false);
        imgF.gameObject.SetActive(false);
        waterHint.SetActive(false);
        sandHint.SetActive(false);
        leaf.SetActive(false);
        seed.SetActive(false);
        iceSprint.SetActive(false);
        fireSprint.SetActive(false);
        target.SetActive(false);
        energyFlower.SetActive(false);
        //slidStamina.gameObject.SetActive(false);
        aimCenter.SetActive(false);
        //barrel.enabled = false;
        shoot2.enabled = false;
        playerctrl.enabled = false;
        btnSkip.onClick.AddListener(OnBtnEndClick);
    }

    void FixedUpdate()
    {
        Base_FixedUpdate();
    }

    void Update()
    {
        if (GameDb.hp < 5)
        {
            GameDb.hp = 5;
        }
        
        Base_Update();
        #region 文本事件
        if (GameDb.index == 12 && GameDb.touchSand)
        {

            txtDialogue.text = "哇！就叫你小心一點了嘛，千萬不要貿然踩入沙坑！";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            leafBird.SetActive(true);
            txtTutor.transform.parent.gameObject.SetActive(false);
            animator.SetInteger("Index", 2);
            GameDb.index++;
            return;
        }
        timer += Time.deltaTime;
        if (timer >= nextTime || skip)
        {
            timer = 0;

            if (GameDb.index == 1)
            {
                txtDialogue.text = "這裡是神樹內部的樹洞，我會在這裡教你怎麼使用能力！";
                GameDb.index++;
                return;
            }

            if (GameDb.index == 2)
            {
                txtDialogue.text = "首先來教你怎麼走路吧！";
                GameDb.index++;
                return;
            }

            if (GameDb.index == 3)
            {
                txtTutor.text = "按AD(手把左蘑菇頭)移動，空白鍵(手把B)跳躍，踩香菇能跳更高(0/1)";
                txtTutor.transform.parent.gameObject.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                playerctrl.enabled = true;
                GameDb.index++;
                return;
            }
           
            if (GameDb.index == 5)
            {
                waterHint.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 6)
            {
                txtDialogue.text = "接下來你試著進到那座小水池，進到水池裡你可以補充水分，也就是你的血量！";
                txtDialogue.transform.parent.parent.gameObject.SetActive(true);
                leafBird.SetActive(true);
                waterHint.SetActive(false);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 7)
            {
                txtTutor.text = "前往水池(0/1)";
                txtTutor.transform.parent.gameObject.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                waterWall.GetComponent<BoxCollider2D>().isTrigger = true;
                GameDb.index++;
                return;
            }

            if (GameDb.index == 9)
            {    
                sandHint.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                animator.Play("LeafBird_Idle_UI");
                GameDb.index++;
                return;
            }
            if (GameDb.index == 10)
            {
                txtDialogue.text = "你看看那裡有一個小沙坑，你要小心不要踩到了...水滴會被沙子吸收，踩上去會減少水分喔！";
                txtDialogue.transform.parent.parent.gameObject.SetActive(true);
                leafBird.SetActive(true);
                sandHint.SetActive(false);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 11)
            {
                txtTutor.text = "前往沙地(0/1)";
                txtTutor.transform.parent.gameObject.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                sandWall.GetComponent<BoxCollider2D>().isTrigger = true;
                GameDb.index++;
                return;
            }

            if (GameDb.index == 13)
            {
                txtDialogue.text = "可以發射出水球，嘗試看看吧！發射水球會扣你一點的血量。";
                animator.SetInteger("Index", 0);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 14)
            {
                txtTutor.text = "左鍵(手把ZR)發射水球並攻擊目標(0/1)";
                txtTutor.transform.parent.gameObject.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                //barrel.enabled = true;
                shoot2.enabled = true;
                aimCenter.SetActive(true);
                target.SetActive(true);
  
                GameDb.index++;
                return;
            }

            if (GameDb.index == 16)
            {
                txtDialogue.text = "觸碰種子並按F看看？";
                seed.SetActive(true);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 17)
            {
                txtTutor.text = "觸碰種子並按F(手把X)(0/1)";
                txtTutor.transform.parent.gameObject.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                GameDb.index++;
                return;
            }

            if (GameDb.index == 19)
            {
                txtDialogue.text = "接下來我來教你怎麼使用自身的能力吧！";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 20)
            {
                txtDialogue.text = "你可以通過採集能量花來轉換能量。集滿10個即可轉換一次型態。";
                GameDb.energy = 9;
                GameDb.index++;
                return;
            }
            if (GameDb.index == 21)
            {
                txtTutor.text = "觸碰能量花(0/1)";
                txtTutor.transform.parent.gameObject.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                energyFlower.SetActive(true);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 24)
            {
                txtDialogue.text = "觸碰看看他吧。";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 25)
            {
                txtTutor.text = "觸碰藍色生物(0/1)";
                txtTutor.transform.parent.gameObject.SetActive(true);
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
                leafBird.SetActive(false);
                iceSprint.SetActive(true);
                GameDb.index++;
                return;
            }

            if (GameDb.index == 27)
            {
                txtDialogue.text = "碰紅色生物能變蒸氣，在蒸氣型態時只能左右移動，你可以多練習看看，該教得差不多都教完了！";
                fireSprint.SetActive(true);
                imgArrow.gameObject.SetActive(true);
            }
        }
        if (GameDb.index == 0)
        {
            txtDialogue.text = "嗨！你好哇，小水滴！";
            GameDb.index++;
        }
        if (GameDb.index == 4 && isLeft && isRight && isJump)
        {
            txtDialogue.text = "做得好！你已經學會移動了！";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            leafBird.SetActive(true);
            txtTutor.transform.parent.gameObject.SetActive(false);
            GameDb.index++;

        }

        if (GameDb.index == 8 && touchWaterhole)
        {
            
            txtDialogue.text = "太好了！你恢復了一點水分，現在你還很弱小，蒐集越多水分你可以做的事情更多，一定要多注意喔！";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            leafBird.SetActive(true);
            txtTutor.transform.parent.gameObject.SetActive(false);
            animator.SetInteger("Index", 1);
            GameDb.index++;
        }
        
        if (GameDb.index == 15 && isTargetDestroy)
        {
            txtDialogue.text = "再來下一步，我教你如何幫植物澆水，我先在這裡放下一顆種子。";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            leafBird.SetActive(true);
            txtTutor.transform.parent.gameObject.SetActive(false);
            GameDb.index++;
        }
        if (GameDb.index == 18 && touchSeed)
        {
            txtDialogue.text = "你看！植物長大了，在地圖中替種子澆水之後踩著植物就可以走到原來走不到的地方喔！";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            leafBird.SetActive(true);
            txtTutor.transform.parent.gameObject.SetActive(false);
            GameDb.index++;
        }
        if (GameDb.index == 22 && touchFlower)
        {
            txtTutor.text = "能量滿了後按Tab(手把ZL)型態選單並用左鍵選擇。(0/1)";
            GameDb.index++;
        }
        if (GameDb.index == 23 && GameDb.tab)
        {
            txtDialogue.text = "你可以通過精靈的力量來轉換型態也可以通過蒐集能量來轉換。";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            leafBird.SetActive(true);
            txtTutor.transform.parent.gameObject.SetActive(false);
            GameDb.index++;
        }
        if (GameDb.index == 26 && GameDb.touchIce)
        {
            txtDialogue.text = "你變成冰型態了！在這個型態時不用怕被沙子吸收水分了。";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            leafBird.SetActive(true);
            txtTutor.transform.parent.gameObject.SetActive(false);
            GameDb.index++;
        }
        if (player.GetComponent<Rigidbody2D>().position.x < -11)
        {
            isLeft = true;
        }
        if (player.GetComponent<Rigidbody2D>().position.x > -11)
        {
            isRight = true;
        }
        if (!GameDb.isGround)
        {
            isJump = true;
        }
        #endregion

    }
    void OnBtnEndClick()
    {
        GameDb.level++;
        SceneManager.LoadScene("Loading");
    }
    
    public void TutorSkip(InputAction.CallbackContext context)
    {
        GameDb.level = 1;
        SceneManager.LoadScene("Loading");
    }
  
    public void Setting_Train(InputAction.CallbackContext context)
    {
        isSettingViewOpen = !isSettingViewOpen;
        if (isSettingViewOpen)
        {
            Time.timeScale = 0;
            settingView.SetActive(true);
            btnSkip.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(slidMusic.gameObject);
        }
        else
        {
            Time.timeScale = 1;
            settingView.SetActive(false);
            btnSkip.gameObject.SetActive(true);
        }
    }

    public void TypeChange_Train(InputAction.CallbackContext context)
    {
        if (GameDb.energy >= 10 && context.performed)
        {
            Time.timeScale = 0;
            typeChange.SetActive(true);
            EventSystem.current.SetSelectedGameObject(btnWater.gameObject);
        }
        else
        {
            Time.timeScale = 1;
            typeChange.SetActive(false);
        }
    }
}