using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; //########################��ܿ;���

public class RewardMgr : MonoBehaviour
{
    public static RewardMgr reward;
    public InventoryManager inventoryMgr;
    public ImageList imagelist;


    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObject; //Prefab (Json char �޸�)


    public GameObject rewardContent;


    private void Start()
    {
        inventoryMgr = GameObject.Find("InventoryMgr").GetComponent<InventoryManager>();
        imagelist = GameObject.Find("ImageList").GetComponent<ImageList>();
        var jsonitemFile = Resources.Load<TextAsset>("Json/ItemList");
        txtFile = jsonitemFile;

        jsonObject = Resources.Load<GameObject>("Prefabs/item");
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

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n-1; // �Ű�����


        GameObject character = Instantiate(jsonObject); // ����ž�

        character.transform.name = jsonData["Weapon"][item]["Name"]; // ������Ʈ�� ����

        character.GetComponent<ItemJsonData>().charname = (jsonData["Weapon"][item]["Name"]);
        character.GetComponent<ItemJsonData>().discription = (jsonData["Weapon"][item]["Discription"]);
        character.GetComponent<ItemJsonData>().atk = (int)(jsonData["Weapon"][item]["Str"]);
        character.GetComponent<ItemJsonData>().rarity = (int)(jsonData["Weapon"][item]["Rarity"]);
        character.GetComponent<ItemJsonData>().count += itemcount;
        Debug.Log(jsonData["Weapon"][item]["Name"]);
        character.GetComponent<Image>().sprite = imagelist.meterialsImage[n];

        character.tag = "Material";
        character.transform.SetParent(rewardContent.transform);

    } 


             //����ġ ���� ��ȯ �Լ�
    public void InstExp(int n, int itemcount) 
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n-1; // �Ű�����

        GameObject character = Instantiate(jsonObject); // ����ž�

        character.transform.name = jsonData["Food"][item]["Name"]; // ������Ʈ�� ����

        character.GetComponent<ItemJsonData>().charname = (jsonData["Food"][item]["Name"]);
        character.GetComponent<ItemJsonData>().discription = (jsonData["Food"][item]["Discription"]);
        character.GetComponent<ItemJsonData>().exp = (int)(jsonData["Food"][item]["exp"]);
        character.GetComponent<ItemJsonData>().rarity = (int)(jsonData["Food"][item]["Rarity"]);
        character.GetComponent<ItemJsonData>().count += itemcount;
        Debug.Log(jsonData["Food"][item]["Name"]);
        character.GetComponent<Image>().sprite = imagelist.expPotionImage[n];

        character.tag = "Exp";
        character.transform.SetParent(rewardContent.transform);

    } 


             // ��� ��ȯ �Լ�
    public void InstGOld(int itemcount)
    {
        GameObject character = Instantiate(jsonObject); // ����ž�

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);
        character.GetComponent<ItemJsonData>().count += itemcount;
        character.GetComponent<Image>().sprite = imagelist.goldImage[1];
        character.tag = "Gold";
        character.transform.SetParent(rewardContent.transform);
    }



}