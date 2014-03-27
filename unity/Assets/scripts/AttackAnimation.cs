﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackAnimation : MonoBehaviour
{
    public float WindowSize;
    public List<string> animationNames;

    float timeElapsedSinceLastAttack = 0;
    float timeElapsed = 0;

    float animationLength;
    string nextAnimation;
    string currentAnimation;

    // Use this for initialization
    void Start()
    {
        nextAnimation = "";
        currentAnimation = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (networkView.isMine)
        {
            if (!animation.isPlaying)
            {
                currentAnimation = "";
                if (nextAnimation != "")
                {
                    currentAnimation = nextAnimation;
                    nextAnimation = "";
                    PlayNext();
                }
            }
            else
            {
                timeElapsedSinceLastAttack += Time.deltaTime;
            }
        }
    }

    private void PlayNext()
    {
        timeElapsedSinceLastAttack = 0;
        animation.Play(currentAnimation);
    }

    private string Anim(PlayerManager player)
    {
        return animationNames[(player.ComboCount % animation.GetClipCount())];
    }

    public void Attack(PlayerManager player)
    {
        if (animation.isPlaying)
        {
            animationLength = animation[currentAnimation].length;
            //if (timeElapsedSinceLastAttack > (animationLength * attackCritWindows[animIndex]) - WindowSize
            //   && timeElapsedSinceLastAttack < (animationLength * attackCritWindows[animIndex]) + WindowSize)
            //{
            if (timeElapsedSinceLastAttack > animationLength - WindowSize & timeElapsedSinceLastAttack < animationLength)
            {
                player.ComboCount++;
                Debug.Log("AttackCrit Combo " + player.ComboCount);
            }
            else
            {
                player.ComboCount = 0;
            }
            nextAnimation = Anim(player);
            timeElapsedSinceLastAttack = 0;
        }
        else
        {
            player.ComboCount = 0;
            currentAnimation = Anim(player);
            PlayNext();
        }
    }
}
