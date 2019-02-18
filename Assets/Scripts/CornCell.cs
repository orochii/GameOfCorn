using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornCell : MonoBehaviour {
    [SerializeField] float distance;
    [SerializeField] GameObject graphic;
    internal bool state;
    internal bool cachedState;
    private float timer;
    
    public int X;
    public int Y;
    private CornGrid grid;

    void Awake() {
        SetState(cachedState);
    }

    public void SetupPosition(int x, int y, CornGrid _grid) {
        X = x;
        Y = y;
        grid = _grid;
    }

    void Update() {
        if (timer > Time.time) return;
        // Check
        int ss = GetSurroundState();
        switch (ss) {
            case 0:
            case 1:
                cachedState = false;
                break;
            case 2:
                break;
            case 3:
                cachedState = true;
                break;
            default:
                cachedState = false;
                break;
        }
        timer = Time.time + 0.05f;
    }

    private void LateUpdate() {
        SetState(cachedState);
    }

    private int GetSurroundState() {
        int s = 0;
        if (GetState( 1,-1)) s++;
        if (GetState( 1, 0)) s++;
        if (GetState( 1, 1)) s++;
        if (GetState(-1,-1)) s++;
        if (GetState(-1, 0)) s++;
        if (GetState(-1, 1)) s++;
        if (GetState( 0,-1)) s++;
        if (GetState( 0, 1)) s++;
        return s;
    }

    private bool GetState(int h, int v) {
        int xx = X + h;
        int yy = Y + v;
        if (grid.xLoop) {
            xx = (xx + grid.cellWidth) % grid.cellWidth;
        }
        if (grid.yLoop) {
            yy = (yy + grid.cellHeight) % grid.cellHeight;
        }
        if (grid.Valid(xx, yy)) {
            if (grid.CornCells[xx, yy] != null) {
                return grid.CornCells[xx, yy].state;
            }
        }
        return false;
    }

    private void SetState(bool s) {
        state = s;
        cachedState = s;
        graphic.SetActive(s);
    }
    
    private void OnMouseDown() {
        SetState(!state);
    }
    
}
