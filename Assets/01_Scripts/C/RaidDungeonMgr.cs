using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidDungeonMgr : MonoBehaviour
{
<<<<<<< HEAD
    public int clearCut = 1;
    public RewardMgr rewardMgr;
    public RaidBossCtrl boss;
    public DataMgrDontDestroy dataMgrDontDestroy;
    public RaidQuestManager raidQuestManager;
    private void Start()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
    }
=======
    public int Count = 0;
    public Transform[] spawnPoint;
    public GameObject bossPrefab;
    public GameObject bossMakeEffect;
    public GameObject bossPhaseEffect;
>>>>>>> upstream/DEV

    public GameObject ground_1f;


    public void MakeBoss()
    {
        Count++; //���� ��ũ������ ���������� ī��Ʈ ���� �ʿ�.

        Debug.Log("�ܸ� " + Count + "���� ����.");
        Debug.Log("�������� " + (10 - Count) + "���� ����.");

        if (Count >= 10)
        {
<<<<<<< HEAD
            if (boss.GetComponent<RaidBossCtrl>().die == true)
            {
                ClearEndBoss();
                raidQuestManager.ClearEndBs(3);
            }
=======
            Instantiate(bossPrefab, spawnPoint[0]);
            Instantiate(bossMakeEffect, spawnPoint[0]);
>>>>>>> upstream/DEV
        }
    }

    public void BossPhase()
    {
        StartCoroutine(AngryBoss());
    }

    IEnumerator AngryBoss()
    {
        Destroy(ground_1f);
        yield return new WaitForSeconds(1.5f);
        Instantiate(bossPhaseEffect, spawnPoint[1]);
    }

    public void ClearEndBoss()
    {
        //clearPanel.SetActive(true);
    }
}
