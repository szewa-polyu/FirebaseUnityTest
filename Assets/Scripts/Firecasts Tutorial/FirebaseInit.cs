using Firebase;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseInit : MonoBehaviour
{
    public UnityEvent OnFirebaseInitialized = new UnityEvent();


    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log($"Failed to initialize Firebase with {task.Exception}");
                return;
            }

            Debug.Log("Firebase initialized");
            OnFirebaseInitialized?.Invoke();
        });
    }
}
