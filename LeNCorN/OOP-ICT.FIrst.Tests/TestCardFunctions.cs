﻿using OOP_ICT.Exceptions;
using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestCardFunctions
{
    [Fact]
    public void IsCardDeckLengthEqual52_InputIsListOfCardsCount_ReturnTrue()
    {
        var cardDeck = new CardDeck();
        Assert.Equal(52, cardDeck.Cards.Count);
    }
}