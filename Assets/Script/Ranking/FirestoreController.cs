using Firebase.Firestore;
using UnityEngine;
using Firebase.Extensions;

public class FirestoreController : MonoBehaviour
{
    void Start()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        db.Collection("Test").Document("Doc1").SetAsync(new { hello = "����!" })
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompletedSuccessfully)
                    Debug.Log("Firestore ���ε� ����!");
                else
                    Debug.LogError("Firestore ���ε� ����: " + task.Exception);
            });
    }
}
