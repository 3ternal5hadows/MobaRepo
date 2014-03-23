using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatManager : MonoBehaviour {
    //Chat stuff
    private List<string> chatMessages;
    private GUIStyle chatStyle;
    private Rect chatTextFieldRect;
    private bool isTyping;
    private int chatMaxChars;
    private string chatMessage;

    public Font font;
    public string playerName;
    public PlayerManager player;

	// Use this for initialization
	void Start () {
        //Chat stuff
        chatMessages = new List<string>();
        chatStyle = new GUIStyle();
        chatStyle.font = font;
        chatStyle.fontSize = 14;
        chatStyle.normal.textColor = Color.white;
        chatTextFieldRect = new Rect(20, Screen.height - 30, 400, 50);
        isTyping = false;
        chatMaxChars = 30;
        chatMessage = "";

        if (Network.isServer)
        {
            playerName = "Server";
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        //Open chat
        if (!isTyping & Input.GetKeyUp(KeyCode.Return))
        {
            isTyping = true;
        }
        //Close chat
        else if (Input.GetKeyDown(KeyCode.Escape) & isTyping)
        {
            chatMessage = "";
            isTyping = false;
        }
    }

    private void SetMessage()
    {
        if (chatMessage.Length > 0)
        {
            //We can make commands here
            if (chatMessage == "/suicide")
            {
                player.TakeDamage(1000000, player.playerNumber, false);
            }
            else
            {
                if (chatMessage.ToLower().Contains("greg") & chatMessage.ToLower().Contains("loser"))
                {
                    chatMessage = "Greg is a Boss";
                }
                LanguageFilter();
                networkView.RPC("SendChatMessage", RPCMode.AllBuffered, new object[] { playerName + ": " + chatMessage });
            }
        }
        chatMessage = "";
    }

    private void LanguageFilter()
    {
        foreach (string word in DataGod.wordsWeDontLike)
        {
            if (chatMessage.ToLower().Contains(word))
            {
                chatMessage = DataGod.GetRandomMessage();
            }
        }
    }

    void OnGUI()
    {
        if (isTyping)
        {
            GUI.SetNextControlName("chatTextField");
            chatMessage = GUI.TextField(chatTextFieldRect, chatMessage, chatStyle);
            GUI.FocusControl("chatTextField");

            if (Event.current.keyCode == KeyCode.Return & Event.current.type == EventType.KeyUp)
            {
                SetMessage();
                isTyping = false;
            }
        }
        Color tempColor = chatStyle.normal.textColor;
        for (int i = 0; i < chatMessages.Count; i++)
        {
            if (chatMessages[i].Length > 6)
            {
                if ((chatMessages[i].Remove(6)) == "Server")
                {
                    chatStyle.normal.textColor = Color.red;
                }
            }
            GUI.Label(new Rect(chatTextFieldRect.xMin, chatTextFieldRect.yMin + 25 * i - 25 * chatMessages.Count, chatTextFieldRect.width, chatTextFieldRect.height), chatMessages[i], chatStyle);
            chatStyle.normal.textColor = tempColor;
        }
    }

    [RPC]
    public void SendChatMessage(string message)
    {
        chatMessages.Add(message);
        if (chatMessages.Count > 5)
        {
            chatMessages.RemoveAt(0);
        }
    }
}
