using UnityEngine;
using TMPro;

public class TimerReceiver : MonoBehaviour
{
    [Header("Timer Settings")]
    public float countdownTime;

    [Header("UI References")]
    public GameObject timerUI;                // drag TimerUI panel
    public TextMeshProUGUI timerText;         // drag TimerText

    private float timeRemaining;
    private bool timerRunning = false;
    private bool timerFinished = false;

    void Start()
    {
        if (timerUI != null)
            timerUI.SetActive(false);
    }

    void Update()
    {
        if (!timerRunning || timerFinished)
            return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining < 0)
                timeRemaining = 0;

            UpdateTimerDisplay();
        }
        else
        {
            timerRunning = false;
            timerFinished = true;

            if (timerUI != null)
                timerUI.SetActive(false);

            DoSomethingAfterTimer();
        }
    }

    public void StartNPCTimer()
    {
        if (timerRunning || timerFinished)
            return;

        timeRemaining = countdownTime;
        timerRunning = true;

        if (timerUI != null)
            timerUI.SetActive(true);

        UpdateTimerDisplay();

        Debug.Log("Timer started");
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    void DoSomethingAfterTimer()
    {
        Debug.Log("Timer finished!");
        // Enemy spawns, Deceives villager, Quest fails
    }
}