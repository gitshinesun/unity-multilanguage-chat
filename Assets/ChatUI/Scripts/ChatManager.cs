using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

public class ChatManager : MonoBehaviour {

    public int ChatCount;
	public Transform content;
	public GameObject chatBarPrefab;
	public Sprite userSprite;
	public Color userImageColor;
	public Sprite userChatBarSprite;	
	public Color textColor;
	public int fontSize;
    public InputField ChatBox;
	private VerticalLayoutGroup verticalLayoutGroup;
    string path = string.Empty;    
    public static DataManager Instance { get; private set; }

    void Start () {
        DataManager.Instance.Get_ChatDataList();
        ShowUserMsg();
    }

    public void ShowUserMsg () {
		    
		for(int i = 0; i < DataManager.Instance.ChatList.Count; i++) {
			StartCoroutine(ShowUserMsgCoroutine (DataManager.Instance.ChatList[i]));
		}
	}

    bool onClickSend = false;
    public void onClickSendButton()
    {
        onClickSend = true;
    }
    
    private void Update()
    {
        if (ChatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return) || onClickSend)
            {
                int i = DataManager.Instance.ChatList.Count;
                string strChat = ChatBox.text;

                DataManager.Instance.Set_ChatDataList(strChat);
                StartCoroutine(ShowUserMsgCoroutine(strChat));

                ChatBox.text = "";
                onClickSend = false;
            }
        }
        else
        {
            if (!ChatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
                ChatBox.ActivateInputField();
        }
    }

	IEnumerator ShowUserMsgCoroutine (string msg) {

		GameObject chatObj = Instantiate(chatBarPrefab,Vector3.zero, Quaternion.identity) as GameObject;
		chatObj.transform.SetParent(content.transform,false);

		chatObj.SetActive(true);

		ChatListObject clb = chatObj.GetComponent<ChatListObject>();

        fontSize = (int)(Screen.height * 0.025f); //FontSize

        clb.parentText.fontSize = fontSize;
		clb.childText.fontSize = fontSize;
			
		clb.parentText.text = msg;

		clb.childText.color = Color.black;

		yield return new WaitForEndOfFrame();

		float height = chatObj.GetComponent<RectTransform>().rect.height;
		float width = chatObj.GetComponent<RectTransform>().rect.width;

		clb.chatbarImage.rectTransform.sizeDelta = new Vector2(width+80,height+80);        

		clb.childText.rectTransform.sizeDelta = new Vector2(width,height);
        clb.childText.rectTransform.position = new Vector2(clb.childText.rectTransform.position.x + 30, clb.childText.rectTransform.position.y - 30);        

		clb.childText.text = msg;

		clb.chatbarImage.color = new Color(userImageColor.r, userImageColor.g, userImageColor.b,1);
		clb.userImage.sprite = userSprite;

		clb.chatbarImage.sprite = userChatBarSprite;

		clb.userImage.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,clb.userImage.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
		clb.chatbarImage.rectTransform.anchoredPosition = new Vector2(-3f,clb.chatbarImage.rectTransform.anchoredPosition.y);        
	}
}
