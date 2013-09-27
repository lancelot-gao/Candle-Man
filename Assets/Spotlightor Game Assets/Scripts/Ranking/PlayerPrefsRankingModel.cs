using UnityEngine;
using System.Collections;

/// <summary>
/// A local ranking using PlayerPrefs to store data.
/// </summary>
public class PlayerPrefsRankingModel : MonoBehaviour
{
	public string rankingDataKeyNamePrefix = "local_ranking_";
	public int defaultTopScore = 20;
	public bool scoreIncreaseByRank = true;
	public int defaultScoreStep = 1;
	public int rankingCapacity = 9;
	private int[] rankingScores;
	
	public int[] RankingScores {
		get {
			if (rankingScores == null) {
				LoadRankingScores ();
			}
			return rankingScores;
		}
	}
	
	public void LoadRankingScores ()
	{
		rankingScores = new int[rankingCapacity];
		for (int i = 0; i < rankingScores.Length; i++) {
			int defaultScore = defaultTopScore;
			if (scoreIncreaseByRank)
				defaultScore -= defaultScoreStep * i;
			else
				defaultScore += defaultScoreStep * i;
			rankingScores [i] = PlayerPrefs.GetInt (GetKeyNameByRank (i), defaultScore);
		}
	}
	
	public int InsertRankingScoreAndGetRank (int score)
	{
		int scoreIndex = 99;
		if (IsInRanking (score)) {
			scoreIndex = 0;
			if (scoreIncreaseByRank) {
				while (score < RankingScores [scoreIndex])
					scoreIndex++;
			} else {
				while (score > RankingScores [scoreIndex])
					scoreIndex++;
			}
			for (int i = rankingCapacity-1; i > scoreIndex; i--) {
				RankingScores [i] = RankingScores [i - 1];
			}
			RankingScores [scoreIndex] = score;
			
		}
		return scoreIndex + 1;
	}
	
	public bool IsInRanking (int score)
	{
		return scoreIncreaseByRank ? score > RankingScores [rankingCapacity - 1] : score < RankingScores [rankingCapacity - 1];
	}
	
	private void OnDestroy ()
	{
		SaveRankingDatas ();
	}
	
	private void OnApplicationQuit ()
	{
		SaveRankingDatas ();
	}
	
	public void SaveRankingDatas ()
	{
		for (int i = 0; i < rankingCapacity; i++) {
			PlayerPrefs.SetInt (GetKeyNameByRank (i), RankingScores [i]);
		}
		PlayerPrefs.Save ();
	}
	
	public void ClearRankingDatas ()
	{
		for (int i = 0; i < rankingCapacity; i++) {
			PlayerPrefs.DeleteKey (GetKeyNameByRank (i));
			rankingScores = null;
		}
	}
	
	private string GetKeyNameByRank (int rank)
	{
		return rankingDataKeyNamePrefix + rank.ToString ();
	}
	
}
