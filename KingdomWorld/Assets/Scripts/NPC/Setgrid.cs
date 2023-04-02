using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setgrid : NPCParameter
{
    // 그리드 크기
    private int _gridWidth;
    private int _gridHeight;

    // 그리드 노드들
    //월드 그리드를 위한 노드 배열
    private Node[,] _grid;

    // 다른 오브젝트를 위한 레이어
    /*public LayerMask streetLayer;
    public LayerMask walkableLayer;
    public LayerMask stoneLayer;
    public LayerMask treeLayer;*/

    public class Node
    {
        public int X { get; set; }
        public int Z { get; set; }
        public float Weight { get; set; }
        public Vector3 WorldPosition { get; set; }
        public float GCost { get; set; }
        public float HCost { get; set; }
        public float FCost { get { return GCost + HCost; } }
        public bool IsWalkable { get; set; }
        public Node Parent { get; set; }

        public Node(int x, int z, Vector3 worldPosition, int weight, bool isWalkable)
        {
            X = x;
            Z = z;
            WorldPosition = worldPosition;
            Weight = weight;
            IsWalkable = isWalkable;
        }

    }

    // 그리드 초기화
    public void InitializeGrid(int width, int height)
    {
        _gridWidth = width;
        _gridHeight = height;
        _grid = new Node[width, height];//월드 그리드크기 만큼 노드 생성
        //한칸한칸 노드 그리드 생성
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 worldPosition = new Vector3(x, 0, z);
                int weight = 1;
                bool iswalkable = true;
                // 현재 위치가 거리인지 확인
                RaycastHit hit;
                if (Physics.Raycast(worldPosition + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity))
                {
                    // 오브젝트가 거리 레이어에 있다면 가중치를 1로 설정
                    if (hit.collider.CompareTag("Street"))
                    {
                        weight = 1;
                        iswalkable = true;
                    }
                    // 오브젝트가 walkable 레이어에 있다면 가중치를 2로 설정
                    else if (hit.collider.CompareTag("Walkable"))
                    {
                        weight = 10;
                        iswalkable = true;
                    }
                    /*else if (((1 << hit.collider.gameObject.layer) & stoneLayer) != 0)
                    {
                        iswalkable = false;
                    }
                    else if (((1 << hit.collider.gameObject.layer) & treeLayer) != 0)
                    {
                        iswalkable = false;
                    }*/
                }

                _grid[x, z] = new Node(x, z, worldPosition, weight, iswalkable);
            }
        }
    }
    public List<Node> FindPath(Vector3 startPos, Vector3 endPos)//startPos에는 플레이어위치, endPos에는 목표위치
    {
        Node startNode = _grid[Mathf.RoundToInt(startPos.x), Mathf.RoundToInt(startPos.z)];
        Node endNode = _grid[Mathf.RoundToInt(endPos.x), Mathf.RoundToInt(endPos.z)];

        startNode.GCost = Vector3.Distance(startNode.WorldPosition, endNode.WorldPosition);
        
        List<Node> openSet = new List<Node>(); // 아직 방문하지 않은 노드들
        HashSet<Node> closedSet = new HashSet<Node>(); // 이미 방문한 노드들

        openSet.Add(startNode);

        // 오픈셋에 노드가 있는 동안 반복
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                // F코스트가 낮은 노드를 현재 노드로 설정
                if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost))
                {
                    currentNode = openSet[i];
                }
            }

            // 현재 노드를 오픈셋에서 제거하고 클로즈드셋에 추가
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            // 현재 노드의 이웃 노드들을 검사
            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                // 이웃 노드가 이미 방문한 노드이거나 이동 불가능한 경우, 건너뜀
                if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
                {
                    continue;
                }
                
                // 이웃 노드까지의 새로운 비용을 계산
                float newCostToNeighbor = Vector3.Distance(currentNode.WorldPosition, neighbor.WorldPosition) + neighbor.Weight + currentNode.Weight;
                if (newCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    // 이웃 노드의 비용과 휴리스틱을 업데이트
                    
                    neighbor.GCost = newCostToNeighbor;
                    neighbor.HCost = Vector3.Distance(neighbor.WorldPosition, endNode.WorldPosition);
                    neighbor.Parent = currentNode;

                    // 이웃 노드가 오픈셋에 없으면 추가
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        // 경로를 찾을 수 없는 경우
        return null;
    }
    // 경로를 되돌리고 최종 경로를 반환
    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        // 시작 노드까지 되돌아가며 경로를 생성
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        // 경로의 순서를 반전시키고 반환
        path.Reverse();
        return path;
    }

    // 두 노드 사이의 거리를 계산
    private int GetDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.X - b.X);
        int distZ = Mathf.Abs(a.Z - b.Z);

        // 대각선 이동이 포함된 경우, 가로 이동과 세로 이동 중 큰 값을 반환
        return Mathf.Max(distX, distZ);
    }

    // 주어진 노드의 이웃 노드들을 반환
    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // 상하좌우 이웃 노드를 확인
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                // 중앙 노드는 건너뜀
                if (x == 0 && z == 0) continue;

                // 대각선 이웃 노드는 건너뜀
                //if (x != 0 && z != 0) continue;

                int checkX = node.X + x;
                int checkZ = node.Z + z;

                // 이웃 노드가 그리드 내에 있는지 확인
                if (checkX >= 0 && checkX < _gridWidth && checkZ >= 0 && checkZ < _gridHeight)
                {
                    neighbors.Add(_grid[checkX, checkZ]);
                }
            }
        }

        return neighbors;
    }
}
