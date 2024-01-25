using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using Firebase;
using UnityEngine.SceneManagement;

public class LoginSystem_test : MonoBehaviour
{
    public static string userEmail;
    public string password;
    public InputField emailInput;
    public InputField pwInput;

    public Text outputText;

    public bool isExist = false;
    private FirebaseAuth auth; // �α��� or ȸ������ � ���
    private FirebaseUser user; // ������ �Ϸ�� ���� ����

    void Start()
    {
        LoginState += OnChangedState;
        Init();
    }

    public string UserId => user.UserId;

    public Action<bool> LoginState;

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;

        // �ӽ� ó��
        if (auth.CurrentUser != null)
        {
            Logout();
        }
        auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signed = auth.CurrentUser != user && auth.CurrentUser != null;
            if (!signed && user != null)
            {
                Debug.Log("�α׾ƿ�");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("�α���");
                LoginState?.Invoke(true);
            }
        }
    }
    
    // �ű����� ������ FireStore�� �ۼ�
    IEnumerator CreateUserInFirestore(string userEmail, string userPassword)
    {
        Debug.Log("�ڷ�ƾ ����");

        // Firestore�� ����� ������ �߰�
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(userEmail);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            {"UserPw", userPassword },
            {"UpdateTime", FieldValue.ServerTimestamp }
        };

        yield return docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            Debug.Log("�������ۼ� ����");
            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.InnerExceptions)
                {
                    if (exception is FirebaseException firebaseException)
                    {
                        Debug.LogError($"FirebaseException: {firebaseException.ErrorCode} - {firebaseException.Message}");
                    }
                    else
                    {
                        Debug.LogError($"Exception: {exception}");
                    }
                }
                Debug.Log("�������ۼ� ����");
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("�������ۼ� ���");
            }
            else
            {
                Debug.Log($"{userEmail} �� ȸ�������� �Ϸ�Ǿ����ϴ�...");
                Debug.Log("�������ۼ� ��");
            }
        });
        Debug.Log("�ڷ�ƾ ����");
    }

    public void OnClickCreateBtn()
    {
        CheckingEmailAndPw();

        auth.CreateUserWithEmailAndPasswordAsync(userEmail, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("ȸ������ ���");
                return;
            }
            if (task.IsFaulted)
            {
                // ȸ������ ���� ���� => �̸����� ������ / ��й�ȣ�� �ʹ� ���� / �̹� ���Ե� �̸��� ���..
                Debug.Log("ȸ������ ����");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("ȸ������ �Ϸ�");

            // ȸ�������� �Ϸ�� �Ŀ� �ٸ� ������ ������ �� ����
            StartCoroutine(CreateUserInFirestore(userEmail, password));
        });
    }

    // �������� ������ �ҷ�����
    IEnumerator ReadUserData()
    {
        Debug.Log("�ڷ�ƾ ����");
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(userEmail);
        yield return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists) // �̸����� �ִٸ�...
            {
                Debug.Log($"{snapshot.Id}");
                isExist = true;
                Dictionary<string, object> doc = snapshot.ToDictionary();

                foreach (KeyValuePair<string, object> pair in doc)
                {
                    if (pair.Key == "UserPw")
                    {
                        Debug.Log("Password :: " + pair.Value.ToString());
                    }
                }
            }
            else
            {
                Debug.Log($"{userEmail} �� �������� �ʽ��ϴ�...");
                outputText.text = $"{userEmail} �� �������� �ʽ��ϴ�...";
                isExist = false;
            }
        });
        Debug.Log("�ڷ�ƾ ����");
    }

    public void OnClickLoginBtn()
    {
        CheckingEmailAndPw();
        auth.SignInWithEmailAndPasswordAsync(userEmail, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("�α��� ���");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("�α��� ����");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("�α��� �Ϸ�");

            StartCoroutine(ReadUserDataAndLoadScene());
        });
    }

    IEnumerator ReadUserDataAndLoadScene()
    {
        yield return StartCoroutine(ReadUserData());
        SceneManager.LoadScene("Lobby_test");
    }

    public void Logout()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }

    public void CheckingEmailAndPw()
    {
        userEmail = emailInput.text;
        password = pwInput.text;
    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "�α��� : " : "�α׾ƿ� : ";
        outputText.text += UserId;
    }
}
