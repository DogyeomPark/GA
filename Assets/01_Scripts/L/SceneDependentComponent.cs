using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDependentComponent : MonoBehaviour
{
    public ChatManager componentA;
    //public PlayerScript componentB;

    void Awake()
    {
        // ���� �� �̸� ��������
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("���� �� �̸� : "+currentSceneName);

        // ���� ���� ���� ������Ʈ A�� B�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
        if (currentSceneName == "Home")
        {
            // �� A������ ������Ʈ A Ȱ��ȭ, ������Ʈ B ��Ȱ��ȭ
            componentA.enabled = true;
            //componentB.enabled = false;
        }
        else // Ȩ�� �ƴҶ�, �ַδ����̳� ���̵�����϶�
        {
            // �� B������ ������Ʈ A ��Ȱ��ȭ, ������Ʈ B Ȱ��ȭ
            componentA.enabled = false;
            //componentB.enabled = true;
        }
    }
}
