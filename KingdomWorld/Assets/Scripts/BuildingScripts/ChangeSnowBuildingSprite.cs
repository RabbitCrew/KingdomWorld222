using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSnowBuildingSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprArr;
    private SpriteRenderer sprRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        sprRenderer = this.gameObject.GetComponent<SpriteRenderer>();    
    }

    public void ChangeSprite(int i)
	{
        //Debug.Log(this.name);
        sprRenderer.sprite = sprArr[i];
	}
}
