using CardGameChallenge.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardGameChallenge.App;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string _cardsAsString = string.Empty;
    [ObservableProperty] private int _score;
    [ObservableProperty] private string? _errorMessage;

    [RelayCommand]
    private void CalculateScore()
    {
        Score = 0;
        ErrorMessage = null;
        
        try
        {
            var deck = Deck.CreateDeck(CardsAsString);
            Score = deck.GetScore();
        }
        catch (CardException cardEx)
        {
            ErrorMessage = cardEx.Message;
        }
    }
}