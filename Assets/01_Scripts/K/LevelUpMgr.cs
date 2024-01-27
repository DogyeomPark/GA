using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMgr : MonoBehaviour
{
    public StateManager stateMgr;
    public InventoryManager inventoryMgr;
    public int playerLv;
    public int playerExp;
    public int upMaxExp;

    [Header("������ �г�")]
    public Text playerLvTxt;
    public Image expSlideImg;
    public Text expChartTxt;
    public Text playerHaveExpTxt;

    private void Awake()
    {
        playerLvTxt = GameObject.Find("PlayerLevelInfo").GetComponent<Text>();
    }
    //Collider���� stateMgr�ް� ����!!!


    public void PlayerCheck() // ���� ������ üũ
    {
        playerLvTxt.text = stateMgr.level.ToString();
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
        LevelUpCheck(playerLv);
    }

    public void LevelUpCheck(int lv) // �������� ���Ѱ�
    {
        


        //���⿡ �г� ���� �Լ�();
    }

    public void OnLevelUpBtn()
    {

        InitCheckLevel();
    }

    public void InitCheckLevel()
    {
        inventoryMgr.playerLv.text = inventoryMgr.playerLv.ToString();
        inventoryMgr.expTxt.text = inventoryMgr.expPotion.ToString();
        playerLvTxt.text = stateMgr.level.ToString();
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
    }
}
