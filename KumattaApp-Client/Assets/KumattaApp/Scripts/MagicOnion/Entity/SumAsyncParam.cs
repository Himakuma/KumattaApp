using MessagePack;

[MessagePackObject]
public class SumAsyncParam
{
    [Key(0)]
    public int X { get; set; }
    [Key(1)]
    public int Y { get; set; }
}
