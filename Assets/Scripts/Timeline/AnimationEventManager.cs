using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    [SerializeField] private float CurrentTime;

    public bool isPaused = false;

    public bool DebugTime = false;

    [SerializeField] private float StartTime = 0;

    // Define the OnUpdate event
    public event Action<float> OnUpdate;

    public List<GameObject> animationEvents;


    private void Start()
    {
        CurrentTime = StartTime;
        Debug.Log("Manager Started");
    }

    private void Update()
    {
        if (!isPaused)
        {
            // Invoke the OnUpdate event with the current time
            OnUpdate?.Invoke(Time.deltaTime);

            CurrentTime = CurrentTime + Time.deltaTime;

            if (DebugTime)
            {
                //debug.logging current time
                Debug.Log(CurrentTime);
            }

            // List<GameObject> eventsToRemove = new List<GameObject>(); // Collect events to remove

            foreach (var animEventObject in animationEvents)
            {
                AnimationEventTrigger animEvent = animEventObject.GetComponent<AnimationEventTrigger>();

                if (animEvent != null && CurrentTime >= animEvent.GetTime())
                {
                    animEvent.TriggerEvent();
                   // eventsToRemove.Add(animEventObject);
                }
            }
            // Remove events after iterating
          /*  foreach (var eventToRemove in eventsToRemove)
            {
                RemoveEvent(eventToRemove);
            } */
        }
    }

    public void PauseEvents()
    {
        isPaused = true;
    }

    public void ResumeEvents()
    {
        isPaused = false;
    }

    public void AddEvent(GameObject eventObject)
    {
        animationEvents.Add(eventObject);
    }

    public void RemoveEvent(GameObject eventObject)
    {
        animationEvents.Remove(eventObject);
    }
}
