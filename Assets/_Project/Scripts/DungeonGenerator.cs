using System.Collections;
using System.Collections.Generic;
using Guymon.Utilities;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    //broken
    private DungeonSpot[,] dungeon;
    private int loopCount = 0;
    [SerializeField][Min(0)] private Vector2Int minRoomSize;
    [SerializeField][Min(0)] private Vector2Int maxRoomSize;
    [SerializeField][Min(0)] private int maxRoomCount;
    [SerializeField][Min(0)] private Vector2Int totalSize;
    [SerializeField][Range(0.0f, 1.0f)] private float coveragePercent;
    [SerializeField] private float maxLoopage;
    private int[] errors = new int[3];
    private class Area
    {
        public Vector2Int roomCenter;
        public Vector2Int roomNegCorner;
        public Vector2Int roomPosCorner;
        public bool room = false;
        public Area(Vector2Int center) {
            roomCenter = center;
            roomNegCorner = center;
            roomPosCorner = center;
        }
        public Vector2Int GetRoomSize() {
            return new Vector2Int(roomPosCorner.x - roomNegCorner.x + 1, roomPosCorner.y - roomNegCorner.y + 1);
        }
    }
    private void Start() {
        //generate();
    }
    [ContextMenu(nameof(generate))]
    private void generate() {
        loopCount = 0;
        errors = new int[3];

        dungeon = new DungeonSpot[totalSize.x, totalSize.y];
        bool complete = false;
        List<Area> areas = new List<Area>();
        int goodRoomCount = 0;
        int totalAreaFilled = 0;
        while(!complete) {
            if(dontBreak("Creation", 0)) return;
            bool validStart = false;
            Vector2Int roomPoint = -Vector2Int.one;
            while(!validStart) {
                if(dontBreak("CenterPoint", 1)) return;
                roomPoint = new Vector2Int(Random.Range(0, totalSize.x - 1), Random.Range(0, totalSize.y - 1));
                if(dungeon[roomPoint.x, roomPoint.y] == 0) {
                    validStart = true;
                }
            }
            Area focusPoint = new Area(roomPoint);
            Vector2Int expandToSize = new Vector2Int(Random.Range(minRoomSize.x, maxRoomSize.x), Random.Range(minRoomSize.y, maxRoomSize.y));
            bool completedArea = false;
            bool[] maxSizeHit = {false, false, false, false};
            while(!completedArea) {
                if(dontBreak("Expanding", 2)) return;
                int[] set = {0, 1, 2, 3};
                int[] sideOrder = randomOrderFromSet(set);
                foreach(int side in sideOrder) {
                    switch(side) {
                        case 0 : {
                            //up
                            if(maxSizeHit[side]) break;
                            bool isSpaceToExpand = true;
                            int y = focusPoint.roomPosCorner.y + 1;
                            for(int x = focusPoint.roomNegCorner.x; x <= focusPoint.roomPosCorner.x; x++) {
                                if(!(inBounds(x, y, dungeon) && dungeon[x, y] <= DungeonSpot.Desolate)) {
                                    Guymon.Utilities.Logger.Info("Cant Expand -Up- due to: " + x + ", " + y);
                                    isSpaceToExpand = false;
                                    break;
                                }
                            }
                            if(isSpaceToExpand && focusPoint.GetRoomSize().y < maxRoomSize.y && focusPoint.GetRoomSize().y < expandToSize.y) {
                                //expand
                                //Debug.Log("Expand Up");
                                focusPoint.roomPosCorner.y++;
                                totalAreaFilled += focusPoint.GetRoomSize().x;
                            }
                            else {
                                maxSizeHit[side] = true;
                            }
                            break;
                        }
                        case 1 : {
                            //right
                            if(maxSizeHit[side]) break;
                            bool isSpaceToExpand = true;
                            int x = focusPoint.roomPosCorner.x + 1;
                            for(int y = focusPoint.roomNegCorner.y; y <= focusPoint.roomPosCorner.y; y++) {
                                if(!(inBounds(x, y, dungeon) && dungeon[x, y] <= DungeonSpot.Desolate)) {
                                    Guymon.Utilities.Logger.Info("Cant Expand -Right- due to: " + x + ", " + y);
                                    isSpaceToExpand = false;
                                    break;
                                }
                            }
                            if(isSpaceToExpand && focusPoint.GetRoomSize().x < maxRoomSize.x && focusPoint.GetRoomSize().x < expandToSize.x) {
                                //expand
                                //Debug.Log("Expand Right");
                                focusPoint.roomPosCorner.x++;
                                totalAreaFilled += focusPoint.GetRoomSize().y;
                            }
                            else {
                                maxSizeHit[side] = true;
                            }
                            break;
                        }
                        case 2 : {
                            //down
                            if(maxSizeHit[side]) break;
                            bool isSpaceToExpand = true;
                            int y = focusPoint.roomNegCorner.y - 1;
                            for(int x = focusPoint.roomNegCorner.x; x <= focusPoint.roomPosCorner.x; x++) {
                                if(!(inBounds(x, y, dungeon) && dungeon[x, y] <= DungeonSpot.Desolate)) {
                                    Guymon.Utilities.Logger.Info("Cant Expand -Down- due to: " + x + ", " + y);
                                    isSpaceToExpand = false;
                                    break;
                                }
                            }
                            if(isSpaceToExpand && focusPoint.GetRoomSize().y < maxRoomSize.y && focusPoint.GetRoomSize().y < expandToSize.y) {
                                //expand
                                //Debug.Log("Expand Down");
                                focusPoint.roomNegCorner.y--;
                                totalAreaFilled += focusPoint.GetRoomSize().x;
                            }
                            else {
                                maxSizeHit[side] = true;
                            }
                            break;
                        }
                        case 3 : {
                            //left
                            if(maxSizeHit[side]) break;
                            bool isSpaceToExpand = true;
                            int x = focusPoint.roomNegCorner.x - 1;
                            for(int y = focusPoint.roomNegCorner.y; y <= focusPoint.roomPosCorner.y; y++) {
                                if(!(inBounds(x, y, dungeon) && dungeon[x, y] <= DungeonSpot.Desolate)) {
                                    Guymon.Utilities.Logger.Info("Cant Expand -Left- due to: " + x + ", " + y);
                                    isSpaceToExpand = false;
                                    break;
                                }
                            }
                            if(isSpaceToExpand && focusPoint.GetRoomSize().x < maxRoomSize.x && focusPoint.GetRoomSize().x < expandToSize.x) {
                                //expand
                                //Debug.Log("Expand Left");
                                focusPoint.roomNegCorner.x--;
                                totalAreaFilled += focusPoint.GetRoomSize().y;
                            }
                            else {
                                maxSizeHit[side] = true;
                            }
                            break;
                        }
                    }
                    if(focusPoint.GetRoomSize().Equals(expandToSize)) {
                        //good
                        Guymon.Utilities.Logger.Info("Created a good room of size: " + focusPoint.GetRoomSize() + " Starting at: " + focusPoint.roomCenter);
                        Guymon.Utilities.Logger.Info("Details: Center: " + focusPoint.GetRoomSize() + " NegCorner: " + focusPoint.roomNegCorner + " PosCorner: " + focusPoint.roomPosCorner);
                        focusPoint.room = true;
                        drawInArea(focusPoint, dungeon);
                        completedArea = true;
                        areas.Add(focusPoint);
                        goodRoomCount++;
                        break;
                    }
                    if(!completedArea && maxSizeHit[0] && maxSizeHit[1] && maxSizeHit[2] && maxSizeHit[3]) {
                        if(focusPoint.GetRoomSize().x >= minRoomSize.x && focusPoint.GetRoomSize().y >= minRoomSize.y) {
                            //fine
                            Guymon.Utilities.Logger.Info("Created a fine room of size: " + focusPoint.GetRoomSize() + ", compared to: " + expandToSize + " Starting at: " + focusPoint.roomCenter);
                            Guymon.Utilities.Logger.Info("Details: Center: " + focusPoint.GetRoomSize() + " NegCorner: " + focusPoint.roomNegCorner + " PosCorner: " + focusPoint.roomPosCorner);
                            focusPoint.room = true;
                            drawInArea(focusPoint, dungeon);
                            completedArea = true;
                            areas.Add(focusPoint);
                            goodRoomCount++;
                        }
                        else {
                            //bad
                            Guymon.Utilities.Logger.Info("Created a bad room of size: " + focusPoint.GetRoomSize() + ", trying to reach: " + expandToSize + " Starting at: " + focusPoint.roomCenter);
                            Guymon.Utilities.Logger.Info("Details: Center: " + focusPoint.GetRoomSize() + " NegCorner: " + focusPoint.roomNegCorner + " PosCorner: " + focusPoint.roomPosCorner);
                            drawInArea(focusPoint, dungeon);
                            completedArea = true;
                            areas.Add(focusPoint);
                        }
                        break;
                    }
                }
            }
            complete = (totalAreaFilled >= dungeon.GetLength(0) * dungeon.GetLength(1) * coveragePercent) || goodRoomCount == maxRoomCount;
        }
        Guymon.Utilities.Logger.Info("Completed in " + loopCount + " loops");
        ConsoleDisplay(dungeon);
    }
    private void drawInArea(Area area, DungeonSpot[,] canvas) {
        for(int x = area.roomNegCorner.x; x <= area.roomPosCorner.x; x++) {
            for (int y = area.roomNegCorner.y; y <= area.roomPosCorner.y; y++) {
                //if(inBounds(x,y, canvas)) {
                    if(x == area.roomNegCorner.x || x == area.roomPosCorner.x || y == area.roomNegCorner.y || y == area.roomPosCorner.y) canvas[x,y] = (area.room ? DungeonSpot.Wall : DungeonSpot.Desolate);
                    else canvas[x,y] = (area.room ? DungeonSpot.Floor : DungeonSpot.Desolate);
                //}
            }
        }
    }
    private bool inBounds(int x, int y, DungeonSpot[,] size) {
        return (x >= 0 && x < size.GetLength(0) && y >= 0 && y < size.GetLength(1));
    }
    private int[] randomOrderFromSet(int[] set) {
        for(int i = 0; i < set.Length; i++) {
            int ran = Random.Range(0, set.Length - 1);
            int temp = set[i];
            set[i] = set[ran];
            set[ran] = temp;
        }
        return set;
    }
    private bool dontBreak(string message, int index) {
        loopCount++;
        errors[index]++;
        if(loopCount >= maxLoopage) {
            Guymon.Utilities.Logger.Warning("Broke by: " + message + " with " + errors[index]);
            return true;
        }
        return false;
    }
    
    public enum DungeonSpot
    {
        Empty = 0,
        Desolate = 1,
        Wall = 2,
        Floor = 3,
        Hall = 4
    }



    private void ConsoleDisplay(DungeonSpot[,] board) {
        string printer = "";
        for(int y = board.GetLength(1) - 1; y >= 0; y--) {
            for (int x = 0; x < board.GetLength(0); x++) {
                //printer += "(" + x + "," + y + ")";
                switch(board[x,y]) {
                    case DungeonSpot.Wall : {
                        printer += "#";
                        break;
                    }
                    case DungeonSpot.Floor : {
                        printer += "_";
                        break;
                    }
                    case DungeonSpot.Hall : {
                        printer += "~";
                        break;
                    }
                    case DungeonSpot.Empty : {
                        printer += " ";
                        break;
                    }
                    case DungeonSpot.Desolate : {
                        printer += "/";
                        break;
                    }
                    default : {
                        printer += "?";
                        break;
                    }
                }
            }
            printer += "\n";
        }
        print(printer);
    }
    private void LogDisplay(DungeonSpot[,] board) {
        string printer = "";
        for(int x = 0; x < board.GetLength(0); x++) {
            for (int y = 0; y < board.GetLength(1); y++) {
                printer += board[x,y];
            }
            printer += "\n";
        }
        print(printer);
    }
}
