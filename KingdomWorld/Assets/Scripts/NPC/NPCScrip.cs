using System.Collections.Generic;
using UnityEngine;

public class NPCScrip : NPCParameter
{
    public Setgrid Grid;
    public bool NPCBUildTrigger = false;
    //public Setgrid Grid;
    public Transform StartPos;
    public Transform EndPos;

    //private List<Setgrid.Node> path;
    private List<Setgrid.Node> path = null;
    protected int currentPathIndex = 0;
    private Setgrid setgrid = new Setgrid();
    private void Start()
    {
        
        //Grid.InitializeGrid(1000, 1000);
        /*InitializeGrid(1000, 1000);*/
        // path = Grid.FindPath(StartPos.position, EndPos.position);
        /*path = FindPath(StartPos.position, EndPos.position);*/
    }
    public void ResetPath(Transform start, Transform end)
    {
        //InitializeGrid(1000, 1000);
            path = Grid.FindPath(start.position, end.position);
        Debug.LogError(path);
        NPCBUildTrigger = false;
    }
    public void Move()
    {

        if (path != null && currentPathIndex < path.Count)
        {
            //transform.LookAt(path[currentPathIndex].WorldPosition);
            transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex].WorldPosition, Speed * Time.deltaTime);
            Debug.LogError(path[currentPathIndex].WorldPosition);
            if (Vector3.Distance(transform.position, path[currentPathIndex].WorldPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }
}