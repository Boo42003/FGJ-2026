using System;
using UnityEngine;

public class MicrowaveInteractable : UIInteractable
{
    public MicrowaveButton[,] buttons = new MicrowaveButton[3,3];
    public bool[,] solution =
    {
        {false, false, true },
        {false, false, true },
        {false, false, true },
    };
    public Canvas buttonsCanvas;
    public AudioClip wrongSound;
    public AudioClip microwaveSound;

    private bool solved = false;

    private void Start()
    {
        controller = FindAnyObjectByType<FirstPersonController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        MicrowaveButton[] buttonChildren = buttonsCanvas.GetComponentsInChildren<MicrowaveButton>();

        for (int i = 0; i < buttonChildren.Length; i++) {
            double division = (double)i/3;
            double ceil = Math.Floor(division);

            int row = (int)ceil;
            int col = i - (3 * row);

            buttons[row,col] = buttonChildren[i];
        }
    }

    public void SyncButtonState(string coordinate)
    {
        string[] split = coordinate.Split(',');
        int row = Int32.Parse(split[0]);
        int col = Int32.Parse(split[1]);
        if(col > 0)
        {
            buttons[row, col - 1].ToggleActive();
        }

        if(col + 1 < 3)
        {
            buttons[row, col + 1].ToggleActive();
        }

        if (row > 0)
        {
            buttons[row - 1, col].ToggleActive();
        }

        if (row + 1 < 3)
        {
            buttons[row + 1, col].ToggleActive();
        }
    }

    public void CheckSolution()
    {
        if (solved)
        {
            StartCoroutine(controller.displayCaption("Don't need the microwave anymore.", 5f));
            return;
        }
        
        bool wrongState = false;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (buttons[i, j].active != solution[i, j])
                {
                    wrongState = true;
                    break;
                }
            }

            if (wrongState)
            {
                break;
            }
        }

        if (!wrongState)
        {
            ThawItem();
        }
        else
        {
            ResetButtons();
        }
    }

    public void ThawItem()
    {
        solved = true;
        audioSource.PlayOneShot(microwaveSound);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                buttons[i, j].solved = true;
            }
        }
    }

    public void ResetButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                buttons[i, j].ResetState();
            }
        }

        audioSource.PlayOneShot(wrongSound);
    }
}
