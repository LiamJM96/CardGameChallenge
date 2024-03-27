namespace CardGameChallenge.Shared;

public class Card
{
    public string Code { get; init; }
    public int Value { get; init; }
    public bool IsJoker { get; init; }
    
    public Card(string code, int value, bool isJoker)
    {
        Code = code;
        Value = value;
        IsJoker = isJoker;
    }
}