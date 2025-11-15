using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void Awake() {
        if (Response.GetPersistentEventCount() == 0) {
            Debug.LogWarning("Response method missing");
        }
    }

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised() {
        Response.Invoke();
    }
}
