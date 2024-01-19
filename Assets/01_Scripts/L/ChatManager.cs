using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public List<string> chatList = new List<string>();
    public Button sendBtn;
    public Text chatLog;
    public Text chattingList;
    public InputField inputChat;
    public ScrollRect scroll_rect;
    public string chatters;

//    void Start()
//    {
//        PhotonNetwork.IsMessageQueueRunning = true;
//        //scroll_rect = GameObject.FindObjectOfType<ScrollRect>();
//    }

//    public void OnClickSendBtn()
//    {
//        if(inputChat.text.Equals(""))
//        {
//            Debug.Log("Empty, ä��â�� ��Ȱ��ȭ �մϴ�");
//            // ä��â ��������� ��Ȱ��ȭ
//            inputChat.Select();
//            return; 
//        }
//        string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, inputChat.text);
//        Debug.Log(msg);
//        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
//        inputChat.ActivateInputField(); // �޼����� ������ Ȱ��ȭ
//        inputChat.text = "";
//    }

//    void Update()
//    {
//        ChatterUpdate();
//        // enterŰ�� ������ inputChat�� ��Ŀ���� ������������ ����(isFocused�� ���������������� true�� ��ȯ��)
//        if (Input.GetKeyDown(KeyCode.Return) && !inputChat.isFocused)
//        {
//            Debug.Log("enterŰ ����, msg����");
//            OnClickSendBtn();
//        }
//        if(Input.GetKeyDown(KeyCode.Return) && inputChat.isFocused)
//        {
//            Debug.Log("enterŰ ���� Focused ��������������");
//        }
//    }

//    void ChatterUpdate()
//    {
//        chatters = "PlayerList\n";
//        foreach(Player p in PhotonNetwork.PlayerList)
//        {
//            chatters += p.NickName + "\n";
//        }
//        chattingList.text = chatters;
//    }

//    [PunRPC]
//    public void ReceiveMsg(string msg)
//    {
//        chatLog.text += "\n" + msg;
//        scroll_rect.verticalNormalizedPosition = 0.0f;
//    }
}
