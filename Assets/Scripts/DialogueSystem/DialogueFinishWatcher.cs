using UnityEngine;
using TMPro;

public class DialogueFinishWatcher : MonoBehaviour
{
    
    public TimerReceiver timerReceiver;     // drag an always-active object here

    private bool waitingForDialogueToClose = false;
    private Dialogue dialogue;
    void Awake()
    {
        dialogue = GetComponent<Dialogue>();
    }
     void Update()
    {
        if (dialogue == null || !gameObject.activeSelf)
            return;

        if (dialogue.lines != null && dialogue.lines.Length > 0)
        {
            string lastLine = dialogue.lines[dialogue.lines.Length - 1];

            if (dialogue.textComponent.text == lastLine)
            {
                waitingForDialogueToClose = true;
            }
        }
    }

    void OnDisable()
    {
        if (waitingForDialogueToClose && timerReceiver != null)
        {
            timerReceiver.StartNPCTimer();
            Debug.Log("Timer started");
        }

        waitingForDialogueToClose = false;
    }
}