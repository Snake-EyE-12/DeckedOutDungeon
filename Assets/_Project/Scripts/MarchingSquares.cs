using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingSquares : MonoBehaviour
{
    public bool drawExtras = true;
    public bool lines = true;
    [SerializeField][Min(0)] private float resolution;
    [SerializeField][Min(0)] private Vector2 size;
    [SerializeField][Range(0, 10)] private float scale;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject linePrefab;


    private float[,] squares;


    private void Start() {
        generate();
    }
    private void generate() {
        squares = new float[(int)(size.x * resolution) + 1, (int)(size.y * resolution) + 1];
        for(int x = 0; x < squares.GetLength(0); x++) {
            for(int y = 0; y < squares.GetLength(1); y++) {
                float randomValue = Random.Range(-1f, 1.0f);
                squares[x,y] = randomValue;
                if(drawExtras) {
                    GameObject newPoint = Instantiate(point, new Vector3(x * scale / resolution, y * scale / resolution), Quaternion.identity);
                    float collapsedValue = (randomValue + 1) * 0.5f;
                    newPoint.name = "" + collapsedValue;
                    newPoint.GetComponent<SpriteRenderer>().color = new Color(collapsedValue, collapsedValue, collapsedValue);
                }
            }
        }
        for(int x = 0; x < squares.GetLength(0) - 1; x++) {
            for(int y = 0; y < squares.GetLength(1) - 1; y++) {
                int A = (int)Mathf.Clamp((squares[x,y] + 1), 0, 1);
                int B = (int)Mathf.Clamp((squares[x+1,y] + 1), 0, 1);
                int C = (int)Mathf.Clamp((squares[x+1,y+1] + 1), 0, 1);
                int D = (int)Mathf.Clamp((squares[x,y+1] + 1), 0, 1);
                int cellCase = (toCase(A, B, C, D));
                Vector2 bottom = new Vector2(x + Mathf.Abs((squares[x,y] + (squares[x+1,y]) /2)), y);
                Vector2 right = new Vector2(x + 1, y + Mathf.Abs((squares[x+1,y] + (squares[x+1,y+1]) /2)));
                Vector2 top = new Vector2(x + Mathf.Abs((squares[x,y+1] + (squares[x+1,y+1]) /2)), y + 1);
                Vector2 left = new Vector2(x, y + Mathf.Abs((squares[x,y] + (squares[x,y+1]) /2)));
                if(cellCase == 1 || cellCase == 10 || cellCase == 14) draw(bottom, left, cellCase);
                if(cellCase == 2 || cellCase == 5 || cellCase == 13) draw(right, bottom, cellCase);
                if(cellCase == 3 || cellCase == 12) draw(right, left, cellCase);
                if(cellCase == 4 || cellCase == 10 || cellCase == 11) draw(top, right, cellCase);
                if(cellCase == 6 || cellCase == 9) draw(top, bottom, cellCase);
                if(cellCase == 5 || cellCase == 7 || cellCase == 8) draw(left, top, cellCase);
            }
        }
    }
    private int toCase(int b1, int b2, int b4, int b8) {
        return b1 + (b2 * 2) + (b4 * 4) + (b8 * 8);
    }
    private void draw(Vector2 from, Vector2 to, int value) {
        //Debug.Log("From: " + from + " To: " + to + " Case: " + value);

        if(lines) {
            GameObject line = Instantiate(linePrefab);
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            Vector3[] positions = new Vector3[2];
            positions[0] = from;
            positions[1] = to;
            lineRenderer.SetPositions(positions);
        }
        else {
            GameObject wall = Instantiate(wallPrefab);
            wall.transform.position = Vector2.Lerp(from * scale / resolution, to * scale / resolution, 0.5f);
            wall.transform.localScale = new Vector3(0.1f / resolution, Vector2.Distance(from * scale / resolution, to * scale / resolution), 1);
            Vector2 directionalVector = to - from;
            wall.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(directionalVector.y, directionalVector.x) + 90);
        }
    }
    
}
