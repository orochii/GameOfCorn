using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {
    private bool paused;

    public void TogglePause() {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
    }
}
