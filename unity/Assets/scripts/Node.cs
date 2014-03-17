using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
    public List<GameObject> adjacentNodes;
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
                        DataGod.talentPoints-=difference;
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
            SetText();
        }
    }
    private SkillTreeManager input;
    private bool mouseOver;
    private Vector2 position;
    private Vector2 mousePosition;
    private Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Sprite mouseOverLockedSprite;
    public Sprite mouseOverUnlockedSprite;

    private const float NODE_COLLISION_RADIUS = 0.25f;

    /// <summary>
    /// Returns true if the node is one of the starting nodes
    /// </summary>
    public bool IsStartingNode
    {
        get { return adjacentNodes.Count <= 2; }
    }
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
	void Start () {
        NodeLevel = 0;
        SetText();
        input = GameObject.Find("SkillTreeManager").GetComponent<SkillTreeManager>();
        lockedSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	// Update is called once per frame
	void Update () {
        mousePosition = input.MouseWorldPosition;
        position = new Vector2(transform.position.x, transform.position.y);
        mouseOver = MouseOver();
        UpdateBackground();

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
	}

    private bool CanRemove()
    {
        if (NodeLevel <= 0)
        {
            return false;
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
            if (currentNode.GetComponent<Node>().IsStartingNode & currentNode.GetComponent<Node>().NodeLevel >= DataGod.POINTS_REQUIRED_FOR_NEXT_NODE)
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
        if (IsStartingNode)
        {
            return true;
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
        bool shine = false;

        if (NodeLevel < DataGod.MAXIMUM_NODE_LEVEL & PrerequisitesMet() & DataGod.talentPoints > 0)
        {
            shine = true;
        }
        if (NodeLevel > 0 & CanRemove())
        {
            shine = true;
        }

        if (mouseOver)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = (shine) ? mouseOverUnlockedSprite : mouseOverLockedSprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = (shine) ? unlockedSprite : lockedSprite;
        }
    }
    private bool LeftClicked() {
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
        NodeLevel = 0;
    }
}
