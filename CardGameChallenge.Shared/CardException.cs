namespace CardGameChallenge.Shared;

[Serializable]
public class CardException : Exception
{
    public CardException(string? message) : base(message) { }
}