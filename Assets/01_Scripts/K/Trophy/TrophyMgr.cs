using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; //########################��ܿ;���

public class TrophyMgr : MonoBehaviour
{

    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObject; //Prefab (Json char �޸�)

    public GameObject achievementContent;

    private void Start()
    {
        var jsonitemFile = Resources.Load<TextAsset>("Json/TrophyTable");
        txtFile = jsonitemFile;
        jsonObject = Resources.Load<GameObject>("Prefabs/Achievement");
        achievementContent = GameObject.Find("AchievementContent");


        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        for (int i = 1; i < jsonData["Achievement"].Count; i++) // ��� ¿���� ����� ���� ������ ����
        {
            InstachievementContent(i);
        }


    }

    public void InstachievementContent(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n-1; // �Ű�����


        GameObject character = Instantiate(jsonObject); // ����ž�

        character.transform.name = jsonData["Achievement"][item]["GoalName"].ToString(); // ������Ʈ�� ����

        character.GetComponent<TrophyJsonData>().trophyName = (jsonData["Achievement"][item]["TrophyName"]);
        character.GetComponent<TrophyJsonData>().goalName = jsonData["Achievement"][item]["GoalName"];
        character.GetComponent<TrophyJsonData>().goal = (int)(jsonData["Achievement"][item]["Goal"]);
      
        character.GetComponent<TrophyJsonData>().rewardItem = (int)(jsonData["Achievement"][item]["RewardItem"]);
        character.GetComponent<TrophyJsonData>().rewardCount = (int)(jsonData["Achievement"][item]["RewardCount"]);

        character.GetComponent<TrophyJsonData>().styleName = (jsonData["Achievement"][item]["StyleName"]);


        character.transform.SetParent(achievementContent.transform);

    } //���� �����




}
