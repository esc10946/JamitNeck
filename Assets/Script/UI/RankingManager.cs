using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;

public class RankingManager : MonoBehaviour
{
    public static RankingManager Instance { get; private set; }

    [Header("UI")]
    public TMP_InputField nicknameInput;
    public GameObject rankListPanel;
    public Transform rankContentParent;
    public GameObject rankItemPrefab;

    private FirebaseFirestore db;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        db = FirebaseFirestore.DefaultInstance;
    }

    public void SubmitScore(int score)
    {
        string nickname = nicknameInput.text.Trim();
        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("�г����� �Է��ϼ���.");
            return;
        }

        var data = new Dictionary<string, object>
        {
            { "nickname", nickname },
            { "score", score }
        };

        db.Collection("Ranking").Document(nickname).SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log($"{nickname} ���� ���� �Ϸ�!");
                //LoadTopRanking();
            }
            else
            {
                Debug.LogError("Firestore ���� ����: " + task.Exception);
            }
        });
    }

    /*
    public void LoadTopRanking()
    {
        // ���� �׸� ����
        foreach (Transform child in rankContentParent)
            Destroy(child.gameObject);

        db.Collection("Ranking")
          .OrderBy("score", Query.Direction.Descending)
          .Limit(10)
          .GetSnapshotAsync()
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsCompletedSuccessfully)
              {
                  foreach (var doc in task.Result.Documents)
                  {
                      string name = doc.GetValue<string>("nickname");
                      int score = doc.GetValue<int>("score");

                      GameObject item = Instantiate(rankItemPrefab, rankContentParent);
                      item.GetComponent<TMP_Text>().text = $"{name} - {score}";
                  }

                  UIManager.Instance.ShowGameOverUI(); // ��ŷ ������ ���ӿ��� ���
              }
              else
              {
                  Debug.LogError("��ŷ �ҷ����� ����: " + task.Exception);
              }
          });
    }*/
}
