using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyJsonData : MonoBehaviour
{
    //������ ���� �� ������Ʈ�� ���� ������

    [Header("����Ʈ �̸�, ����")]
    public string trophyName; //����â�� ����ٰ� �˾�
    public string goalName;
    public int goal;

    [Header("����Ʈ ���Ǹ�����")] //��°�
    public int rewardItem; //��� ������, ������ ������ ���ڸ� �±׷� �޾Ƽ� ������ ȹ��
    public int rewardCount; //��� ������ ����

    [Header("��� Īȣ")]
    public string styleName;



    private void Start()
    {
       // gameObject.transform.GetChild(0).GetComponent<Text>().text = count.ToString();
    }

}

