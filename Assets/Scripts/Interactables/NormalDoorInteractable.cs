using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NormalDoorInteractable : Interactable
{
    public bool unlocked = false;
    public bool open = false;

    private Animator animator;
    private AudioSource audioSource;

    private FirstPersonController controller;

    private void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (!unlocked)
        {
            StartCoroutine(controller.displayCaption("It's locked.", 3.0f));
        }
        else
        {
            string animation = open ? "open_door" : "close_door";

            animator.Play(animation);
            audioSource.Play();
        }
    }
}
