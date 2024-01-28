using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomEnterManager : MonoBehaviourPunCallbacks
{
    public GameObject dungeonPanel;

    public static string dungeonType;

    //�ַδ����� roomleave�ϰ� ���Ŵ����� ������
    public void OnSoloDungeon1ButtonClick()
    {
        JoinDungeon("Solo1Dungeon", 1);
        PhotonNetwork.LeaveRoom();
    }

    public void OnSoloDungeon2ButtonClick()
    {
        JoinDungeon("Solo2Dungeon", 1);
        PhotonNetwork.LeaveRoom();
    }

    public void OnRaidDungeonButtonClick()
    {
        JoinDungeon("RaidDungeon", 3); // ��Ƽ ����, �ִ� 3�� ���� ����
        PhotonNetwork.LeaveRoom();
    }

    public void JoinDungeon(string typeName, int maxPlayers)
    {
        dungeonType = typeName;
        Debug.Log("������ Ÿ�� : " + typeName);
        Debug.Log("maxPlayers : " + maxPlayers);

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = (byte)maxPlayers,
            IsVisible = true, // ���� ����Ʈ�� ���̰� �� ������ ����
            IsOpen = true,    // ���� ���� �ִ��� ����
            CustomRoomProperties = new Hashtable { { "DungeonType", typeName } },
            CustomRoomPropertiesForLobby = new string[] { "DungeonType" }
        };

        // �濡 �����ϰų� ����
        //PhotonNetwork.JoinOrCreateRoom(dungeonType, roomOptions, null);
    }

    // userid �ʿ��ϸ� ���
    public void SetUserId()
    {
        // ���� �̸� ��������
        string userId = PlayerPrefs.GetString("USER_ID");
        // ���� ������ �г��� ���
        PhotonNetwork.NickName = userId;
    }

    #region ���� �ݹ� �Լ�
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("�涰���� �Ϸ�");
        Debug.Log("Lobby�� ���� �õ��մϴ�");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Lobby�� ���� �Ϸ�");
        
    }
    public override void OnJoinedRoom()
    {
        // ���� ���� ���� Ÿ���� ������
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("DungeonType", out object dungeonTypeObj))
        {
            string dungeonType = dungeonTypeObj.ToString();
            Debug.Log("������ �̸��� : " + dungeonType);
            // ������ ���
            if (PhotonNetwork.IsMasterClient)
            {
                // �ش� ���� Ÿ�Կ� �´� ���� �ε�
                PhotonNetwork.LoadLevel(dungeonType);
            }
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }
    #endregion

    // �������� UI panelŰ�� ��ư������ �Լ�
    public void OnClickEnterDungeonBtn()
    {
        dungeonPanel.SetActive(true);
    }
    //�������� UI panel���� ��ư������ �Լ�
    public void OnClickCancelBtn()
    {
        dungeonPanel.SetActive(false);
    }
}
