using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CornGrid : MonoBehaviour {
    [SerializeField] Transform cornContainer;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] CornCell cornCellPrefab;
    [SerializeField] public int cellWidth = 17;
    [SerializeField] public int cellHeight = 32;
    [SerializeField] float distance = 2f;
    [SerializeField] float xOffset = 0.01f;
    [SerializeField] float angleOffset = 11.25f/2;

    public bool xLoop = true;
    public bool yLoop = true;
    public CornCell[,] CornCells;
    private float cellDistX = 0.2354f;
    private float cellDistY = 11.25f;

    public bool Paused;
    public float GenerationTime = 0.05f;

    void Start() {
        CornCells = new CornCell[cellWidth, cellHeight];
        CreateCorn();
    }

    private void LateUpdate() {
        cornContainer.rotation = Quaternion.Euler(_dragCurrent, 0, 0);
    }

    private void CreateObjectAt(int x, int y, Vector3 position, Quaternion rotation) {
        CornCell o = Instantiate<CornCell>(cornCellPrefab, meshFilter.transform);
        o.transform.localPosition = position;
        o.transform.localRotation = rotation;
        o.SetupPosition(x, y, this);
        CornCells[x, y] = o;
    }

    internal bool Valid(int x, int y) {
        return (x >= 0 && x < CornCells.GetLength(0) && y >= 0 && y < CornCells.GetLength(1));
    }

    void CreateCorn() {
        for (int x = 0; x < cellWidth; x++) {
            Vector3 positionX = new Vector3(distance, 0, (x-8) * cellDistX + xOffset);
            for (int y = 0; y < cellHeight; y++) {
                Quaternion rotation = Quaternion.Euler(0, 0, y * cellDistY + angleOffset);
                Vector3 position = rotation * positionX;
                CreateObjectAt(x, y, position, rotation);
            }
        }
    }
    
    Vector3 _dragPosition;
    float _dragStartAngle = 0;
    float _dragCurrent = 0;

    void OnMouseDown() {
        _dragPosition = Input.mousePosition;
        _dragStartAngle = _dragCurrent;
    }
    
    void OnMouseDrag() {
        Vector3 diff = Input.mousePosition - _dragPosition;
        float angleChange = (diff.y * 180) / Screen.height;
        _dragCurrent = _dragStartAngle + angleChange;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}