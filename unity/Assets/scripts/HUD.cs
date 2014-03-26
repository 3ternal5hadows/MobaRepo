using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    //HUD stuff
    private PlayerManager player;

    public List<Sprite> powerCooldownFrames;
    public List<Sprite> normalCooldownFrames;
    public List<Sprite> weaponIcons;
    public List<Sprite> frames;
    public Sprite deathSkull;
    public bool showDeathInfo;
    public Sprite killsIcon;
    public Sprite deathsIcon;

    public Font font;
    private GUIStyle nameStyle;
    private GUIStyle killedByStyle;
    private GUIStyle killsAndDeathsStyle;
    private GUIStyle tooltipStyle;
    private Rect healthBarRect;
    private Rect killsIconRect;
    private Rect deathsIconRect;
    private Rect killCountRect;
    private Rect deathCountRect;
    private GUIStyle teamKillsStyle;

    private ScoreKeeper scoreKeeper;
    private NetworkManager networkManager;

    // Use this for initialization
    void Start()
    {
        showDeathInfo = false;
        nameStyle = new GUIStyle();
        nameStyle.font = font;
        nameStyle.fontSize = 100;
        nameStyle.normal.textColor = Color.red;
        killedByStyle = new GUIStyle();
        killedByStyle.font = font;
        killedByStyle.fontSize = 65;
        killedByStyle.normal.textColor = Color.white;

        killsAndDeathsStyle = new GUIStyle();
        killsAndDeathsStyle.font = font;
        killsAndDeathsStyle.fontSize = 30;
        killsAndDeathsStyle.normal.textColor = Color.white;
        tooltipStyle = new GUIStyle();
        tooltipStyle.font = font;
        tooltipStyle.fontSize = 20;
        tooltipStyle.normal.textColor = Color.white;

        healthBarRect = new Rect(15, 15, 220, 60);
        killsIconRect = new Rect(15, 85, 40, 40);
        deathsIconRect = new Rect(15, 130, 40, 40);
        killCountRect = new Rect(65, 85, 1, 1);
        deathCountRect = new Rect(65, 130, 1, 1);

        teamKillsStyle = new GUIStyle();
        teamKillsStyle.font = font;
        teamKillsStyle.fontSize = 50;
        teamKillsStyle.normal.textColor = Color.green;

        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnGUI()
    {
        if (networkView.isMine)
        {
            player = gameObject.GetComponent<PlayerManager>();
            int frame = (int)(player.GetHealthPercentage() * (frames.Count - 1));
            if (frame < 0)
            {
                frame = 0;
            }
            GUI.DrawTexture(healthBarRect, frames[frame].texture);
            GUI.DrawTexture(killsIconRect, killsIcon.texture);
            GUI.DrawTexture(deathsIconRect, deathsIcon.texture);
            string str = player.kills + "";
            Vector2 size = killsAndDeathsStyle.CalcSize(new GUIContent(str));
            killCountRect.width = size.x;
            killCountRect.height = size.y;
            DrawWithGlow(killCountRect, str, killsAndDeathsStyle, Color.black, 3);
            str = player.deaths + "";
            size = killsAndDeathsStyle.CalcSize(new GUIContent(str));
            deathCountRect.width = size.x;
            deathCountRect.height = size.y;
            DrawWithGlow(deathCountRect, str, killsAndDeathsStyle, Color.black, 3);

            DrawTeamScore(player.teamNumber);
            for (int i = 0; i < scoreKeeper.teamScore.Length; i++)
            {
                if (i != player.teamNumber)
                {
                    DrawTeamScore(i);
                }
            }

            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            string tooltip = "";

            if (healthBarRect.Contains(mousePosition))
            {
                tooltip = "Health";
            }
            else if (new Rect(killsIconRect.xMin, killsIconRect.yMin, killCountRect.xMax - killsIconRect.xMin, killsIconRect.height).Contains(mousePosition))
            {
                tooltip = "Kills";
            }
            else if (new Rect(deathsIconRect.xMin, deathsIconRect.yMin, deathCountRect.xMax - deathsIconRect.xMin, deathsIconRect.height).Contains(mousePosition))
            {
                tooltip = "Deaths";
            }

            if (tooltip != "")
            {
                size = tooltipStyle.CalcSize(new GUIContent(tooltip));
                DrawWithGlow(new Rect(mousePosition.x, mousePosition.y - 20, size.x, size.y), tooltip, tooltipStyle, Color.black, 2);
            }

            if (showDeathInfo)
            {
                string killerName = networkManager.allPlayers[player.killer].name;
                string killedByString = "Killed by";
                Vector2 stringSize = nameStyle.CalcSize(new GUIContent(killerName));
                Vector2 stringSize2 = nameStyle.CalcSize(new GUIContent(killedByString));
                Vector2 skullSize = new Vector2(deathSkull.texture.width, deathSkull.texture.height);
                if (stringSize2.x > stringSize.x)
                {
                    stringSize = stringSize2;
                }
                stringSize.y = 0;
                Vector2 totalSize = skullSize + stringSize;
                GUI.DrawTexture(new Rect(Screen.width / 2 - totalSize.x / 2, Screen.height / 2 - totalSize.y / 2,
                    skullSize.x, skullSize.y), deathSkull.texture);
                DrawWithGlow(new Rect(Screen.width / 2 - totalSize.x / 2 + skullSize.x, Screen.height / 2 - totalSize.y / 2 + 60,
                    skullSize.x, skullSize.y), killerName, nameStyle, Color.black, 5);
                DrawWithGlow(new Rect(Screen.width / 2 - totalSize.x / 2 + skullSize.x, Screen.height / 2 - totalSize.y / 2 + 10,
                    skullSize.x, skullSize.y), killedByString, killedByStyle, Color.black, 5);
            }
            GUI.DrawTexture(new Rect(Screen.width - 100, Screen.height - 70, 30, 30), player.unequippedWeapon.icon.texture);
            GUI.DrawTexture(new Rect(Screen.width - 160, Screen.height - 60, 50, 50), player.leftWeapon.icon.texture);
            GUI.DrawTexture(new Rect(Screen.width - 60, Screen.height - 60, 50, 50), player.rightWeapon.icon.texture);
        }
    }

    private void DrawTeamScore(int i)
    {
        string str = "" + scoreKeeper.teamScore[i];
        Vector2 size = teamKillsStyle.CalcSize(new GUIContent(str));
        if (player.teamNumber == i)
        {
            teamKillsStyle.normal.textColor = Color.green;
        }
        else
        {
            teamKillsStyle.normal.textColor = Color.red;
        }
        Rect teamKillsRect = new Rect(Screen.width / 2 + i * 50 - size.x / 2 - (scoreKeeper.numTeams - 1) * 25, 30 - size.y / 2, size.x, size.y);
        DrawWithGlow(teamKillsRect, str, teamKillsStyle, Color.black, 4);
    }

    private void DrawWithGlow(Rect rect, string str, GUIStyle guiStyle, Color glowColor, int glowSize)
    {
        Vector2[] offsets = new Vector2[8] { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1),
                                             new Vector2(1,1),new Vector2(-1,1),new Vector2(1,-1),new Vector2(-1,-1)};
        Color tempColor = guiStyle.normal.textColor;
        guiStyle.normal.textColor = Color.black * (1 / (float)glowSize);

        for (int n = 0; n < glowSize; n++)
        {
            for (int i = 0; i < offsets.Length; i++)
            {
                GUI.Label(new Rect(rect.xMin + offsets[i].x * (n + 1), rect.yMin + offsets[i].y * (n + 1), rect.width, rect.height), str, guiStyle);
            }
        }

        guiStyle.normal.textColor = tempColor;
        GUI.Label(rect, str, guiStyle);
    }
}
