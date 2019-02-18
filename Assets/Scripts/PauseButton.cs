using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {
    [SerializeField] CornGrid grid = null;

    public void OnPause(bool val) {
        grid.Paused = val;
    }
}
