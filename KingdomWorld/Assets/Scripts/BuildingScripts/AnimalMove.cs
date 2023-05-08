using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public Transform trs;

    private bool outsideCheck = false;

    private float timer = 0f;
    private float increaseInterval = 4f;

    private List<Setgrid.Node> path = null;
    public Setgrid Grid;
    protected int currentPathIndex = 0;
    float Speed = 1f;

    Transform ETransform;
    private void Start()
    {
        Grid = GameManager.instance.GetComponent<Setgrid>();
    }
    
    private void Update()
    {
        timer += Time.deltaTime;

        RandomTimer();
        Move();
    }
    public void ResetPath(Transform start, Transform end)
    {
        path = Grid.FindPath(start.position, end.position);//게임매니저의 FindPath를 사용한다
    }
    public void Move()
    {

        if (path != null && currentPathIndex < path.Count)
        {
            //transform.LookAt(path[currentPathIndex].WorldPosition);
            transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex].WorldPosition, Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, path[currentPathIndex].WorldPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }
    private void RandomTimer()
    {
        if(timer >= increaseInterval)
        {
                Vector3 EPosition = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(1f, 4f));
                RaycastHit hit;
                if (Physics.Raycast(transform.parent.position + EPosition + Vector3.up * 20, Vector3.down, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("Walkable") || hit.collider.CompareTag("Stone"))
                    {
                        ETransform = hit.transform;
                        ResetPath(this.transform, ETransform);
                        currentPathIndex = 0;
                        timer = 0f;
                    }
                }
                timer = 0f;
        }
    }
}
