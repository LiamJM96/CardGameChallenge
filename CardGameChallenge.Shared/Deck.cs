using System.Text.RegularExpressions;

namespace CardGameChallenge.Shared;

public partial class Deck
{
    [GeneratedRegex("^[0-9A-Z]{2}(,[0-9A-Z]{2})*$")]
    private static partial Regex ValidCommaDelimitedStringRegex();
    
    private List<Card> _listOfCards = new();

    public static Deck CreateDeck(string listOfCardsAsString)
    {
        // String should be a valid comma-delimited string and only allow 2 characters per delimiter
        if (ValidCommaDelimitedStringRegex().IsMatch(listOfCardsAsString) is false)
            throw new CardException("Invalid input string");

        var arrayOfCards = listOfCardsAsString.Split(",");

        var listOfCards = new List<Card>();

        foreach (var combination in arrayOfCards)
        {
            if (combination.Equals("JK"))
            {
                listOfCards.Add(new Card(combination, 0, true));
                
                if (listOfCards.Count(x => x.IsJoker) > 2)
                    throw new CardException("A hand cannot contain more than two Jokers");
                
                continue;
            }

            // Don't need to worry about checking string length as it's validated by above regex
            var cardValue = combination[0] switch
            {
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                'T' => 10,
                'J' => 11,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => throw new CardException("Card not recognised")
            };

            var suitValue = combination[1] switch
            {
                'C' => 1,
                'D' => 2,
                'H' => 3,
                'S' => 4,
                _ => throw new CardException("Card not recognised")
            };

            if (listOfCards.Exists(x => x.Code == combination))
                throw new CardException("Cards cannot be duplicated");

            var value = cardValue * suitValue;
            
            listOfCards.Add(new Card(combination, value, false));
        }

        return new Deck() { _listOfCards = listOfCards };
    }

    public int GetScore()
    {
        var totalCardScore = _listOfCards
            .Where(x => x.IsJoker is false)
            .Sum(x => x.Value);

        var numberOfJokers = _listOfCards.Count(x => x.IsJoker);

        return numberOfJokers switch
        {
            0 => totalCardScore,
            1 => totalCardScore * 2,
            2 => totalCardScore * 4,
            _ => throw new CardException("A hand cannot contain more than two Jokers")
        };
    }
}