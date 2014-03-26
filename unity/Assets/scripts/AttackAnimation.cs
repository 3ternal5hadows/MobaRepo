using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackAnimation : MonoBehaviour
{
    public List<float> attackCritWindows;

    public float WindowSize;

    float timeElapsedSinceLastAttack = 0;
    float timeElapsed = 0;

    int counter = 0;
    int Counter
    {
        get { return counter; }
        set
        {
            counter = value;
            if (counter >= attackCritWindows.Count)
            {
                counter = 0;
            }
        }
    }
    int combo = 0;
    bool animationPlay;
    float animationLength;
    string nextAnimation;
    string currentAnimation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (networkView.isMine)
        {
            if (combo > 0)
            {
                timeElapsedSinceLastAttack += Time.deltaTime / 2f;
            }
        }
    }

    public void Attack()
    {
        animationLength = animation["newSword" + (Counter + 1)].length;

        if (combo > 0)
        {
            if (timeElapsedSinceLastAttack > (animationLength * attackCritWindows[Counter]) - WindowSize
               && timeElapsedSinceLastAttack < (animationLength * attackCritWindows[Counter]) + WindowSize)
            {
                combo++;
                Debug.Log("AttackCrit Combo " + combo);
                Counter++;
            }
            else
            {
                combo = 0;
                Counter = 0;
            }
            animation.PlayQueued("newSword" + (Counter + 1));

            timeElapsedSinceLastAttack = 0;
        }
        else
        {
            animation.PlayQueued("newSword" + (Counter + 1));
            Counter++;
            combo++;
            timeElapsedSinceLastAttack = 0;
        }
    }
}
