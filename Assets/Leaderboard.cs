using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardCanvas;
    public GameObject[] leaderboardEntries;
    public static Leaderboard instance;
    void Awake() { instance = this; }
    public void OnLoggedIn()
    {
        leaderboardCanvas.SetActive(true);
    }
    public void DisplayLeaderboard()
    {
        GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest
        {
            StatisticName = "Longest Life",
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest,
            result => UpdateLeaderboardUI(result.Leaderboard),
            error => Debug.Log(error.ErrorMessage)
        );
    }
    void UpdateLeaderboardUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        for (int x = 0; x < leaderboardEntries.Length; x++)
        {
            leaderboardEntries[x].SetActive(x < leaderboard.Count);
            if (x >= leaderboard.Count) continue;

            leaderboardEntries[x].transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = (leaderboard[x].Position + 1) + ". ";

            leaderboardEntries[x].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = leaderboard[x].DisplayName;

            leaderboardEntries[x].transform.Find("Score").GetComponent<TextMeshProUGUI>().text = (-(float)leaderboard[x].StatValue * 0.001f).ToString("F2");
        }
    }
    public void SetLeaderboardEntry(int newScore)
    {
        bool useLegacyMethod = false;
        if (useLegacyMethod)
        {
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
            {
                FunctionName = "UpdateHighscore",
                FunctionParameter = new { score = newScore }
            };
            PlayFabClientAPI.ExecuteCloudScript(request,
                result =>
                {
                    Debug.Log(result);
                    //Debug.Log("SUCCESS");
                    //Debug.Log(result.FunctionName);
                    //Debug.Log(result.FunctionResult);
                    //Debug.Log(result.FunctionResultTooLarge);
                    //Debug.Log(result.Error);
                    DisplayLeaderboard();
                    Debug.Log(result.ToJson());
                },
                error =>
                {
                    Debug.Log(error.ErrorMessage);
                    Debug.Log("ERROR");
                }
            );
        }
        else
        {
            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
                {
                    Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate { StatisticName = "Longest Life", Value = newScore },
                    }
                },
                result => { Debug.Log("User statistics updated"); },
                error => { Debug.LogError(error.GenerateErrorReport()); }
            );
    
        }
    }
}
