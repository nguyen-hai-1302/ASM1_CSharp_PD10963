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
        if(idR == 0)
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

        //Region region = null;
        //switch (idR)
        //{   
        //    case 0:
        //        region = new Region(0, "VN");
        //        break;
        //    case 1:
        //        region = new Region(1, "VN1");
        //        break;
        //    case 2:
        //        region = new Region(2, "VN2");
        //        break;
        //    case 3:
        //        region = new Region(3, "JS");
        //        break;
        //    case 4:
        //        region = new Region(4, "VS");
        //        break;
        //    default:
        //        Debug.Log("Khong tim thay vung ID");
        //        break;
        //}      
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

        // Lấy điểm số của người chơi đầu tiên trong danh sách
        int currentPlayerScore = ScoreKeeper.Instance.GetScore();

        // Tìm các người chơi có điểm số bé hơn điểm số của người chơi hiện tại
        var less = listPlayer.Where(player => player.Score < currentPlayerScore);

        if (!less.Any())
        {
            Debug.Log("Không có người chơi nào có điểm số bé hơn.");
            return;
        }

        // Hiển thị thông tin của các người chơi có điểm số bé hơn
        Debug.Log("Các người chơi có điểm số bé hơn điểm số hiện tại:");
        foreach (var player in less)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log("Tên: " + player.Name + ", Điểm số: " + player.Score + ", Xếp hạng: " + rank);
        }
    }
    public void YC4()
    {
        // sinh viên viết tiếp code ở đây
    }
    public void YC5()
    {
        // sinh viên viết tiếp code ở đây
    }
    public void YC6()
    {
        // sinh viên viết tiếp code ở đây
    }
    public void YC7()
    {
        // sinh viên viết tiếp code ở đây
    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        // sinh viên viết tiếp code ở đây
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