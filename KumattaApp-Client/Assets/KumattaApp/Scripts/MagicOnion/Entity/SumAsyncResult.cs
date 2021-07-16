using MessagePack;

[MessagePackObject]
public class SumAsyncResult 
{
    [Key(0)]
    public int Value { get; set; }
}
