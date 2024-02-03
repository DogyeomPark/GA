using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using Photon.Pun;

//###### Quest List, Quest Description UI ��� ######

public class QuestManager : MonoBehaviour
{
    [Header("DonDestroy")]
    public DataMgrDontDestroy dataMgrDontDestroy;

    public TextAsset txtFile; //Jsonfile
    // ����Ʈ �˾��� �̱������� �ٸ� ���� ���ٰ� �´�. �׷��Ƿ� ����Ʈ NPC�� ��ȣ�ۿ��� �� �� 
    // ���� �˾��� �ִ� ī��Ʈ�� ����Ʈ �Ŵ��� ��ũ��Ʈ�� �ִ� ����, �ִ� ī��Ʈ ���� ��������Ѵ�
    // �׸��� ��ȣ�ۿ��Ҷ� ����Ʈ�� �Ϸ�Ǿ��� �� bool���� True���� Ȯ���ؼ�
    // ����Ʈ�Ϸ� ��ư�� SetActive = true �� �Ѵ�.
    // npc Ŀ���ϰ� �Ұ�. 
    [Header("Component")]
    public QuestPopUpManager questPopUpManager;

    [Header("����Ʈ ����â")]
    public GameObject questPanel;

    [Header("����Ʈ ����â")]
    public GameObject descriptionPanel;
    public Text questNameTxt;
    public Text goalTxt;

    [Header("NPC ��ȭ")]
    public DialogueTrigger dialogueTrigger; //�뺻
    public GameObject nextBtn; //�뺻 ����
    public GameObject nPCConversation;

    public Text textName;
    public Text textSentence;
    public GameObject conversationPanel;

    Queue<string> naming = new Queue<string>();
    Queue<string> sentence = new Queue<string>();

    public Text rewardExp;
    public Text rewardMat;
    public Text rewardGold;

    [Header("����Ʈ �����ư")]
    public GameObject acceptBtn;
    public GameObject ingImg;
    public GameObject completedBtn;

    [Header("����Ʈ ���� ���൵ â")]
    public GameObject questPopUpPanel;
    //public bool questPopUpPanelVisible;
    public Text questDescriptionGoalTxt; //�� �ؽ�Ʈ�� questGoalTxt �� ���ڰ� ��
    public string questGoalTxt; 
    public int questCurCnt;
    public int questMaxCnt;
    

    [Header("����Ʈ ����")]
    public RewardMgr rewardMgr;
    public int expPotionReward;
    public int materialReward;
    public int goldReward;


    public void QuestClearReward(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int item = n - 1; //�Ű�����

        rewardMgr = GetComponent<RewardMgr>();
        expPotionReward=(jsonData["Quest"][item]["Exp"]);
        materialReward=(jsonData["Quest"][item]["Material"]);
        goldReward=(jsonData["Quest"][item]["Gold"]);
        expPotionReward = dataMgrDontDestroy.dungeonNumIdx;
        materialReward = dataMgrDontDestroy.dungeonNumIdx;
        goldReward *= dataMgrDontDestroy.dungeonNumIdx;
        rewardMgr.InstExp(expPotionReward);
        rewardMgr.InstMaterial(materialReward);
        rewardMgr.InstGold(goldReward);
    }

    private void Awake()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        questPopUpManager = GameObject.Find("QuestPopUp").GetComponent<QuestPopUpManager>();
    }
    private void Start()
    {
        //ingBtn.SetActive(false);
        completedBtn.SetActive(false);
        ingImg.SetActive(false);
        descriptionPanel.SetActive(false);
        questPopUpPanel.SetActive(false);
        questPopUpManager.questIdx = dataMgrDontDestroy.QuestIdx;
        questGoalTxt = dataMgrDontDestroy.GoalTxt;
        questCurCnt = dataMgrDontDestroy.QuestCurCnt;
        questMaxCnt = dataMgrDontDestroy.QuestMaxCnt;
        nPCConversation.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("������");
            nextBtn.GetComponent<TalkMgr>();
            Debug.Log("### TalkMgr ������ ###");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("�浹�Ͼ");
                questPanel.SetActive(true);
                nPCConversation.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                // ��ȭâ �����ϱ� �÷��̾��� ������ �ݿ�
                questPanel.SetActive(false);
            }
        }
    }

    public void InstQuest(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var�� �ǹ�: Unity���� ������ �ٰ����´�.

        int item = n - 1; //�Ű�����

        dataMgrDontDestroy.questIdx = n;
        questNameTxt.text = (jsonData["Quest"][item]["QuestName"]);
        goalTxt.text = (jsonData["Quest"][item]["Goal"]);
        rewardExp.text = (jsonData["Quest"][item]["Exp"]);
        rewardMat.text = (jsonData["Quest"][item]["Material"]);
        rewardGold.text = (jsonData["Quest"][item]["Gold"]);
        
        
        #region
        //character.transform.name = (jsonData["��Ʈ1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().charname = (jsonData["��Ʈ1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().atk = (int)(jsonData["��Ʈ1"][n]["Count"]);
        ////character.GetComponent<QuestData>().count++; //QuestData�� ī��Ʈ ����

        //character.tag = "Player"; //prefab�� �±׸� �ްž�.

        //character.transform.SetParent(questCanvas.transform); //���� questCanvas�� �θ�� �ΰ� �����ϰ� Prefab�� �¾.
        #endregion
    }

    public void AcceptBtn()
    {
        ReceiveQuest(dataMgrDontDestroy.questIdx);
        
        //uIMgr.UpdateQuestPopUpInfo(questPopUpManager.questGoalTxt.text, questPopUpManager.questCountTxt.text);
    }

    public void ReceiveQuest(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);
        int item = n - 1;

        dataMgrDontDestroy.goalTxt = (jsonData["Quest"][item]["Goal"]);
        dataMgrDontDestroy.questMaxCnt = (int)(jsonData["Quest"][item]["Count"]);
        dataMgrDontDestroy.questIdx = (int)(jsonData["Quest"][item]["QuestNum"]);

        questPopUpManager.UpdateQuestStatus(dataMgrDontDestroy.goalTxt, dataMgrDontDestroy.questCurCnt, dataMgrDontDestroy.questMaxCnt);

        //questPopUpPanelVisible
        acceptBtn.SetActive(false);
        questPopUpPanel.SetActive(true);
        ingImg.SetActive(true);
        
    }

    public void CompletedBtn()
    {
        questPopUpManager.InitCurQuest();

        if (questPopUpManager.isCompleted)
        {
            // ���� ���� �� ó��
            // (���� ���� �ڵ� �߰� �ʿ�)

            // ����Ʈ ��ư ��Ȱ��ȭ
            acceptBtn.SetActive(false);
            ingImg.SetActive(false);
            completedBtn.SetActive(true);

            // ���� ����Ʈ�� ������ �� ���� ��ư�� �������� ����
            acceptBtn.SetActive(true);
            ingImg.SetActive(false);
            completedBtn.SetActive(false);

            // ����Ʈ �ε��� ���� �� ������ �Ŵ��� ����
            dataMgrDontDestroy.questIdx++;
            dataMgrDontDestroy.QuestIdx = questPopUpManager.questIdx;

            // ����Ʈ �˾� �г� ��Ȱ��ȭ
            questPopUpPanel.SetActive(false);
        }
        //if (questCurCnt >= questMaxCnt)
        //{
        //    questIdx++;
        //    dataMgrDontDestroy.QuestIdx = questIdx;
        //}
        //transform.Find("IngImg").gameObject.SetActive(false);
        //transform.Find("CompletedBtn").gameObject.SetActive(true);
        //QuestCompletedCheck();
    }

    public void QuestCompletedCheck()
    {
        if(questCurCnt >= questMaxCnt)
        {
            
        }
    }

    

    //public void CompleteButton()
    //{
            
    //}
}