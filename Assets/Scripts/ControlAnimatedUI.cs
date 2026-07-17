using UnityEngine;

public class ControlAnimatedUI : MonoBehaviour
{
    public UISpriteAnimation[] animatedSprites;

    private void Start()
    {
        animatedSprites = GetComponentsInChildren<UISpriteAnimation>();
    }

    public void StopAnimation()
    {
        foreach (var anim in animatedSprites)
        {
            anim.Stop();
        }
    }

    public void StartAnimation()
    {
        foreach (var anim in animatedSprites)
        {
            anim.Func_PlayUIAnim();
        }
    }
}
