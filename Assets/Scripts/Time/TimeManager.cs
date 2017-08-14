using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    [SerializeField]
    private float LerpSpeed = 5;
    [SerializeField]
    private float _minTimeScale = 0.05f;
    [SerializeField]
    private float _maxTimeScale = 1;

    private static float TargetTimeScale;
    private static float MaxTimeScale;
    private static float MinTimeScale;

    private static List<TickOverTimeEntry> tickOverTimeEntries;
    
    private void Awake()
    {
        MaxTimeScale = _maxTimeScale;
        MinTimeScale = _minTimeScale;

        tickOverTimeEntries = new List<TickOverTimeEntry>();
    }
    private void Update()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, TargetTimeScale, LerpSpeed * Time.unscaledDeltaTime);

        TargetTimeScale = MinTimeScale;
        
        DoTicksOverTime();
    }
    public static void Tick()
    {
        TargetTimeScale = MaxTimeScale;
    }
    public static void Tick(float targetTimeScale)
    {
        TargetTimeScale = targetTimeScale;
    }
    public static void TickOverTime(float duration)
    {
        tickOverTimeEntries.Add(new TickOverTimeEntry() { DurationLeft = duration, ScaleAmount = MaxTimeScale });
    }
    public static void TickOverTime(float duration, float timeScale)
    {
        tickOverTimeEntries.Add(new TickOverTimeEntry() { DurationLeft = duration, ScaleAmount = timeScale });
    }
    private void DoTicksOverTime()
    {
        if (tickOverTimeEntries.Count == 0)
            return;

        List<TickOverTimeEntry> ValidEntries = new List<TickOverTimeEntry>();

        float sum = 0;

        for (int i = 0; i < tickOverTimeEntries.Count; i++)
        {
            TickOverTimeEntry entry = tickOverTimeEntries[i];

            entry.DurationLeft -= Time.unscaledDeltaTime;

            if(entry.DurationLeft >= 0)
            {
                ValidEntries.Add(entry);

                sum += entry.ScaleAmount;
            }
        }

        if (ValidEntries.Count == 0)
            return;

        TargetTimeScale = sum / ValidEntries.Count;

        tickOverTimeEntries = ValidEntries;
    }
    private struct TickOverTimeEntry
    {
        public float ScaleAmount;
        public float DurationLeft;
    }
}
