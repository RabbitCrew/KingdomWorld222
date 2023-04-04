using System.Collections.Generic;
using UnityEngine;

public class NPCScrip : Setgrid
{
    public bool NPCBUildTrigger = false;
    //public Setgrid Grid;
    public Transform StartPos;
    public Transform EndPos;

    //private List<Setgrid.Node> path;
    private List<Node> path = null;
    private int currentPathIndex = 0;

    private void Start()
    {
        //Grid.InitializeGrid(1000, 1000);
        /*InitializeGrid(1000, 1000);*/
        // path = Grid.FindPath(StartPos.position, EndPos.position);
        /*path = FindPath(StartPos.position, EndPos.position);*/
    }
    public void ResetPath(Transform start, Transform end)
    {
        InitializeGrid(1000, 1000);
        path = FindPath(start.position, end.position);
        NPCBUildTrigger = false;
    }
    public void Move()
    {
        
        if (path != null && currentPathIndex < path.Count)
        {
            Debug.LogError("path ½ÇÇà");
            //transform.LookAt(path[currentPathIndex].WorldPosition);
            transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex].WorldPosition, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, path[currentPathIndex].WorldPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }
}