using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoadPlayerInfo : MonoBehaviour
{
    private FirebaseFirestore db;
    public string curUserEmail;

    public Text nickNameTxt;
    public Text classNameTxt;
    public Text levelTxt;

    public CharacterCreate characterCreate;

    private void Start()
    {
        // �ٸ� ��ũ��Ʈ(LoginSystem ��)���� userEmail ��������
        curUserEmail = LoginSystem_test.userEmail;

        // Firebase Firestore �ʱ�ȭ
        db = FirebaseFirestore.DefaultInstance;

        characterCreate = GameObject.Find("CharacterCreate").GetComponent<CharacterCreate>();

        // �÷��̾� ���� �ε� �ڷ�ƾ ����
        StartCoroutine(LoadPlayerData(curUserEmail));
    }

    public IEnumerator LoadPlayerData(string userEmail)
    {
        // users �÷��ǿ��� userEmail(ID����)�� �� �̸��� ���� �б�
        DocumentReference userDocRef = db.Collection("users").Document(userEmail);
        AggregateQuery cnt = userDocRef.Collection(userEmail).Count;
        Debug.Log(cnt);
        // ����� ������ �÷��ǵ� ��������
        yield return userDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            Debug.Log(snapshot.ToDictionary().Count); //���߿� ī��Ʈ��ŭ ������Ʈ������ɵ�.
            if (snapshot.Exists)
            {
                Debug.Log("Document ID: " + snapshot.Id);

                for(int i = 1; i <= 3; i++)
                {
                    // userEmail �������� ĳ����1 �÷����� Info���� ����
                    DocumentReference characterInfoDocRef = userDocRef.Collection("ĳ����" + i).Document("Info");

                    // ������ �б�
                    characterInfoDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("ĳ���� ���� �б� ����: " + task.Exception);
                            return;
                        }

                        DocumentSnapshot characterSnapshot = task.Result;

                        if (characterSnapshot.Exists)
                        {
                            // ĳ���� ���� ��������
                            IDictionary<string, object> characterData = characterSnapshot.ToDictionary();

                            // characterData���� SlotNum �� Ȯ��
                            if (characterData.ContainsKey("SlotNum"))
                            {
                                int slotNum = (int)characterData["SlotNum"];
                                PlayerPrefs.SetInt("SlotNum" + slotNum, slotNum);
                                // �� ĳ������ ������ PlayerPrefs�� ����
                                if (characterData.ContainsKey("NickName"))
                                    PlayerPrefs.SetString("NickName_" + slotNum, (string)characterData["NickName"]);

                                if (characterData.ContainsKey("Class"))
                                    PlayerPrefs.SetString("Class_" + slotNum, (string)characterData["Class"]);

                                if (characterData.ContainsKey("CharacterLevel"))
                                    PlayerPrefs.SetInt("CharacterLevel_" + slotNum, (int)characterData["CharacterLevel"]);

                                PlayerPrefs.Save();
                            }
                            // UI�� ���� ǥ��
                            //UpdateUI(characterData);
                            UpdateUI();
                        }
                        else
                        {
                            Debug.Log("ĳ���� ���� ã������.");
                        }
                    });
                }
                
            }
        });
    }

    //public void UpdateUI(IDictionary<string, object> characterData)
    public void UpdateUI()
    {
        Debug.Log("UpdateUI ����");
        if (PlayerPrefs.HasKey("SlotNum"))
        {
            int slotNum = PlayerPrefs.GetInt("SlotNum");
            string nickName = PlayerPrefs.GetString("NickName_" + slotNum);
            string className = PlayerPrefs.GetString("Class_" + slotNum);
            int level = PlayerPrefs.GetInt("CharacterLevel_" + slotNum);
            nickNameTxt.text = "�г��� : " + nickName;
            classNameTxt.text = "���� : " + className;
            levelTxt.text = "���� : " + level;
        }
    }
}