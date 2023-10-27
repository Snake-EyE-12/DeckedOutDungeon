using System.Collections;
using System.Collections.Generic;
using Guymon.Utilities;
using UnityEngine;

public class MarchingSquares2 : MonoBehaviour
{
    [SerializeField][Min(0)][Tooltip("points per square")] private float resolution;
    [SerializeField][Min(0)] private Vector2 gridSize;
    [SerializeField][Range(0, 10)] private float gridScale;
    [SerializeField][Min(0)] private float wallWidthScale;
    [SerializeField] private Wall wallPrefab;
    private List<List<Line>> lineGroups = new List<List<Line>>();

    private Cell[,] squares;
    private class Cell
    {
        /*
        +----+
        |    |
        O----+
        */
        public float value = 0;
        public int diagram = -1;
        public Cell(float randomValue) {
            value = randomValue;
            if(IntValue() > 1) Guymon.Utilities.Logger.Warning("Need To Clamp Random Value For Marching Square Cells!!!!");
        }
        public int IntValue() {
            return (int)(value + 1);
        }
        public float UnscaledMiddleDistance(Cell other) {
            return 0.5f + ((value + other.value) * 0.5f);
        }
    }
    private class Line
    {
        public Vector2[] points = new Vector2[2];
        public Wall wall;
        public Line(Vector2 p1, Vector2 p2, Wall wall) {
            points[0] = p1;
            points[1] = p2;
            this.wall = wall;
        }
        public bool checkPoints(Vector2 p1, Vector2 p2) {
            return ((points[0] == p1) || (points[0] == p2) || (points[1] == p1) || (points[1] == p2));
        }
        public float GetLength() {
            return Vector2.Distance(points[0], points[1]);
        }
    }

    private void Start() {
        generate();
    }
    /*
    */
    private void generate() {
        squares = new Cell[(int)(gridSize.x * resolution) + 1, (int)(gridSize.y * resolution) + 1];
        for(int x = 0; x < squares.GetLength(0); x++) {
            for(int y = 0; y < squares.GetLength(1); y++) {
                squares[x,y] = new Cell(Random.Range(-1f, 1.0f));
            }
        }
        for(int x = 0; x < squares.GetLength(0) - 1; x++) {
            for(int y = 0; y < squares.GetLength(1) - 1; y++) {
                squares[x,y].diagram = toCase(squares[x,y].IntValue(), squares[x+1,y].IntValue(), squares[x+1,y+1].IntValue(), squares[x,y+1].IntValue());
                
                Vector2 bottom   = new Vector2(x + squares[x,y].UnscaledMiddleDistance(squares[x+1,y]),     y);
                Vector2 right    = new Vector2(x + 1,                                                       y + squares[x+1,y].UnscaledMiddleDistance(squares[x+1,y+1]));
                Vector2 top      = new Vector2(x + squares[x,y+1].UnscaledMiddleDistance(squares[x+1,y+1]), y + 1);
                Vector2 left     = new Vector2(x,                                                           y + squares[x,y].UnscaledMiddleDistance(squares[x,y+1]));

                if(squares[x,y].diagram == 1 || squares[x,y].diagram == 10 || squares[x,y].diagram == 14) draw(bottom, left);
                if(squares[x,y].diagram == 2 || squares[x,y].diagram == 5 || squares[x,y].diagram == 13) draw(right, bottom);
                if(squares[x,y].diagram == 3 || squares[x,y].diagram == 12) draw(right, left);
                if(squares[x,y].diagram == 4 || squares[x,y].diagram == 10 || squares[x,y].diagram == 11) draw(top, right);
                if(squares[x,y].diagram == 6 || squares[x,y].diagram == 9) draw(top, bottom);
                if(squares[x,y].diagram == 5 || squares[x,y].diagram == 7 || squares[x,y].diagram == 8) draw(left, top);
            }
        }
        // Wall Initialization
        foreach(List<Line> group in lineGroups) {
            float totalLength = 0;
            foreach(Line line in group) {
                totalLength += line.GetLength();
            }
            foreach(Line line in group) {
                //line.wall.Initialize(group.Count, totalLength, line.points);
            }
        }
    }
    private int toCase(int b1, int b2, int b4, int b8) {
        return b1 + (b2 * 2) + (b4 * 4) + (b8 * 8);
    }
    private void draw(Vector2 from, Vector2 to) {
            GameObject wall = Instantiate(wallPrefab.gameObject, Vector2.Lerp(from * gridScale / resolution, to * gridScale / resolution, 0.5f), Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2((to - from).y, (to - from).x) - 90), transform);
            //wall.transform.position = Vector2.Lerp(from * gridScale / resolution, to * gridScale / resolution, 0.5f);
            wall.transform.localScale = new Vector3(wallWidthScale / resolution, Vector2.Distance(from * gridScale / resolution, to * gridScale / resolution), 1);
            //wall.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2((to - from).y, (to - from).x) - 90);
            groupNewLine(from, to, wall.GetComponent<Wall>());
            wall.name = from + " - " + to;
    }
    private void groupNewLine(Vector2 p1, Vector2 p2, Wall wall) {
        int connectionCount = 0;
        int[] groupJoinIndex = {-1, -1};
        for(int group = 0; group < lineGroups.Count; group++) {
            for(int line = 0; line < lineGroups[group].Count; line++) {
                if(lineGroups[group][line].checkPoints(p1, p2)) {
                    groupJoinIndex[connectionCount] = group;
                    connectionCount++;
                    if(connectionCount > 1) break;
                }
            }
        }
        switch(connectionCount) {
            case 0 : {
                // No Connections = Add a New Group
                List<Line> newGroup = new List<Line>();
                newGroup.Add(new Line(p1, p2, wall));
                lineGroups.Add(newGroup);
                return;
            }
            case 1 : {
                // One Connection = Add To a Existing Group
                lineGroups[groupJoinIndex[0]].Add(new Line(p1, p2, wall));
                return;
            }
            case 2 : {
                // Two Connections = Join Groups If Differnt & Add To the New Group
                if(groupJoinIndex[0] == groupJoinIndex[1]) {
                    lineGroups[groupJoinIndex[0]].Add(new Line(p1, p2, wall));
                    return;
                }
                else {
                    lineGroups[groupJoinIndex[0]].AddRange(lineGroups[groupJoinIndex[1]]);
                    lineGroups[groupJoinIndex[0]].Add(new Line(p1, p2, wall));
                    lineGroups.RemoveAt(groupJoinIndex[1]);
                    return;
                }
            }
        }


        // foreach(List<Line> group in lineGroups) {
        //     bool foundInGroup = false;
        //     foreach(Line line in group) {
        //         if(line.checkPoints(p1, p2)) {
        //             foundInGroup = true;
        //             break;
        //         }
        //     }
        //     if(foundInGroup) {
        //         group.Add(new Line(p1, p2, wallSprite));
        //         return;
        //     }
        // }
        // List<Line> newGroup = new List<Line>();
        // newGroup.Add(new Line(p1, p2, wallSprite));
        // lineGroups.Add(newGroup);
    }
}