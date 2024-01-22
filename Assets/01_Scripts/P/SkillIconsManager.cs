using JetBrains.Annotations;

using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconsManager : MonoBehaviour
{
   // public StateManager stateManager;
    public Player player;

    public Image coolTImeFillQ; //�����ܿ� �ʾ��Ʈ �����ð�
    public Image coolTImeFillE;
    public Image coolTImeFillR;

    public Image qskillIcon;
    public Image eskillIcon;
    public Image rskillIcon;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        if(player != null)
        {
            coolTImeFillQ = GameObject.Find("CoolTImeFillQ").GetComponent<Image>();
            coolTImeFillE = GameObject.Find("CoolTimeFillE").GetComponent<Image>();
            coolTImeFillR = GameObject.Find("CoolTimeFillR").GetComponent<Image>();

            qskillIcon = GameObject.Find("SkillIconQ").GetComponent<Image>();
            qskillIcon = GameObject.Find("SkillIconE").GetComponent<Image>();
            qskillIcon = GameObject.Find("SkillIconR").GetComponent<Image>();
        }

    }

    void Update()
    {
        if(player != null)
        {
            coolTImeFillQ.fillAmount = player.Qskillcool / player.CurQskillcool;
            coolTImeFillE.fillAmount = player.Eskillcool / player.CurEskillcool;
            coolTImeFillR.fillAmount = player.Rskillcool / player.CurRskillcool;

        }
        else
        {
            Debug.Log("�÷��̾� ��ã��");
        }


    }
}
