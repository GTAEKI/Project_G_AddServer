using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{
    public GameObject Map { get; private set; }
    public Grid CellGrid { get; private set; }
    
    



    public void Clear() 
    {
        Map = null;
        CellGrid = null;
        //_cells = null;
        _cells.Clear();
    }

    // (CellPos, Creature) 셀 위치에 따른 Creature
    private Dictionary<Vector3Int, Creature> _cells = new Dictionary<Vector3Int, Creature>();

    private int MinX;
    private int MaxX;
    private int MinY;
    private int MaxY;

    // 셀 좌표는 float가아닌 Int로 관리
    public Vector3Int World2Cell(Vector3 worldPos) { return CellGrid.WorldToCell(worldPos); }
    public Vector3 Cell2World(Vector3Int cellPos) { return CellGrid.CellToWorld(cellPos); }

    private Define.ECellCollisionType[,] _collision;

    public void LoadMap(string mapName)
    {
        GameObject map = GameObject.Find(mapName);
        Map = map;
        CellGrid = Util.FindChild<Grid>(map, "Grid", true);

        ParseCollisionData(map, mapName);
    }

    void ParseCollisionData(GameObject map, string mapName, string tilemap = "TileMap_MovableArea")
    {
        GameObject collision = Util.FindChild(map, tilemap, true);
        if (collision != null)
            collision.SetActive(false);

        // 갈 수 있는 지역 읽어오기
        TextAsset txt = Managers.Resource.LoadFromResources<TextAsset>($"Data/{tilemap}Collision");
        StringReader reader = new StringReader(txt.text);

        MinX = int.Parse(reader.ReadLine());
        MaxX = int.Parse(reader.ReadLine());
        MinY = int.Parse(reader.ReadLine());
        MaxY = int.Parse(reader.ReadLine());

        int xCount = MaxX - MinX + 1;
        int yCount = MaxY - MinY + 1;
        _collision = new Define.ECellCollisionType[xCount, yCount];

        for (int y = 0; y < yCount; y++)
        {
            string line = reader.ReadLine();
            for (int x = 0; x < xCount; x++)
            {
                switch (line[x])
                {
                    case Define.MAP_TOOL_WALL:
                        _collision[x, y] = Define.ECellCollisionType.Wall;
                        break;
                    case Define.MAP_TOOL_NONE:
                        _collision[x, y] = Define.ECellCollisionType.None;
                        break;
                    case Define.MAP_TOOL_BUILDING:
                        _collision[x, y] = Define.ECellCollisionType.Building;
                        break;
                    case Define.MAP_TOOL_Enemy:
                        _collision[x, y] = Define.ECellCollisionType.Enemy;
                        break;
                }
                // Debug.Log(_collision[x , y]);
            }
        }
    }

    public bool MoveTo(Creature obj, Vector3Int cellPos, Define.ECreatureType cretureType, bool forceMove = false)
    {
        if (CanGo(cellPos, cretureType) == false) 
        {
            return false;
        }

        // 기존 좌표에 있던 오브젝트를 밀어준다.
        // (단, 처음 신청했으면 해당 CellPos의 오브젝트가 본인이 아닐 수도 있음)
        RemoveObject(obj);

        // 새 좌표에 오브젝트를 등록한다.
        AddObject(obj, cellPos, cretureType);

        // 셀 좌표 이동
        obj.SetCellPos(cellPos, forceMove);

        //Debug.Log($"Move To {cellPos}");

        return true;
    }

    #region Helpers
    public Creature TryGetCreature(Vector3Int cellPos)
    {
        // 없으면 null
        _cells.TryGetValue(cellPos, out Creature value);
        return value;
    }

    public Creature TryGetCreature(Vector3 worldPos)
    {
        Vector3Int cellPos = World2Cell(worldPos);
        return TryGetCreature(cellPos);
    }

    public bool RemoveObject(Creature obj)
    {
        Creature prev = TryGetCreature(obj.CellPos);

        // 해당 Cell위치에 본인이 아닌 obj가 있다면 false
        if (prev != obj)
            return false;

        _cells[obj.CellPos] = null;
        return true;
    }

    // 해당 셀 위치에 자신을 추가 
    public bool AddObject(Creature obj, Vector3Int cellPos, Define.ECreatureType creatureType)
    {
        if (CanGo(cellPos, creatureType) == false)
        {
            Debug.LogWarning($"AddObject Failed");
            return false;
        }

        Creature prev = TryGetCreature(cellPos);
        if (prev != null)
        {
            Debug.LogWarning($"AddObject Failed");
            return false;
        }

        _cells[cellPos] = obj;
        return true;
    }

    public bool CanGo(Vector3 worldPos, Define.ECreatureType creatureType)
    {
        return CanGo(World2Cell(worldPos), creatureType);
    }

    public bool CanGo(Vector3Int cellPos, Define.ECreatureType creatureType)
    {
        if (cellPos.x < MinX || cellPos.x > MaxX)
            return false;
        if (cellPos.y < MinY || cellPos.y > MaxY)
            return false;
        
        Creature obj = TryGetCreature(cellPos);
        if (obj != null)
            return false;
        
        int x = cellPos.x - MinX;
        int y = MaxY - cellPos.y;

        Define.ECellCollisionType type = _collision[x, y];

        switch (creatureType) 
        {
            case Define.ECreatureType.Hero:
                if (type == Define.ECellCollisionType.None || type == Define.ECellCollisionType.Building)
                    return true;
                break;
            case Define.ECreatureType.Enemy:
                if (type == Define.ECellCollisionType.None || type == Define.ECellCollisionType.Enemy)
                    return true;
                break;
        }

        return false;
    }

    public void ClearObjects()
    {
        _cells.Clear();
    }

    #endregion

    #region A* Algorithm
    public struct PQNode : IComparable<PQNode>
    {
        public int H; // 휴리스틱
        public Vector3Int CellPos;
        public int Depth;

        public int CompareTo(PQNode other)
        {
            if (H == other.H)
                return 0;
            return H < other.H ? 1 : -1;
        }
    }

    // cell 이동방향
    List<Vector3Int> _cellDir = new List<Vector3Int>()
    {
        new Vector3Int(0, 1, 0),
		new Vector3Int(1, 1, 0),
		new Vector3Int(1, 0, 0),
		new Vector3Int(1, -1, 0),
		new Vector3Int(0, -1, 0),
		new Vector3Int(-1, -1, 0),
		new Vector3Int(-1, 0, 0),
		new Vector3Int(-1, 1, 0),
	};

    public List<Vector3Int> FindPath(Vector3Int startCellPos, Vector3Int destCellPos, Define.ECreatureType cretureType, int maxDepth = 20)
    {
        Dictionary<Vector3Int, int> best = new Dictionary<Vector3Int, int>();
        Dictionary<Vector3Int, Vector3Int> parent = new Dictionary<Vector3Int, Vector3Int>();

        PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>(); // 후보군

        Vector3Int pos = startCellPos;
        Vector3Int dest = destCellPos;

        // destCellPos에 도착 못하더라도 제일 가까운 애로.
        Vector3Int closestCellPos = startCellPos;
        int closestH = (dest - pos).sqrMagnitude;

        // 첫 노드
        {
            int h = (dest - pos).sqrMagnitude;
            pq.Push(new PQNode() { H = h, CellPos = pos, Depth = 1 });
            parent[pos] = pos;
            best[pos] = h;
        }

        while (pq.Count > 0)
        {
            PQNode node = pq.Pop();
            pos = node.CellPos;

            if (pos == dest)
                break;

            // 무한으로 깊이 들어가진 않음.
            if (node.Depth >= maxDepth)
                break;

            // 이동 가능 여부 확인
            foreach (Vector3Int dir in _cellDir)
            {
                Vector3Int next = pos + dir;

                if (CanGo(next, cretureType) == false)
                    continue;

                int h = (dest - next).sqrMagnitude;

                if (best.ContainsKey(next) == false)
                    best[next] = int.MaxValue;

                if (best[next] <= h)
                    continue;

                best[next] = h;

                pq.Push(new PQNode() { H = h, CellPos = next, Depth = node.Depth + 1 });
                parent[next] = pos;

                if (closestH > h)
                {
                    closestH = h;
                    closestCellPos = next;
                }
            }
        }

        // 목적지에 도달할수 없을경우 가장 나은 경로
        if (parent.ContainsKey(dest) == false)
            return CalcCellPathFromParent(parent, closestCellPos);

        return CalcCellPathFromParent(parent, dest);
    }

    List<Vector3Int> CalcCellPathFromParent(Dictionary<Vector3Int, Vector3Int> parent, Vector3Int dest)
    {
        List<Vector3Int> cells = new List<Vector3Int>();

        if (parent.ContainsKey(dest) == false)
            return cells;

        Vector3Int now = dest;

        while (parent[now] != now)
        {
            cells.Add(now);
            now = parent[now];
        }

        cells.Add(now);
        cells.Reverse();

        return cells;
    }

    #endregion

}
