// See https://aka.ms/new-console-template for more information

var jobs = new int[] { 6, 5, 7, 3 };
int day = 3;
Solution s = new Solution();
var answer = s.MinDifficulty(jobs, day);
Console.WriteLine(answer);

public class Solution
{
  public int MinDifficulty(int[] jobDifficulty, int day)
  {
    var length = jobDifficulty.Length;
    var inf = int.MaxValue;
    int maxd = 0;
    if (length < day) return -1;
    var dp = new int[length + 1];
    Array.Fill(dp, inf);
    dp[length] = 0;
    for (int d = 1; d <= day; d++)
    {
      for (int i = 0; i <= length - d; i++)
      {
        maxd = 0;
        dp[i] = inf;
        for (int j = i; j <= length - d; j++)
        {
          maxd = Math.Max(maxd, jobDifficulty[j]);
          if (dp[j + 1] != inf)
          {
            dp[i] = Math.Min(dp[i], maxd + dp[j + 1]);
          }
        }
      }
    }

    return dp[0];
  }
}