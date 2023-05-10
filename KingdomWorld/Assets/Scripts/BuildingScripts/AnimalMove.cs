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

    public Animator animator;

    Vector3 AnimalPos;

    private void Start()
    {
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsRight", false);
        animator.SetBool("IsLeft", false);
        animator.SetBool("Hungry", false);

        Grid = GameManager.instance.GetComponent<Setgrid>();

        InvokeRepeating("GetAnimalPos", 0.1f, 0.5f);
    }
    
    private void Update()
    {
        timer += Time.deltaTime;

        RandomTimer();
        Move();

        AnimalAniSet();
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

    void GetAnimalPos()
    {
        AnimalPos = trs.position;
    }

    void AnimalAniSet()
    {
        bool GoRight = false;
        bool GoLeft = false;
        bool GoUpDown = false;

        if (trs.position.x > AnimalPos.x) //처음 받은 값보다 현재 값이 오른쪽일때
        {
            GoRight = true;
            GoLeft = false;
            GoUpDown = false;
        }
        else if (trs.position.x < AnimalPos.x)// 처음 받은 값보다 현재 값이 왼쪽일 때
        {
            GoRight = false;
            GoUpDown = false;
            GoLeft = true;
        }
        else if(trs.position == AnimalPos)
        {
            GoUpDown = false;
            GoRight = false;
            GoLeft = false;
        }
        else if(trs.position.z != AnimalPos.z)
        {
            GoUpDown = true;
            GoRight = false;
            GoLeft = false;
        }

        if(GoRight == true)
        {
            animator.SetBool("IsRight", true);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsHungry", false);
            animator.SetBool("IsLeft", false);
        }
        else if(GoLeft == true)
        {
            animator.SetBool("IsRight", false);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsHungry", false);
            animator.SetBool("IsLeft", true);
        }
        else if(GoUpDown == true)
        {
            animator.SetBool("IsRight", false);
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsHungry", false);
            animator.SetBool("IsLeft", false);
        }
        else
        {
            animator.SetBool("IsRight", false);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsHungry", true);
            animator.SetBool("IsLeft", false);
        }
 
    }//애니메이션 만드는중. 미완성임
}
