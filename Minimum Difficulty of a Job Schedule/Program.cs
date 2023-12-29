// See https://aka.ms/new-console-template for more information

var jobs = new int[] { 6, 5, 7, 3 };
int day = 3;
Solution s = new Solution();
var answer = s.MinDifficulty_Dfs(jobs, day);
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

    public int MinDifficulty_Dfs(int[] jobDifficulty, int day)
    {
        // we will be solving this using decision tree
        // at every step will take decision to 1. Continue with current job 2. End of that day
        // if we continue and dont break and move to next day, we can have the same current max as the max for next array item to compare
        // if we breakand move to next day, we have to decrement day by one and aslo need to update the cur_max as -1, we also need to add the previous day cur_max to this

        var length = jobDifficulty.Length;
        if (length < day) return -1;
        var cache = new Dictionary<(int, int, int), int>();
        int Dfs(int i, int d, int cur_max)
        {
            // base condition
            if (i == length) return d == 0 ? 0 : 10000;
            if (d == 0) return 10000;
            var key = (i, d, cur_max);
            if(cache.ContainsKey(key)) return cache[key];
            cur_max = Math.Max(cur_max, jobDifficulty[i]);

            // continue with current day 
            var a = Dfs(i + 1, d, cur_max);
            var b = cur_max + Dfs(i + 1, d - 1, -1);
            var res = Math.Min(a,b );

            cache.Add(key, res);
            return res;
        }

        return Dfs(0, day, -1);
    }
}