﻿namespace OOP_ICT.Models;

public class Dealer
{
    private CardDeck _cardDeck;
    public Dealer(CardDeck cardDeck)
    {
        _cardDeck = cardDeck;
    }

    public void ShuffleTheDeck()
    {
        _perfectShuffle();
    }

    public Card DealCard()
    {
        var card = _cardDeck.Cards[0];
        _cardDeck.Cards.RemoveAt(0);
        return card;
    }
    
    private void _perfectShuffle()
    {
        var half = _cardDeck.Cards.Count / 2;
        var shuffledCards = new CardDeck().Cards;
        for (var i = 0; i < half; i++)
        {
            shuffledCards[i * 2] = _cardDeck.Cards[i + half];
            shuffledCards[i * 2 + 1] = _cardDeck.Cards[i];
        }

        _cardDeck.Cards = shuffledCards;
    }
}