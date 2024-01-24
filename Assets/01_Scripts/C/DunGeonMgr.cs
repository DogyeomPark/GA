using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class DunGeonMgr : MonoBehaviour
{
    public Text discription;

    public int cursort; // ���� ���� ����(�̱�1, ī����2, ���̵�3)
    public int curIdx; // �̱�-1, �̱�-2 , �̱�-3


    public GameObject singlePanel;
    public GameObject chaosPanel;
    public GameObject raidPanel;

    public GameObject singleContent;
    public GameObject chaosContent;
    public GameObject raidContent;


    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObject; //Prefab (Json char �޸�)

    private void Awake()
    {
        singleContent = GameObject.Find("SingleContent");
        chaosContent = GameObject.Find("ChaosContent");
        raidContent = GameObject.Find("RaidContent");


        singlePanel = GameObject.Find("SinglePanel");
        chaosPanel = GameObject.Find("ChaosPanel");
        raidPanel = GameObject.Find("RaidPanel");

        discription = GameObject.Find("DungeonDescription").GetComponent<Text>();
    }

    private void Start()
    {
        var jsonitemFile = Resources.Load<TextAsset>("Json/DungeonList");
        txtFile = jsonitemFile;

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        for (int i = 1; i < jsonData["Single"].Count + 1; i++)
        {
            InstSingleDunGeon(i);
        }
        for (int i = 1; i < jsonData["Chaos"].Count + 1; i++)
        {
            InstChaosDunGeon(i);
        }
    }



    public void InstChaosDunGeon(int n)
    {
        {

            string json = txtFile.text;
            var jsonData = JSON.Parse(json);


            int idx = n - 1; // �Ű�����


            GameObject character = Instantiate(jsonObject); // ����ž�

            character.transform.name = jsonData["Chaos"][idx]["dungeonName"]; // ������Ʈ�� ����

            character.GetComponent<DunGeonInfo>().dunGeonName = (jsonData["Chaos"][idx]["dungeonName"]);
            character.GetComponent<DunGeonInfo>().discription = jsonData["Chaos"][idx]["discription"];
            character.GetComponent<DunGeonInfo>().difficult = (jsonData["Chaos"][idx]["difficulty"]);

            character.transform.SetParent(chaosContent.transform);

        }
    }
    public void InstSingleDunGeon(int n)
    {
        {

            string json = txtFile.text;
            var jsonData = JSON.Parse(json);


            int idx = n - 1; // �Ű�����


            GameObject character = Instantiate(jsonObject); // ����ž�

            character.transform.name = jsonData["Single"][idx]["dungeonName"]; // ������Ʈ�� ����

            character.GetComponent<DunGeonInfo>().dunGeonName = (jsonData["Single"][idx]["dungeonName"]);
            character.GetComponent<DunGeonInfo>().discription = jsonData["Single"][idx]["discription"];
            character.GetComponent<DunGeonInfo>().difficult = (jsonData["Single"][idx]["difficulty"]);

            character.transform.SetParent(singleContent.transform);

        }
    }
    public void OnSinglePanel()
    {
        singlePanel.SetActive(true);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(false);
        cursort = 1;
        //discription.text = 
    }
    public void OnChaosPanel()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(true);
        raidPanel.SetActive(false);
        cursort = 2;
    }
    public void OnRaidPanel()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(true);
        cursort =3;
    }

}
