using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PhoneInteractable : UIInteractable
{
    public string correctSequence;
    public AudioClip[] buttonSounds;
    public TextMeshProUGUI firstPart;
    public TextMeshProUGUI secondPart;
    public TextMeshProUGUI thirdPart;

    private string currentSequence = "";
    private bool isLockedIn = false;
    private readonly Dictionary<string, int> buttonValueToSoundIndex = new()
    {
        {"1", 0},
        {"2", 1},
        {"3", 2},
        {"4", 3},
        {"5", 4},
        {"6", 5},
        {"7", 6},
        {"8", 7},
        {"9", 8},
        {"*", 9},
        {"0", 10},
        {"#", 11},
    };
    public void PhoneButtonPressed(string value)
    {
        if (isLockedIn) {
            StartCoroutine(controller.displayCaption("Can't call. The number is stuck on screen.", 5f));
            return;
        }

        if (!Level1Manager.Instance.powerOn)
        {
            StartCoroutine(controller.displayCaption("No power. Phone doesn't work.", 5f));
        }
        else
        {
            int sequenceLen = currentSequence.Length;
            if (sequenceLen == 6) {
                currentSequence = "";
                firstPart.text = "";
                secondPart.text = "";
                thirdPart.text = "";
                sequenceLen = 0;
            }

            if(sequenceLen < 2)
            {
                firstPart.text += value;
            }
            else if(sequenceLen < 4)
            {
                secondPart.text += value;
            }
            else
            {
                thirdPart.text += value;
            }

            int buttonIndex = buttonValueToSoundIndex[value];
            audioSource.PlayOneShot(buttonSounds[buttonIndex]);
            currentSequence += value;

            if (currentSequence.Length == 6 && currentSequence == correctSequence) {
                LockSequence();
            }
        }
    }

    public void LockSequence()
    {
        isLockedIn = true;
    }
}
