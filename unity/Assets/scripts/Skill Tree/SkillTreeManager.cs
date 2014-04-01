using UnityEngine;
using System.Collections;

public class SkillTreeManager : MonoBehaviour
{
    public Vector2 MouseWorldPosition
    {
        get
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            return new Vector2(mousePosition.x, mousePosition.y);
        }
    }
    private Vector3 mouseScreenPosition;

    // Use this for initialization
    void Start()
    {
        DataGod.talentPoints = DataGod.MAXTALENTPOINTS;
        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Node");
        int i = 0;
        foreach (GameObject obj in allNodes)
        {
            if (!obj.GetComponent<Node>().isStartingNode)
            {
                obj.GetComponent<Node>().ID = i;
                obj.GetComponent<Node>().NodeLevel = WeaponData.treeData[WeaponData.currentTree, i];
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        mouseScreenPosition = Input.mousePosition;
        GameObject.Find("TotalPointsText").GetComponent<TextMesh>().text = DataGod.talentPoints + "";

        if (Input.GetMouseButtonDown(0))
        {
            GameObject resetButton = GameObject.Find("ResetButtonBackground");
            if (resetButton.GetComponent<SkillTreeButton>().MouseHit(MouseWorldPosition))
            {
                ResetNodes();
            }
            GameObject mainMenuButton = GameObject.Find("WeaponSelectButtonBackground");
            if (mainMenuButton.GetComponent<SkillTreeButton>().MouseHit(MouseWorldPosition))
            {
                Application.LoadLevel("WeaponSelect");
            }
        }
    }

    public void ResetNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].GetComponent<Node>().Reset();
        }
    }
}
