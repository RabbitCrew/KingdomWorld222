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

        animator.SetInteger("IsIdle", 80);
        animator.SetInteger("IsWearingClothes", 80);
    }

    private void Start()
    {
        InvokeRepeating("GetPos", 0.1f, 0.08f);
    }

    private void FixedUpdate()
    {
        NPCAniSet();

        NPCAnimationSet();
    }

    public void SetPAni(int AniNum)
    {
        animator.SetInteger("IsIdle", AniNum);
    }

    void GetPos()
    {
        NPCPos = NPC.transform.position;
    }

    void NPCAnimationSet()
    {
        if(NPC.tag == "NPC")
        {
            animator.SetInteger("IsWearingClothes", -1);
        }
        else if (NPC.tag == "WoodCutter")
        {
            animator.SetInteger("IsWearingClothes", 6);
        }
        else if (NPC.tag == "CarpenterNPC")
        {
            animator.SetInteger("IsWearingClothes", 1);
        }
        else if (NPC.tag == "Smith")
        {
            animator.SetInteger("IsWearingClothes", 11);
        }
        else if (NPC.tag == "FabricNPC")
        {
            animator.SetInteger("IsWearingClothes", 8);
        }
        else if (NPC.tag == "CheeseNPC")
        {
            animator.SetInteger("IsWearingClothes", 7);
        }
        else if (NPC.tag == "HamNPC")
        {
            animator.SetInteger("IsWearingClothes", 9);
        }
        else if (NPC.tag == "StoneMineWorker")
        {
            animator.SetInteger("IsWearingClothes", 12);
        }
        else if (NPC.tag == "IronMineWorker")
        {
            animator.SetInteger("IsWearingClothes", 10);
        }
        else if (NPC.tag == "StorageNPC")
        {
            animator.SetInteger("IsWearingClothes", 5);
        }
        else if (NPC.tag == "Pastoralist")
        {
            animator.SetInteger("IsWearingClothes", 4);
        }
        else if (NPC.tag == "FarmNPC")
        {
            animator.SetInteger("IsWearingClothes", 2);
        }
        else if (NPC.tag == "Hunter")
        {
            animator.SetInteger("IsWearingClothes", 3);
        }
    }

    public void SetBNPC(int BuildNum)
    {
        //Debug.Log(this.name);
        switch (BuildNum)
        {
            // ½Ã¹Î
            case 0:
                NPC.tag = "NPC";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // ³ª¹«²Û
            case 1:
                NPC.tag = "WoodCutter";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // ¸ñ¼ö
            case 2:
                NPC.tag = "CarpenterNPC";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // »ç³É²Û
            case 3:
                NPC.tag = "Hunter";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // ³óºÎ
            case 4:
                NPC.tag = "FarmNPC";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // ¸ñÃà¾÷ÀÚ
            case 5:
                NPC.tag = "Pastoralist";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // Ã¢°íÁö±â
            case 6:
                NPC.tag = "StorageNPC";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // Ã¶ ±¤ºÎ
            case 7:
                NPC.tag = "IronMineWorker";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // µ¹ ±¤ºÎ
            case 8:
                NPC.tag = "StoneMineWorker";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // ÇÜ °¡°ø»ç
            case 9:
                NPC.tag = "HamNPC";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // Ä¡Áî °¡°ø»ç
            case 10:
                NPC.tag = "CheeseNPC";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;

                break;
            // ¿Ê°¨ °¡°ø»ç
            case 11:
                NPC.tag = "FabricNPC";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            // ´ëÀåÀåÀÌ
            case 12:
                NPC.tag = "Smith";
                NPC.GetComponent<NPC>().NPCBUildTrigger = true;
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

        //z°¡ À§¾Æ·¡ x°¡ ¾ç¿·
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