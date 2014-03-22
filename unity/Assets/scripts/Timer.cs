using UnityEngine;
using System.Collections;

public class Timer {
    private float time;
    private float maxTime;
    private bool paused;

    public Timer(float maxTime) {
        this.maxTime = maxTime;
        time = 0;
        paused = false;
    }

    public void Pause() {
        paused = true;
    }
    public void Resume() {
        paused = false;
    }

    public void Reset() {
        time = 0;
        paused = false;
    }

    public bool HasCompleted() {
        if (time >= maxTime) {
            Reset();
            return true;
        }
        return false;
    }
    public float GetPercentComplete()
    {
        return time / maxTime;
    }

    public void Update() {
        if (!paused) {
            time += Time.deltaTime;
        }
    }
}
