using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardMgr : MonoBehaviour
{
    public static RewardMgr reward;
    public GameObject rewardContent;

    private void Start()
    {
        //imagelist = GameObject.Find("ImageList").GetComponent<ImageList>();
        rewardContent = GameObject.Find("RewardContent");
    }

    public void clear()
    {
        InstExp(3, 100);
    }
            ///�����Դϴ�. �̷��� �ҷ�������!!!
    public void MakeItem(int itemIdx, int count) // n��° �������� count�� ����
    {
        InstMaterial(itemIdx, count);
    }
    public void MakeItemOne(int itemIdx) // n��° �������� 1�� ����
    {
        InstMaterial(itemIdx, 1);
    }
    public void MakeItemRandomBtn()
    {
        int makeidx = Random.Range(1, 5);// 1���� 4���� ����
        InstMaterial(makeidx, 1);
    } // �������� ��� 1�� ��� ��ư
    public void SoloClearReward()
    {

    }
    public void ChaosClearReward()
    {

    }
    public void RaidClearReward()
    {

    }
    public void QuestClearReward()
    {

    }
    public void Reward100exp3EABtn()
    {
        //���ڸ� 3���� �����ҰԿ�!!!!!
        InstExp(3, 100);
        InstMaterial(3, 200);
        InstGOld(100); //3
    }






    // ����ȯ �Լ�
    public void InstMaterial(int n, int itemcount)
    {

        //character.GetComponent<Image>().sprite = imagelist.meterialsImage[n];

        //character.tag = "Material";
        //character.transform.SetParent(rewardContent.transform);

    } 


    //����ġ ���� ��ȯ �Լ�
    public void InstExp(int n, int itemcount) 
    {
        int item = n-1; // �Ű�����

        //character.tag = "Exp";
        //character.transform.SetParent(rewardContent.transform);

    } 


    // ��� ��ȯ �Լ�
    public void InstGOld(int itemcount)
    {

        //character.tag = "Gold";
        //character.transform.SetParent(rewardContent.transform);
    }



}