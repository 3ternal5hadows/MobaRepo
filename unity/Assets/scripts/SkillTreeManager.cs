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

    }

    // Update is called once per frame
    void Update()
    {
        mouseScreenPosition = Input.mousePosition;
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
