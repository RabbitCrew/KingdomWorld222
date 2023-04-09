using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingNPCSet : MonoBehaviour
{
    public Animator animator;

    public GameObject NPC;
    Vector3 NPCPos;

    private void Awake()
    {
        NPCPos = NPC.transform.position;
    }

    private void Start()
    {
        InvokeRepeating("GetPos", 0.1f, 0.08f);
    }

    private void FixedUpdate()
    {
        NPCAniSet();
    }

    public void SetPAni(int AniNum)
    {
        animator.SetInteger("IsIdle", AniNum);
    }

    void GetPos()
    {
        NPCPos = NPC.transform.position;
    }

    public void SetBNPC(int BuildNum)
    {
        switch (BuildNum)
        {
            case 0:
                NPC.gameObject.tag = "StorageNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;    
                break;
            case 1:
                NPC.gameObject.tag = "CarpenterNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 2:
                NPC.gameObject.tag = "CheeseNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 3:
                NPC.gameObject.tag = "FabricNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 4:
                NPC.gameObject.tag = "FarmNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 5:
                NPC.gameObject.tag = "HamNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 6:
                NPC.gameObject.tag = "NPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 7:
                NPC.gameObject.tag = "Hunter";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 8:
                NPC.gameObject.tag = "MineWorker";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 9:
                NPC.gameObject.tag = "WoodCutter";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 10:
                NPC.gameObject.tag = "Smith";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            default:
                break;
        }
    }

    void NPCAniSet()
    {
        float Directionx = 0;
        float Directionz = 0;

        bool GoRight = false;
        bool GoFront = false;

        if(NPC.transform.position.x > NPCPos.x)
        {
            Directionx = (NPC.transform.position.x - NPCPos.x);

            GoRight = true;
        }
        else if(NPC.transform.position.x < NPCPos.x)
        {
            Directionx = (NPCPos.x - NPC.transform.position.x);

            GoRight = false;
        }

        if(NPC.transform.position.z > NPCPos.z)
        {
            Directionz = (NPC.transform.position.z - NPCPos.z);

            GoFront = false;
        }
        else if(NPC.transform.position.z < NPCPos.z)
        {
            Directionz = (NPCPos.z - NPC.transform.position.z);

            GoFront = true;
        }
        
        if(NPC.transform.position == NPCPos)
        {
            GoFront = false;
            GoRight = false;

            animator.SetBool("IsFront", false);
            animator.SetBool("IsBack", false);

            animator.SetBool("IsRight", false);
            animator.SetBool("IsLeft", false);
        }

        //z가 위아래 x가 양옆
        if(GoFront == true)
        {
            animator.SetBool("IsFront", true);
            animator.SetBool("IsBack", false);

            if (Directionx > Directionz)
            {
                animator.SetBool("IsFront", false);
                animator.SetBool("IsBack", false);

                animator.SetBool("IsRight", true);
                animator.SetBool("IsLeft", false);
            }
            else if(Directionx < Directionz)
            {
                animator.SetBool("IsRight", false);
                animator.SetBool("IsLeft", false);
            }
        }
        else
        {
            animator.SetBool("IsFront", false);
            animator.SetBool("IsBack", true);

            if (Directionx > Directionz)
            {
                animator.SetBool("IsFront", false);
                animator.SetBool("IsBack", false);

                if (GoRight == true)
                {
                    animator.SetBool("IsRight", true);
                    animator.SetBool("IsLeft", false);
                }
                else
                {
                    animator.SetBool("IsRight", false);
                    animator.SetBool("IsLeft", true);
                }
            }
            else if (Directionx < Directionz)
            {
                animator.SetBool("IsRight", false);
                animator.SetBool("IsLeft", false);

                animator.SetBool("IsFront", false);
                animator.SetBool("IsBack", true);
            }
        }

        if(GoRight == false && GoFront == false)
        {
            animator.SetBool("IsRight", false);
            animator.SetBool("IsLeft", false);

            animator.SetBool("IsFront", false);
            animator.SetBool("IsBack", false);
        }
    }
}