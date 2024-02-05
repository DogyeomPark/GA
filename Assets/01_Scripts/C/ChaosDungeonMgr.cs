using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaosDungeonMgr : MonoBehaviour
{

    public GameObject ground1;
    public GameObject ground2;
    public GameObject ground3;
    public GameObject ground4;

    public bool isBattle;           //���� �� �� ����.

    public GameObject reset;        //�������� ���µǴ� �÷��̾�
    public GameObject[] bossPrefab; //bossPrefab[0]�� ��ĭ
    public Transform[] spawnPoint;

    public GameObject midBossEffect;
    public GameObject endBossEffect;

    public MageMiddleBoss mageMiddle;
    public Tboss tboss;

    private void LateUpdate()
    {
        if (mageMiddle != null)
        {
            mageMiddle = GameObject.FindGameObjectWithTag("Boss").GetComponent<MageMiddleBoss>();
        }
        if (tboss != null)
        {
            tboss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Tboss>();
        }
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ground1)
        {
            InstBoss1();
            isBattle = true;
        }
        if (other.CompareTag("Player") && ground2)
        {
            InstBoss2();
            isBattle = true;
        }
        if (other.CompareTag("Player") && ground3)
        {
            InstBoss3();
            isBattle = true;
        }
        if (other.CompareTag("Player") && ground4)
        {
            InstBoss4();
            isBattle = true;
        }
    }

    public void InstBoss1()
    {
        Instantiate(midBossEffect, spawnPoint[1]);
        Instantiate(bossPrefab[1], spawnPoint[1]);
        Door();
    }
   
    public void InstBoss2()
    {
        Instantiate(midBossEffect, spawnPoint[2]);
        Instantiate(bossPrefab[2], spawnPoint[2]);
        Door();
    }
    public void InstBoss3()
    {
        Instantiate(midBossEffect, spawnPoint[3]);
        Instantiate(bossPrefab[3], spawnPoint[3]);
        Door();
    }
    public void ClearMidBoss() //�߰� �������������� ȣ���ؾ���.
    {
        if(mageMiddle.GetComponent<StateManager>().hp <= 0)
        {
            isBattle = false;
            Door();
        }
    }
    public void InstBoss4()
    {
        StartCoroutine(InstChaosBoss());
        Door();
    }

    IEnumerator InstChaosBoss()
    {
        Instantiate(endBossEffect, spawnPoint[4]);
        yield return new WaitForSeconds(1.5f);
        Instantiate(bossPrefab[4], spawnPoint[4]);
    }

    public void Door() //������ �޷�����. �ο� �����ϰų� ���� �� ȣ�� �ʿ�.
    {
        Jun_TweenRuntime[] gameObjects = GetComponents<Jun_TweenRuntime>();

        if (isBattle)
        {
            gameObjects[0].Play();
        }
        else
        {
            gameObjects[1].Play();
        }
    }

    

    public void ClearEndBoss()
    {
        if(tboss.GetComponent<StateManager>().hp <= 0)
        {
            //clearPanel.SetActive(true);
        }
        
    }

    public void Update()
    {
        ClearMidBoss();
        ClearEndBoss();
       // if (reset.transform.position.y < -7f)
       //      ResetPlayer();

    }

    public void ResetPlayer()
    {
        reset.transform.position = spawnPoint[0].position;
    }

}
