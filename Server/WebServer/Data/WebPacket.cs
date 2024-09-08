

public class ScrapPacketReq 
{
    public string userId { get; set; }
    public string token { get; set; }
    public int scrap { get; set; }
}

public class ScrapPacketRes 
{
    public bool success { get; set; }
    public int scrap { get; set; }
}