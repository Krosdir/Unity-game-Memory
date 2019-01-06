using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targerObject;
    [SerializeField] private string targetMessage;
    public Color highlightColor = Color.cyan;

    public void OnMouseOver()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
            sprite.color = highlightColor;
    }

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
            sprite.color = Color.white;
    }

    public void OnMouseUp()
    {
        transform.localScale = new Vector3(0.183f, 0.183f, 0.183f);
        if (targerObject != null)
            targerObject.SendMessage(targetMessage);
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(0.233f, 0.233f, 0.233f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
