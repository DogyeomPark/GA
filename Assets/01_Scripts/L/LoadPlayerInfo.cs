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

    IEnumerator LoadPlayerData(string userEmail)
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

                // userEmail �������� ĳ����1 �÷����� Info���� ����
                DocumentReference characterInfoDocRef = userDocRef.Collection("ĳ����1").Document("Info");

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

                        // UI�� ���� ǥ��
                        UpdateUI(characterData);
                }
                else
                {
                    Debug.Log("ĳ���� ���� ã������.");
                }
                });
            }
        });
    }

    public void UpdateUI(IDictionary<string, object> characterData)
    {
        Debug.Log("UpdateUI ����");
        nickNameTxt.text = "�г���: " + characterData["NickName"];
        classNameTxt.text = "����: " + characterData["Class"];
        levelTxt.text = "����: " + characterData["CharacterLevel"];

        //Text[] infoText = characterCreate.slots[CharacterCreate.currentSlotNum].GetComponentsInChildren<Text>();

        //infoText[0].text = "�г���: " + characterData["NickName"].ToString();
        //infoText[1].text = "����: " + characterData["Class"].ToString();
        //infoText[2].text = "����: " + characterData["CharacterLevel"].ToString();
    }
}