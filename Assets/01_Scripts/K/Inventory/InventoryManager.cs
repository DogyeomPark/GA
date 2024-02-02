using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class InventoryManager : MonoBehaviourPunCallbacks
{
    //#region �̱���
    //private static InventoryManager instance;
    //public static InventoryManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //             �ν��Ͻ��� ������ ���� ����
    //            GameObject singletonObject = new GameObject("InventoryManager");
    //            instance = singletonObject.AddComponent<InventoryManager>();
    //            DontDestroyOnLoad(singletonObject); // �� ��ȯ �ÿ��� �����ǵ��� ����
    //        }

    //        return instance;
    //    }
    //}
    //#endregion  
    public DataMgrDontDestroy dataMgrDontDestroy;
    public PhotonView pv;

    [Space(1)]
    //public StateManager stateMgr;
    public GameObject rewardCanvas;
    public GameObject inventoryPanel;
    public bool isInven;

    // �� �����͵鵵 DataManager���� �̱��濡�� ���ܿ;��Ѵ�.
    public int playerLevel;
    public int weaponLv;
    public int attackPower;
    public string playerTitle; // Īȣ
    public string playerNick; // �г���
    public int expPotion;
    public int materials;
    public int gold;


    public Image goldImage;
    public Image expImage;
    public Image materialImage;
    public Text goldTxt;
    public Text expTxt;
    public Text materialTxt;
    public Text atkInfo;
    public Text playerLvTxt;   // �κ��丮â ����
    public Text playerTitleTxt; // �κ��丮â Īȣ
    public Text playerNickTxt; // �κ��丮â �̸�


    private void Awake()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        pv = GetComponent<PhotonView>();
        playerLevel = dataMgrDontDestroy.Level;
        weaponLv = dataMgrDontDestroy.WeaponLevel;
        attackPower = dataMgrDontDestroy.AttackPower;
        expPotion = dataMgrDontDestroy.UserExpPotion;
        materials = dataMgrDontDestroy.UserMaterial;
        gold = dataMgrDontDestroy.UserGold;
        playerNick = dataMgrDontDestroy.NickName;
        playerTitle = "Faker"; // ��â �߰�. �ʿ�ÿ� �ٲ۴�.

        inventoryPanel.SetActive(false);
        //Transform tr = transform.GetChild(0).GetChild(0).GetChild(1);
        //rewardCanvas = GameObject.Find("RewardContent").gameObject;//TODO:
        //inventoryPanel = GameObject.Find("InventoryCanvas").gameObject;
        //goldImage = tr.Find("item_gold").GetComponent<Image>();
        //expImage = tr.transform.Find("item_exp").GetComponent<Image>();
        //materialImage = tr.transform.Find("item_material").GetComponent<Image>();
        //goldTxt = tr.transform.Find("item_goldTxt").GetComponent<Text>();
        //expTxt = tr.transform.Find("item_expTxt").GetComponent<Text>();
        //materialTxt = tr.transform.Find("item_materialTxt").GetComponent<Text>();
        //atkInfo = tr.transform.Find("atkInfo").GetComponent<Text>();
        //playerLvTxt = tr.transform.Find("lvInfo").GetComponent<Text>();
        //playerNickTxt = tr.transform.Find("PlayerNick").GetComponent<Text>();
        //playerTitleTxt = tr.transform.Find("PlayerTitle").GetComponent<Text>();
    }

    private void Update() // i ������ �κ��丮 ����
    {
        if (pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!isInven)
                {
                    inventoryPanel.SetActive(true);
                    InitInventory();
                    isInven = true;
                }
                else
                {
                    InitInventory();
                    inventoryPanel.SetActive(false);
                    isInven = false;
                }
            }
        }
    }
    public void InitInventory()
    {
        #region ������ 1�� �̻��̸� �ҵ�����
        if (gold >= 1)
        {
            goldImage.color = Color.white;
        }
        else
        {
            goldImage.color = Color.gray;
        }

        if (expPotion >= 1)
        {
            expImage.color = Color.white;
        }
        else
        {
            expImage.color = Color.gray;
        }

        if (materials >= 1)
        {
            materialImage.color = Color.white;
        }
        else
        {
            materialImage.color = Color.gray;
        }
        #endregion

        #region DataMgrDontDestroy�� ���� ����ȭ
        // ����, ���ݷ�, ���, ����ġ����, ���, Īȣ�� �ʱ�ȭ�ϸ�ɵ�
        playerLevel = dataMgrDontDestroy.Level;
        attackPower = dataMgrDontDestroy.AttackPower;
        expPotion = dataMgrDontDestroy.UserExpPotion;
        materials = dataMgrDontDestroy.UserMaterial;
        gold = dataMgrDontDestroy.UserGold;
        playerTitle = "Faker";
        #endregion

        goldTxt.text = gold.ToString();
        expTxt.text = expPotion.ToString();
        materialTxt.text = materials.ToString();
        atkInfo.text = attackPower.ToString();
        playerLvTxt.text = playerLevel.ToString();

        playerNickTxt.text = playerNick;
        playerTitleTxt.text = playerTitle;
    }
}
