using System.Collections;
using System.Collections.Generic;
using Guymon.Utilities;
using UnityEngine;

public class DungeonGenerationManager : MonoBehaviour
{
    [Header("Basic Outline")]
    [SerializeField][Min(0)][Tooltip("points per square")] private float resolution;
    [SerializeField][Min(0)] private Vector2 gridSize;
    [SerializeField][Min(0)] private float gridScale;
    private List<List<Line>> lineGroups = new List<List<Line>>();
    [Header("Wall Information")]
    [SerializeField] private Wall wallPrefab;
    [SerializeField] private GameObject pillarPrefab;
    [SerializeField][Min(0)] private float wallWidthScale;
    [SerializeField][Min(0)] private float pillarScale;
    [SerializeField][Min(0)] private float minimumWallLengthForFrame;
    //[SerializeField][Min(0)] private float minimumAreaForEntry; - Never Figured Out Area
    [SerializeField][Min(0)] private int openLoopsAttempts;

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
            if(IntValue() > 1) Guymon.Utilities.Console.Warning("Need To Clamp Random Value For Marching Square Cells!!!!");
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
        public bool canBeFrame;
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
        initializeGrid();
        programWallPositions();
        initializeWalls();
    }
    private void initializeGrid() {
        squares = new Cell[(int)(gridSize.x * resolution) + 1, (int)(gridSize.y * resolution) + 1];
        for(int x = 0; x < squares.GetLength(0); x++) {
            for(int y = 0; y < squares.GetLength(1); y++) {
                squares[x,y] = new Cell(Random.Range(-1f, 1.0f));
            }
        }
    }
    private void programWallPositions() {
        for(int x = 0; x < squares.GetLength(0) - 1; x++) {
            for(int y = 0; y < squares.GetLength(1) - 1; y++) {
                squares[x,y].diagram = toCase(squares[x,y].IntValue(), squares[x+1,y].IntValue(), squares[x+1,y+1].IntValue(), squares[x,y+1].IntValue());
                
                Vector2 bottom   = new Vector2(x + squares[x,y].UnscaledMiddleDistance(squares[x+1,y]),     y);
                Vector2 right    = new Vector2(x + 1,                                                       y + squares[x+1,y].UnscaledMiddleDistance(squares[x+1,y+1]));
                Vector2 top      = new Vector2(x + squares[x,y+1].UnscaledMiddleDistance(squares[x+1,y+1]), y + 1);
                Vector2 left     = new Vector2(x,                                                           y + squares[x,y].UnscaledMiddleDistance(squares[x,y+1]));

                if(squares[x,y].diagram == 1 || squares[x,y].diagram == 10 || squares[x,y].diagram == 14) positionWall(bottom, left);
                if(squares[x,y].diagram == 2 || squares[x,y].diagram == 5 || squares[x,y].diagram == 13) positionWall(right, bottom);
                if(squares[x,y].diagram == 3 || squares[x,y].diagram == 12) positionWall(right, left);
                if(squares[x,y].diagram == 4 || squares[x,y].diagram == 10 || squares[x,y].diagram == 11) positionWall(top, right);
                if(squares[x,y].diagram == 6 || squares[x,y].diagram == 9) positionWall(top, bottom);
                if(squares[x,y].diagram == 5 || squares[x,y].diagram == 7 || squares[x,y].diagram == 8) positionWall(left, top);
            }
        }
    }
    private void initializeWalls() {
        HashSet<Vector2> placedPillarPoints = new HashSet<Vector2>();
        foreach(List<Line> group in lineGroups) {
            for(int a = 0; a < openLoopsAttempts; a++)   {
                group[Random.Range(0, group.Count)].canBeFrame = true;
            }
            foreach(Line line in group) {
                bool isFrame = line.GetLength() >= minimumWallLengthForFrame && line.canBeFrame;
                line.wall.Initialize(isFrame);
                if(!placedPillarPoints.Contains(line.points[0])) {
                    GameObject pillar = Instantiate(pillarPrefab, line.points[0] * gridScale / resolution, Quaternion.identity);
                    pillar.transform.localScale = new Vector3(wallWidthScale / resolution * pillarScale, wallWidthScale / resolution * pillarScale, 1);
                    placedPillarPoints.Add(line.points[0]);
                }
                if(!placedPillarPoints.Contains(line.points[1])) {
                    GameObject pillar = Instantiate(pillarPrefab, line.points[1] * gridScale / resolution, Quaternion.identity);
                    pillar.transform.localScale = new Vector3(wallWidthScale / resolution * pillarScale, wallWidthScale / resolution * pillarScale, 1);
                    placedPillarPoints.Add(line.points[1]);
                }
            }
        }
    }
    // private void calculateGeometry(List<Line> lines, out float perimeter, out float area) {
    //     perimeter = lines[lines.Count - 1].GetLength();
    //     area = 0;
    //     for(int n = 0; n < lines.Count - 1; n++) {
    //         perimeter += lines[n].GetLength();
    //         area += (lines[n].points[0].x * lines[n+1].points)
    //     }
    //     area = Mathf.Abs(area) * 0.5f;
    // }
    // private void sortByPoints(List<Line> lines) {

    // }
    private int toCase(int b1, int b2, int b4, int b8) {
        return b1 + (b2 * 2) + (b4 * 4) + (b8 * 8);
    }
    private void positionWall(Vector2 from, Vector2 to) {
            GameObject wall = Instantiate(wallPrefab.gameObject, Vector2.Lerp(from * gridScale / resolution, to * gridScale / resolution, 0.5f), Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2((to - from).y, (to - from).x) - 90), transform);
            wall.transform.localScale = new Vector3(wallWidthScale / resolution, Vector2.Distance(from * gridScale / resolution, to * gridScale / resolution), 1);
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