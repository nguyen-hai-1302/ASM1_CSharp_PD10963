using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>();

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {        
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    public string calculate_rank(int score)
    {
        if (score < 100)
        {
            return "Dong";
        }
        else if ( score < 500)
        {
            return "Bac";
        }
        else if (score < 1000)
        {
            return "Vang";
        }
        else if (score >= 1000)
        {
            return "Kim Cuong";
        }
        return null;
    }

    public void YC1()
    {
        string name = ScoreKeeper.Instance.GetUserName();
        int id = ScoreKeeper.Instance.GetID();
        int idR = ScoreKeeper.Instance.GetIDregion();
        int score = ScoreKeeper.Instance.GetScore();
        string regionName = "";
        if (idR == 0)
        {
            regionName = "VN";
        }
        else if (idR == 1)
        {
            regionName = "VN1";
        }
        else if (idR == 2)
        {
            regionName = "VN2";
        }
        else if (idR == 3)
        {
            regionName = "JS";
        }
        else if (idR == 4)
        {
            regionName = "VS";
        }
        else
        {
            Debug.Log("Khong tim thay vung ID");
        }
        //
        Region region1 = new Region(idR, regionName);
        Players Player1 = new Players(id, "Anh", 20, region1);
        listPlayer.Add(Player1);
        //
        Region region2 = new Region(idR, regionName);
        Players Player2 = new Players(id, "Hoa", 500, region2);
        listPlayer.Add(Player2);
        //
        Region region3 = new Region(idR, regionName);
        Players Player3 = new Players(id, name, score, region3);
        listPlayer.Add(Player3);
       
    }  
    //
    public void YC2()
    {
        foreach (Players players in listPlayer)
        {
            Debug.Log("Player Name: " + players.Name +"- Region: " + players.PlayerRegion.Name + "- Score: " + players.Score + "- Rank: " + calculate_rank(players.Score));            
        }
    }
    public void YC3()
    {
        if (listPlayer.Count == 0)
        {
            Debug.Log("Không có người chơi nào.");
            return;
        }
       
        int currentPlayerScore = ScoreKeeper.Instance.GetScore();
        
        var less = listPlayer.Where(player => player.Score < currentPlayerScore);

        if (!less.Any())
        {
            Debug.Log("Không có người chơi nào có điểm số bé hơn.");
            return;
        }
       
        Debug.Log("Các người chơi có điểm số bé hơn điểm số hiện tại:");
        foreach (var player in less)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log("Player Name: " + player.Name + "- Region: " + player.PlayerRegion.Name + "- Score: " + player.Score + "- Rank: " + rank);
        }
    }
    public void YC4()
    {        
        int currentPlayerId = ScoreKeeper.Instance.GetID();
        var findPlayerByID = listPlayer.FirstOrDefault(player => player.ID == currentPlayerId);

        if (findPlayerByID != null)
        {
            string rank = calculate_rank(findPlayerByID.Score);
            Debug.Log("Player found - Name: " + findPlayerByID.Name + " - Score: " + findPlayerByID.Score + " - Rank: " + rank);
        }
        else
        {
            Debug.Log("Không tìm thấy người chơi với ID: " + currentPlayerId);
        }
    }
    //
    public void YC5()
    {
        var sortedPlayers = listPlayer.OrderByDescending(player => player.Score);
        //In ra
        Debug.Log("Thông tin các người chơi theo thứ tự điểm số giảm dần:");
        foreach (var player in sortedPlayers)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log("Player Name: " + player.Name + "- Region: " + player.PlayerRegion.Name + "- Score: " + player.Score + "- Rank: " + rank);
        }
    }
    //
    public void YC6()
    {
         
        var lowestScorePlayers = listPlayer.OrderBy(player => player.Score).Take(5);
            
        Debug.Log("Thông tin 5 người chơi có điểm số thấp nhất theo thứ tự tăng dần:");
        foreach (var player in lowestScorePlayers)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log("Player Name: " + player.Name + "- Region: " + player.PlayerRegion.Name + "- Score: " + player.Score + "- Rank: " + rank);
        }
    }
    //
    public void YC7()
    {
    
        Thread leaderboardThread = new Thread(new ThreadStart(CalculateAndSaveAverageScoreByRegion));
        leaderboardThread.Name = "BXH";
        leaderboardThread.Start();
    }

    void CalculateAndSaveAverageScoreByRegion()
    {
        Dictionary<string, List<int>> scoresByRegion = new Dictionary<string, List<int>>();
    
        foreach (var player in listPlayer)
        {
            string regionName = player.PlayerRegion.Name;

            if (!scoresByRegion.ContainsKey(regionName))
            {
                scoresByRegion[regionName] = new List<int>();
            }

            scoresByRegion[regionName].Add(player.Score);
        }

    
        using (StreamWriter writer = new StreamWriter("bxhRegion.txt"))
        {
            foreach (var regionName in scoresByRegion.Keys)
            {
                double averageScore = scoresByRegion[regionName].Average();
                writer.WriteLine($"Region: {regionName}, Average Score: {averageScore:F2}");
            }
        }

        Debug.Log("Đã tính toán và lưu điểm số trung bình của mỗi khu vực vào tệp bxhRegion.txt");
    }
}

[SerializeField]
public class Region
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    public int ID {  get; set; }
    public string Name { get; set; }  
    public int Score { get; set; }
    public Region PlayerRegion {  get; set; }
    public Players(int id, string name, int score, Region region)
    {
        ID = id;
        Name = name;
        Score = score;
        PlayerRegion = region;

    }
}