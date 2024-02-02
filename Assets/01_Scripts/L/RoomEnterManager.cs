using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class RoomEnterManager : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;

    //�ַδ����� roomleave�ϰ� ���Ŵ����� ������

    public void LeaveVillige()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;

        switch (dataMgrDontDestroy.DungeonSortIdx)
        {
            case 1: // �̱۴���
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("Dungeon_1"); // �׽�Ʈ�� �ٲ����
                break;
            case 2: // ī��������
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("Dungeon_2"); // �׽�Ʈ�� �ٲ����
                break;
            case 3:
                PhotonNetwork.LeaveRoom(); // ���� ���� �����ϴ�.
                StartCoroutine(LoadLoadingScene());
                break;
            default:
                break;
        }
    }

    IEnumerator LoadLoadingScene()
    {
        yield return new WaitForSeconds(1.0f); // �ε� ������

        SceneManager.LoadScene("DungeonLoadingScene"); // �ε� ������ ��ȯ
    }

    // ���⿡ �ݹ��Լ� ����ɵ�
    #region ���� �ݹ� �Լ�
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("�涰���� �Ϸ�");
        Debug.Log("Lobby�� ���� �õ��մϴ�");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }
    #endregion
}
