namespace Weather.Models;
public class WeathModel
{
    public List<Weather> weather { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Clouds clouds { get; set; }
}