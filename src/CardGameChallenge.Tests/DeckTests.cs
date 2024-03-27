using CardGameChallenge.Shared;
using Xunit;

namespace CardGameChallenge.Tests;

public class DeckTests
{
    /// <summary>
    /// Scenario: Convert a List of Cards to their Score<br/><br/>
    /// GIVEN I have started the Card Game application<br/>
    /// WHEN I enter a 'listOfCards'<br/>
    /// THEN the correct 'score' should be displayed on the user interface<br/>
    /// </summary>
    [Theory]
    [InlineData("2C", 2)]
    [InlineData("2D", 4)]
    [InlineData("2H", 6)]
    [InlineData("2S", 8)]
    [InlineData("TC", 10)]
    [InlineData("JC", 11)]
    [InlineData("QC", 12)]
    [InlineData("KC", 13)]
    [InlineData("AC", 14)]
    [InlineData("3C,4C", 7)]
    [InlineData("TC,TD,TH,TS", 100)]
    [InlineData("6H,7D,8S,9S", 100)]
    public void Deck_ConvertListOfCardsToCorrectScore(string listOfCards, int expected)
    {
        var deck = Deck.CreateDeck(listOfCards);

        var actual = deck.GetScore();
        
        Assert.Equal(expected, actual);
    }
    
    /// <summary>
    /// Scenario: Jokers<br/><br/>
    /// GIVEN I have started the Card Game application<br/>
    /// WHEN I enter a 'listOfCards' containing one or two jokers<br/>
    /// THEN the 'score' for the hand should be doubled and displayed on the user interface<br/>
    /// </summary>
    [Theory]
    [InlineData("JK", 0)]
    [InlineData("JK,JK", 0)]
    [InlineData("2C,JK", 4)]
    [InlineData("JK,2C,JK", 8)]
    [InlineData("TC,TD,JK,TH,TS", 200)]
    [InlineData("TC,TD,JK,TH,TS,JK", 400)]
    public void Deck_JokersToCorrectScore(string listOfCards, int expected)
    {
        var deck = Deck.CreateDeck(listOfCards);

        var actual = deck.GetScore();
        
        Assert.Equal(expected, actual);
    }
    
    /// <summary>
    /// Scenario: Invalid Lists<br/><br/>
    /// GIVEN I have started the Card Game application<br/>
    /// WHEN I enter a 'listOfCards' that's invalid<br/>
    /// THEN an 'errorMessage' should be displayed on the user interface<br/>
    /// </summary>
    [Theory]
    [InlineData("1S", "Card not recognised")]
    [InlineData("2B", "Card not recognised")]
    [InlineData("2S,1S", "Card not recognised")]
    [InlineData("3H,3H", "Cards cannot be duplicated")]
    [InlineData("4D,5D,4D", "Cards cannot be duplicated")]
    [InlineData("JK,JK,JK", "A hand cannot contain more than two Jokers")]
    [InlineData("2S|3D", "Invalid input string")]
    public void CreateDeck_InvalidListCorrectErrorMessage(string listOfCards, string expected)
    {
        var exception = Assert.Throws<CardException>(() => Deck.CreateDeck(listOfCards));
        
        Assert.Equal(expected, exception.Message);
    }
}