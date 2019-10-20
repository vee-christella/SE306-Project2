using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopAnimation : MonoBehaviour
{
    public GameObject Panel;

    public void OpenShop() 
    {
        Animator animator = Panel.GetComponent<Animator>();
        bool isOpen = animator.GetBool("open");

        animator.SetBool("open", true);
    }

    public void CloseShop() 
    {
        Animator animator = Panel.GetComponent<Animator>();
        bool isOpen = animator.GetBool("open");
        
        animator.SetBool("open", false);

    }
}
