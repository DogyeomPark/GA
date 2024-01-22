using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomEnterManager : MonoBehaviourPunCallbacks
{
    public GameObject dungeonPanel;

    public void OnSoloDungeon1ButtonClick()
    {
        JoinDungeon("Dungeon1_", 1); // �ַ� ����, �ִ� 1�� ���� ����
    }

    public void OnSoloDungeon2ButtonClick()
    {
        JoinDungeon("Dungeon2_", 1); // �ַ� ����, �ִ� 1�� ���� ����
    }

    public void OnRaidDungeonButtonClick()
    {
        JoinDungeon("RaidDungeon", 4); // ��Ƽ ����, �ִ� 4�� ���� ����
    }

    public void JoinDungeon(string dungeonType, int maxPlayers)
    {
        Debug.Log("������ Ÿ�� : " + dungeonType);
        Debug.Log("maxPlayers : " + maxPlayers);

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = (byte)maxPlayers,
            IsVisible = true, // ���� ����Ʈ�� ���̰� �� ������ ����
            IsOpen = true,    // ���� ���� �ִ��� ����
            CustomRoomProperties = new Hashtable { { "DungeonType", dungeonType } },
            CustomRoomPropertiesForLobby = new string[] { "DungeonType" }
        };

        // �濡 �����ϰų� ����
        PhotonNetwork.JoinOrCreateRoom(dungeonType, roomOptions, null);
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
    public override void OnJoinedRoom()
    {
        dungeonPanel.SetActive(false);

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

        //CreateDungeon();
    }
    #endregion

    // �������� UI panelŰ�� ��ư������ �Լ�
    public void OnClickEnterDungeonBtn()
    {
        dungeonPanel.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("���� �����ϴ�..");
    }
}
