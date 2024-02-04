using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Photon.Pun;

public class LevelUpMgr : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public TextAsset leveltxtFile; //Jsonfile

    public StateManager stateMgr;
    public GameObject lvupPanel;
    public int classNum; //0:����, 1:�ų�, 2:����
    public int playerlv;
    public float playerHp;
    public int playerExp;
    public int playerExpPotion;
    public float playerCriDamage;
    public int playerCriChance;
    public int expRequire;

    [Header("������ �г�")]
    public Text playerLvTxt;
    public Image expSlideImg;
    public Text expRequireTxt;
    public Text curExpPotionTxt;

    [Header("������ �г�")]
    public Text beforeHealth;
    public Text afterHealth;
    public Text beforeCriPer;
    public Text afterCriPer;
    public Text beforeCriDmg;
    public Text afterCriDmg;

    private void Awake()
    {
        var jsonitemFile = Resources.Load<TextAsset>("Json/LvupTable");
        leveltxtFile = jsonitemFile;

        dataMgrDontDestroy = DataMgrDontDestroy.Instance;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("�浹�Ͼ");
                PlayerDataCheck();
                Debug.Log("������Ʈ�� ui�� Ŭ�����ѹ� : " + classNum);
                UpdateUiData(classNum);
                lvupPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                SyncDataMgr();
                // ������â �����ϱ� �÷��̾��� ������ �ݿ�
                lvupPanel.SetActive(false);
            }
        }
    }

    public void OnClickOneBtn()
    {
        Debug.Log("���� �÷��̾��� ����ġ�� : " + playerExp);
        if (playerExpPotion == 0) // ������ 0�����
        {
            Debug.Log("������ �����մϴ�.");
        }
        else // ������ 0���� �ƴ϶��
        {
            if (playerExp == expRequire) // ����ġ�� �ִ�� ���־��ٸ�
            {
                Debug.Log("if�� ����ġ �ִ�� ����");
                Debug.Log("������ ����");
            }
            else // ����ġ�� �ִ�� ������������ else�� ����
            {
                playerExpPotion -= 1;
                playerExp += 100;
                UpdateUiData(classNum);
            }
        }
    }
    public void OnClickMaxBtn()
    {
        Debug.Log("���� �÷��̾��� ����ġ�� : " + playerExp);
        if (playerExpPotion == 0) // ������ 0�����
        {
            Debug.Log("������ �����մϴ�.");
        }
        else // ������ 0���� �ƴ϶��
        {
            if (playerExp == expRequire) // ����ġ�� �ִ�� ���־��ٸ�
            {
                Debug.Log("if�� ����ġ �ִ�� ����");
                Debug.Log("������ ����");
            }
            else // ����ġ�� �ִ�� ������������ else�� ����
            {
                playerExpPotion -= 1;
                playerExp += 100;
                UpdateUiData(classNum);
            }
        }
    }

    public void OnClickLevelUpBtn()
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        playerExp = 0;
        playerlv++;
        UpdateUiData(classNum);

        playerHp = (jsonData[classNum][playerlv]["PlayerHp"]);
        playerCriDamage = (jsonData[classNum][playerlv]["CriDMG"]);
        playerCriChance = (jsonData[classNum][playerlv]["CriPer"]);

        // DataMgrDontDestroy���� ������ �����ش�.
        dataMgrDontDestroy.Level = playerlv;
        dataMgrDontDestroy.Exp = playerExp;
        dataMgrDontDestroy.UserExpPotion = playerExpPotion;
        dataMgrDontDestroy.MaxHp = playerHp;
        dataMgrDontDestroy.CriDamage = playerCriDamage;
        dataMgrDontDestroy.CriChance = playerCriChance;
    }

    public void PlayerDataCheck()
    {
        playerlv = dataMgrDontDestroy.Level;
        playerExp = dataMgrDontDestroy.Exp;
        playerExpPotion = dataMgrDontDestroy.UserExpPotion;
        playerHp = dataMgrDontDestroy.MaxHp;
        playerCriDamage = dataMgrDontDestroy.CriDamage;
        playerCriChance = dataMgrDontDestroy.CriChance;
        classNum = dataMgrDontDestroy.ClassNum;

        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        expRequire = jsonData["ExpRequireTable"][playerlv + 1]["needExp"];
    }
    public void UpdateUiData(int classNumber)
    {
        Debug.Log("���� �÷��̾��� ����ġ�� : " + playerExp);
        Debug.Log("�������ϱ����� �ʿ��� ����ġ �� : " + expRequire);
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        playerLvTxt.text = playerlv.ToString();
        beforeHealth.text = playerHp.ToString();
        afterHealth.text = (jsonData[classNumber][playerlv+1]["PlayerHp"]);
        beforeCriDmg.text = playerCriDamage.ToString();
        afterCriDmg.text = (jsonData[classNumber][playerlv + 1]["CriDMG"]);
        beforeCriPer.text = playerCriChance.ToString();
        afterCriPer.text = (jsonData[classNumber][playerlv + 1]["CriPer"]);
        curExpPotionTxt.text = playerExpPotion.ToString();
        expRequireTxt.text = $"{playerExp} / {expRequire}";
    }

    public void SyncDataMgr()
    {
        dataMgrDontDestroy.Level = playerlv;
        dataMgrDontDestroy.Exp = playerExp;
        dataMgrDontDestroy.UserExpPotion = playerExpPotion;
        dataMgrDontDestroy.MaxHp = playerHp;
        dataMgrDontDestroy.CriDamage = playerCriDamage;
        dataMgrDontDestroy.CriChance = playerCriChance;
        dataMgrDontDestroy.ClassNum = classNum;
    }
}
