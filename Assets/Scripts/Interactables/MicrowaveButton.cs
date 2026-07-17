using UnityEngine;

public class MicrowaveButton : MonoBehaviour
{
    public bool active = false;
    public Sprite[] inactiveSprites;
    public Sprite[] activeSprites;
    public bool solved = false;

    private UISpriteAnimation spriteAnimation;
    private AudioSource audioSource;

    private void Start()
    {
        spriteAnimation = GetComponent<UISpriteAnimation>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleActive()
    {
        if(solved)
        {
            return;
        }

        active = !active;

        spriteAnimation.m_SpriteArray = active ? activeSprites : inactiveSprites;
        audioSource.Play();
    }

    public void ResetState()
    {
        active = false;
        spriteAnimation.m_SpriteArray = inactiveSprites;
    }
}
