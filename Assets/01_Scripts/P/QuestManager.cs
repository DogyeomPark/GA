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

    Queue<string> naming = new Queue<string>();
    Queue<string> sentence = new Queue<string>();

    public Text rewardExp;
    public Text rewardMat;
    public Text rewardGold;

    [Header("����Ʈ �����ư")]
    public GameObject acceptBtn;
    public GameObject ingImg;
    public GameObject completedBtn;
    public GameObject endBtn;

    [Header("����Ʈ ���� ���൵ â")]
    public GameObject questPopUpPanel;
    //public bool questPopUpPanelVisible;
    public Text questDescriptionGoalTxt; //�� �ؽ�Ʈ�� questGoalTxt �� ���ڰ� ��
    public string questGoalTxt; 
    public int questCurCnt;
    public int questMaxCnt;


    [Header("����Ʈ ����")]
    public GameObject rewardPanel;
    public RewardMgr rewardMgr;
    public int expPotionReward;
    public int materialReward;
    public int goldReward;

    public bool isFirst;
    public void QuestClearReward(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int item = n - 1; //�Ű�����

        rewardMgr = GameObject.Find("RewardMgr").GetComponent<RewardMgr>();
        expPotionReward=(jsonData["Quest"][item]["RewardExp"]);
        materialReward=(jsonData["Quest"][item]["RewardMat"]);
        goldReward=(jsonData["Quest"][item]["RewardGold"]);
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
        descriptionPanel.SetActive(false);
    }
    private void Start()
    {
        //ingBtn.SetActive(false);
        completedBtn.SetActive(false);
        ingImg.SetActive(false);
        endBtn.SetActive(false);

        questPanel.SetActive(false);
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
                QuestCompletedCheck();
                nPCConversation.SetActive(true);
                if (isFirst)
                {
                    dialogueTrigger.Trigger();
                }
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
        rewardExp.text = (jsonData["Quest"][item]["RewardExp"]);
        rewardMat.text = (jsonData["Quest"][item]["RewardMaterial"]);
        rewardGold.text = (jsonData["Quest"][item]["RewardGold"]);
        Debug.Log("Json ������ �ҷ���");
        
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
        isFirst = true;
        //uIMgr.UpdateQuestPopUpInfo(questPopUpManager.questGoalTxt.text, questPopUpManager.questCountTxt.text);
    }

    public void ReceiveQuest(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);
        int item = n - 1;

        if (jsonData["Quest"][item]["Goal"] != null)
        {
            dataMgrDontDestroy.GoalTxt = jsonData["Quest"][item]["Goal"];
        }
        else
        {
            // ���� ó�� �Ǵ� �⺻�� �Ҵ�
            dataMgrDontDestroy.GoalTxt = "Default Goal Text";
        }
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
            completedBtn.SetActive(false);
            endBtn.SetActive(true);
            // ���� ����Ʈ�� ������ �� ���� ��ư�� �������� ����
            //acceptBtn.SetActive(true);
            //ingImg.SetActive(false);
            //completedBtn.SetActive(false); 

            // ����Ʈ �ε��� ���� �� ������ �Ŵ��� ����
            dataMgrDontDestroy.questIdx++;
            dataMgrDontDestroy.QuestIdx = questPopUpManager.questIdx;

            // ����Ʈ �˾� �г� ��Ȱ��ȭ
            questPopUpPanel.SetActive(false);

            //����Ʈ ���� �˾� Ȱ��ȭ
            rewardPanel.SetActive(true);
            QuestClearReward(dataMgrDontDestroy.questIdx);
            //����Ʈ �Ϸ�� ����
            //if (questCurCnt >= questMaxCnt)
            //{

            //}
            //transform.Find("IngImg").gameObject.SetActive(false);
            //transform.Find("CompletedBtn").gameObject.SetActive(true);
        }
       

    }

    public void QuestCompletedCheck()
    {
        if(dataMgrDontDestroy.questCurCnt >= dataMgrDontDestroy.questMaxCnt && isFirst)
        {
            completedBtn.SetActive(true);
        }
    }

    

    //public void CompleteButton()
    //{
            
    //}
}