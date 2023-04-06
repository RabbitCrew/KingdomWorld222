using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite[] citizenSprArr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Sprite GetCitizenSprArr(int index)
    {
        if (index < citizenSprArr.Length && index > -1)
        {
            return citizenSprArr[index];
        }
        else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
