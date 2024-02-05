using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDependentComponent : MonoBehaviour
{
    public ChatManager componentA;
    public Player componentB;
    public ChaosPlayerCtlr chaosPlayer;
    void Awake()
    {
        componentA = GetComponent<ChatManager>();
        // ���� �� �̸� ��������
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("���� �� �̸� : "+currentSceneName);

        // ���� ���� ���� ������Ʈ A�� B�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
        if (currentSceneName == "Town" || currentSceneName == "Raid")
        {
            // �� A������ ������Ʈ A Ȱ��ȭ, ������Ʈ B ��Ȱ��ȭ
            componentA.enabled = true;
            chaosPlayer.enabled = false;
            componentB.enabled = true;
            //componentB.enabled = false;
        }
        else // Ȩ�� �ƴҶ�, �ַδ����̳� ���̵�����϶�
        {
            // �� B������ ������Ʈ A ��Ȱ��ȭ, ������Ʈ B Ȱ��ȭ
            componentA.enabled = false;
            chaosPlayer.enabled = true;
            componentB.enabled = false;
            //componentB.enabled = true;
        }
    }
}
