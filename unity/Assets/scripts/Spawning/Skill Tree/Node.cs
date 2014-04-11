using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public List<GameObject> adjacentNodes;
    public bool isStartingNode;
    private int nodeLevel;
    public int NodeLevel
    {
        get { return nodeLevel; }
        set
        {
            int newLevel = value;
            int difference = newLevel - NodeLevel;
            if (difference > 0)
            {
                if (NodeLevel + difference <= DataGod.MAXIMUM_NODE_LEVEL)
                {
                    if (DataGod.talentPoints >= difference)
                    {
                        nodeLevel += difference;
                        DataGod.talentPoints -= difference;
                    }
                }
            }
            else
            {
                if (NodeLevel + difference >= 0)
                {
                    nodeLevel += difference;
                    DataGod.talentPoints -= difference;
                }
            }
            if (!isStartingNode)
            {
                WeaponData.treeData[WeaponData.currentTree, ID] = nodeLevel;
            }
            SetText();
        }
    }
    private SkillTreeManager input;
    private bool mouseOver;
    private Vector2 position;
    private Vector2 mousePosition;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Sprite mouseOverLockedSprite;
    public Sprite mouseOverUnlockedSprite;
    public Sprite lockedSpriteWithPoints;
    private Transform unlockedEffect;
    private SpriteRenderer unlockedEffectSpriteRenderer;
    private GameObject secondStar;
    public GameObject connectionParticle;
    private Timer connectionParticleTimer;
    private float unlockedEffectAlpha;
    private bool unlockedEffectEnabled;
    public int ID;
    private GUIStyle tooltipStyle;
    public Font font;
    public Sprite tooltipOutline;
    public Sprite tooltipBackground;

    private const float NODE_COLLISION_RADIUS = 0.25f;

    /// <summary>
    /// Returns true if node has at least 1 point in it
    /// </summary>
    public bool Activated
    {
        get { return (NodeLevel > 0); }
    }
    /// <summary>
    /// Returns true if no more points can be added to this node
    /// </summary>
    public bool Maxed
    {
        get { return (NodeLevel >= DataGod.MAXIMUM_NODE_LEVEL); }
    }
    /// <summary>
    /// Returns true if this node has enough points to unlock the next node
    /// </summary>
    public bool Filled
    {
        get { return (NodeLevel >= DataGod.POINTS_REQUIRED_FOR_NEXT_NODE); }
    }

    // Use this for initialization
    void Start()
    {
        input = GameObject.Find("SkillTreeManager").GetComponent<SkillTreeManager>();
        lockedSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        unlockedEffect = transform.FindChild("UnlockedNodeEffect");
        unlockedEffect.Rotate(new Vector3(0, 0, 1), Random.Range(0, 360));
        unlockedEffectSpriteRenderer = unlockedEffect.GetComponent<SpriteRenderer>();
        connectionParticleTimer = new Timer(0.2f);
        if (isStartingNode)
        {
            nodeLevel = 5;
            unlockedEffectAlpha = 1;
            secondStar = (GameObject)Instantiate(unlockedEffect.gameObject, new Vector3(transform.position.x, transform.position.y, -1.9f), Quaternion.identity);
            secondStar.transform.Rotate(new Vector3(0, 0, 1), Random.Range(0, 360));
        }
        else
        {
            unlockedEffectAlpha = 0;
        }
        SetUnlockedEffectColor();
        tooltipStyle = new GUIStyle();
        tooltipStyle.font = font;
        tooltipStyle.fontSize = 20;
        tooltipStyle.normal.textColor = Color.white;
    }
    // Update is called once per frame
    void Update()
    {
        mousePosition = input.MouseWorldPosition;
        position = new Vector2(transform.position.x, transform.position.y);
        mouseOver = MouseOver();
        UpdateBackground();
        UpdateRotationalEffects();

        if (LeftClicked())
        {
            //Check if any adjacent nodes have enough points
            if (PrerequisitesMet())
            {
                NodeLevel++;
            }
        }
        if (RightClicked())
        {
            //Check if PAAAAATH
            if (CanRemove())
            {
                NodeLevel--;
            }
        }
        UpdateConnectionEffect();
    }
    void OnGUI()
    {
        if (mouseOver & !isStartingNode)
        {
            string tooltip;
            if (ID == 3)
            {
                int weaponType = WeaponData.weaponType[WeaponData.weapons[WeaponData.currentTree]];
                tooltip = DataGod.treeToolTipsID3[weaponType];
            }
            else
            {
                tooltip = DataGod.treeToolTips[ID];
            }
            Vector2 stringSize = tooltipStyle.CalcSize(new GUIContent(tooltip));
            int margin = 4;
            int lineWidth = 2;
            int offset = 20;
            Rect tooltipRect = new Rect(Input.mousePosition.x - margin - lineWidth + offset, Screen.height - Input.mousePosition.y - margin - lineWidth + offset, stringSize.x + margin * 2 + lineWidth * 2, stringSize.y + margin * 2 + lineWidth * 2);
            if (tooltipRect.x + tooltipRect.width > Screen.width)
            {
                tooltipRect.x -= (tooltipRect.x + tooltipRect.width) - Screen.width;
            }
            if (tooltipRect.y + tooltipRect.height > Screen.height)
            {
                tooltipRect.y -= (tooltipRect.y + tooltipRect.height) - Screen.height;
            }
            GUI.DrawTexture(tooltipRect, tooltipOutline.texture);
            GUI.DrawTexture(new Rect(tooltipRect.x + lineWidth, tooltipRect.y + lineWidth, tooltipRect.width - lineWidth * 2, tooltipRect.height - lineWidth * 2), tooltipBackground.texture);
            GUI.Label(new Rect(tooltipRect.x + lineWidth + margin, tooltipRect.y + lineWidth + margin, tooltipRect.width - lineWidth * 2, tooltipRect.height - lineWidth * 2), tooltip, tooltipStyle);
        }
    }

    private void SetUnlockedEffectColor()
    {
        unlockedEffectSpriteRenderer.color = Color.white * unlockedEffectAlpha;
    }
    private void UpdateRotationalEffects()
    {
        if (unlockedEffectSpriteRenderer.enabled)
        {
            unlockedEffect.Rotate(new Vector3(0, 0, 1), 60 * Time.deltaTime);
            if (isStartingNode)
            {
                secondStar.transform.Rotate(new Vector3(0, 0, 1), 43 * Time.deltaTime);
            }
        }
    }
    private void UpdateConnectionEffect()
    {
        connectionParticleTimer.Update();
        if (connectionParticleTimer.HasCompleted())
        {
            for (int i = 0; i < adjacentNodes.Count; i++)
            {
                if (NodeLevel > 0 & adjacentNodes[i].GetComponent<Node>().NodeLevel > 0)
                {
                    if (NodeLevel >= DataGod.POINTS_REQUIRED_FOR_NEXT_NODE |
                        adjacentNodes[i].GetComponent<Node>().NodeLevel >= DataGod.POINTS_REQUIRED_FOR_NEXT_NODE)
                    {
                        GameObject newEffect = (GameObject)Instantiate(connectionParticle, transform.position, Quaternion.identity);
                        newEffect.GetComponent<NodeConnectionParticle>().target = adjacentNodes[i];
                    }
                }
            }
        }
    }
    private bool CanRemove()
    {
        if (isStartingNode)
        {
            return false;
        }
        if (NodeLevel <= 0)
        {
            return false;
        }
        if (NodeLevel > DataGod.POINTS_REQUIRED_FOR_NEXT_NODE)
        {
            return true;
        }
        foreach (GameObject node in adjacentNodes)
        {
            if (node.GetComponent<Node>().NodeLevel > 0)
            {
                if (!FindPathToStartingNode(node))
                {
                    return false;
                }
            }
        }
        return true;
    }
    private bool FindPathToStartingNode(GameObject node)
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        List<GameObject> queue = new List<GameObject>();
        GameObject currentNode = node;
        queue.Add(currentNode);
        List<GameObject> closed = new List<GameObject>();
        closed.Add(gameObject);

        while (queue.Count > 0)
        {
            currentNode = queue[0];
            if (currentNode.GetComponent<Node>().isStartingNode & currentNode.GetComponent<Node>().NodeLevel >= DataGod.POINTS_REQUIRED_FOR_NEXT_NODE)
            {
                return true;
            }
            List<GameObject> nextNodes = new List<GameObject>();
            nextNodes.AddRange(currentNode.GetComponent<Node>().adjacentNodes);
            for (int i = 0; i < nextNodes.Count; i++)
            {
                if (nextNodes[i].GetComponent<Node>().NodeLevel < DataGod.POINTS_REQUIRED_FOR_NEXT_NODE)
                {
                    nextNodes.RemoveAt(i);
                    i--;
                }
                else
                {
                    for (int p = 0; p < closed.Count; p++)
                    {
                        if (nextNodes[i] == closed[p])
                        {
                            nextNodes.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
            closed.AddRange(nextNodes);
            queue.AddRange(nextNodes);
            queue.RemoveAt(0);
        }

        return false;
    }
    private bool PrerequisitesMet()
    {
        if (isStartingNode)
        {
            return false;
        }
        foreach (GameObject node in adjacentNodes)
        {
            if (node.GetComponent<Node>().NodeLevel >= DataGod.POINTS_REQUIRED_FOR_NEXT_NODE)
            {
                return true;
            }
        }
        return false;
    }
    private void UpdateBackground()
    {
        if (isStartingNode)
        {
            return;
        }
        bool locked = true;

        if (NodeLevel < DataGod.MAXIMUM_NODE_LEVEL & PrerequisitesMet() & DataGod.talentPoints > 0)
        {
            locked = false;
        }
        if (NodeLevel > 0 & CanRemove())
        {
            locked = false;
        }

        if (locked)
        {
            unlockedEffectEnabled = false;
            if (NodeLevel > 0)
            {
                SetSprite(lockedSpriteWithPoints);
            }
            else
            {
                if (mouseOver)
                {
                    SetSprite(mouseOverLockedSprite);
                }
                else
                {
                    SetSprite(lockedSprite);
                }
            }
        }
        else
        {
            if (NodeLevel < DataGod.MAXIMUM_NODE_LEVEL)
            {
                unlockedEffectEnabled = true;
            }
            else
            {
                unlockedEffectEnabled = false;
            }
            if (mouseOver)
            {
                SetSprite(mouseOverUnlockedSprite);
            }
            else
            {
                SetSprite(unlockedSprite);
            }
        }
        if (unlockedEffectEnabled)
        {
            if (unlockedEffectAlpha < 1)
            {
                unlockedEffectAlpha += Time.deltaTime * 2;
            }
            else
            {
                unlockedEffectAlpha = 1;
            }
        }
        else
        {
            if (unlockedEffectAlpha > 0)
            {
                unlockedEffectAlpha -= Time.deltaTime * 2;
            }
            else
            {
                unlockedEffectAlpha = 0;
            }
        }
        SetUnlockedEffectColor();
    }
    private void SetSprite(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private bool LeftClicked()
    {
        return (mouseOver & Input.GetMouseButtonDown(0));
    }
    private bool RightClicked()
    {
        return (mouseOver & Input.GetMouseButtonDown(1));
    }
    private bool MouseOver()
    {
        return ((mousePosition - position).magnitude <= NODE_COLLISION_RADIUS);
    }
    private void SetText()
    {
        if (NodeLevel == 0)
        {
            GetTextMesh().text = "";
        }
        else
        {
            GetTextMesh().text = NodeLevel + "";
        }
    }
    public TextMesh GetTextMesh()
    {
        return gameObject.GetComponentInChildren<TextMesh>();
    }
    public void Reset()
    {
        if (!isStartingNode)
        {
            NodeLevel = 0;
        }
    }
}
