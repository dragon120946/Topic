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
                txtDialogue.text = "�A�O�ڭ̴˪L�̳̫᪺�Ʊ�F�I���U�A�������I�p���w�I";
                animator.SetInteger("Index", 0);
                GameDb.index++;
                return;
            }
            if (GameDb.index == 2)
            {
                txtDialogue.text = "�{�b����˪L���J�F�M���A�b���𪺪F�����䦳�B�ʳ��s�򨪵K�}�]�A";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 3)
            {
                txtDialogue.text = "�L�̵����̯���j�H����q�u���X�X�_�]�C";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 4)
            {
                txtDialogue.text = "����j�H�|�b�C���ʦ~�i�J�@����v���A�O�@�˪L�����ɷ|�����z�A";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 5)
            {
                txtDialogue.text = "�ҥH�B�ʳ��s�򨪵K�}�]���N�O�X�o�q�ɶ��]�ӷm���F�_�]�C";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 6)
            {
                txtDialogue.text = "����j�H�u�n�j�����v���A�åB�κɳѾl���O�q�A�b���𤧬u���u�����`�J���O��O�A�N�ϥͤF�I";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 7)
            {
                txtDialogue.text = "�ҩ��ڭ��٫O�d�̫�@���H���I���O���Q�@�}�����j���F......";
                playerCine.enabled = false;
                chipCine.enabled = true;
                GameDb.index++;
                return;
            }
            if (GameDb.index == 8)
            {
                txtDialogue.text = "�̭��V�Ӭݫܦ��i���ٯd�b�˪L�̭��I���U�A�F�A�⥦��^�ӧa�I";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 9)
            {
                txtDialogue.text = "�{�b�p���w���_�I�����}�l�F�A�ڷ|�H�ɳ��b�A�����I";
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
                txtDialogue.text = "�ҥH����L�ɰO�o���ֶ]���άO�Τ��y�����L�C";
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
                txtDialogue.text = "�p�G�S�����}�L���ܥi��|�L�i�F�|�̪��A�p���w�n�p���ԷV��C";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 16)
            {
                txtDialogue.text = "���̦��@�Ӭ\��ΡI���쨺�W�����ܴN�w���F�I";
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
                txtDialogue.text = "���U�ӴN�O�e�����K�}�]�F�A�U�@�Ӧa��|��M�I�A�O�o�p���ԷV�C";
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
            txtDialogue.text = "���M�ܬ�M�A���O�A�ڥ����n�i�D�A�ܭ��n���Ʊ�...�I";
            animator.SetInteger("Index", 3);
            GameDb.index++;
        }
        if (GameDb.index == 11 && butterfly)
        {
            txtDialogue.text = "�I......�n�M�I��I��診�ӬO�d�����A�e�̪��ߩʴN�O�ݨ���Ϊ�e�N�|�Q�l���A";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            animator.SetInteger("Index", 3);
            GameDb.index++;
        }
        if (GameDb.index == 14 && sandBug)
        {
            txtDialogue.text = "���ӬO�F�\�ΡA�L�|�q�F�l�̭��p�X�ӧ�������g�L���ͪ��A";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            GameDb.index++;
        }
        if (GameDb.index == 18 && GameDb.key == 1)
        {
            txtDialogue.text = "�Ӧn�F�I�ש�F�����Ȫ��Ĥ@�B�F�I";
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            GameDb.index++;
        }
        txtTutor.text = "��o�_�]�H��(" + GameDb.key + "/1)";
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
