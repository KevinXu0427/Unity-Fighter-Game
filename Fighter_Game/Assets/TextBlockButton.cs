using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextBlockButton : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void OnMouseEnterEvent()
    {
        Debug.Log("Enter");
        //anim.SetTrigger("MouseEnter");
        anim.SetBool("MouseEnter1",true);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
