using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMananger : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GameObject.Find("Canvas").GetComponent<Animator>();
    }

    public void GoToInstructions()
    {
        animator.SetTrigger("Instructions");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Prototype");
    }
}
