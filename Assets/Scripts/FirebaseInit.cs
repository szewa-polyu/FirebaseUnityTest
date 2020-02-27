using Firebase;
using Firebase.Database;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{    
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            
        });
    }
}
