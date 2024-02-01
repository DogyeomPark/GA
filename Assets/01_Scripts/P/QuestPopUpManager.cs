using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopUpManager : MonoBehaviour
{
    public DataMgr dataMgr;
    
    public int questIndex; //���� ����Ʈ ��ȣ

    public Text questCountTxt;

    public string goal;
    public int curCnt;
    public int maxCnt;

    public bool isCompleted;

    private void Start()
    {
        dataMgr = DataMgr.instance;

        questCountTxt = GameObject.Find("QCountTxt").GetComponent<Text>();
        questIndex = dataMgr.CurQuestIndex;
        goal = dataMgr.Goal;
        curCnt = dataMgr.CurCnt;
        maxCnt = dataMgr.MaxCnt;
    }


    public enum QuestCondition
    {
        Talk = 1,
        NormalMonKill,
        BossKill,
        GotGold,
        Buy
    }

    private QuestCondition qcondition;

    public void QuestIndexUp(int n) //����Ʈ �޼��� ����
    {
        qcondition = (QuestCondition)n; // ���� �̳Ѱ��� 1, 2 �� ���ž�
        if (questIndex == n)
        { curCnt++;}

        InitCurQuest();
    }

    public void InitCurQuest() //�۾� �ʱ�ȭ
    {
        questCountTxt.text = $"({curCnt} /  {maxCnt} )";

        if(curCnt >= maxCnt)
        {
            questCountTxt.color = Color.yellow;
            isCompleted = true;
        }
        else
        {
            questCountTxt.color = Color.white;
        }
    }


}



