using FluentAssertions;
using ZooplaMatch.Application.Managers;
using ZooplaMatch.Domain.Models;
using ZooplaMatch.Domain.Models.Enums;

namespace ZooplaMatch.Application.UnitTests.Managers
{
    public class GameRuleManagerTests
    {
        [Theory]
        [InlineData(Suit.Spades, Rank.Ace, Suit.Spades, Rank.Three)]
        [InlineData(Suit.Hearts, Rank.Queen, Suit.Hearts, Rank.King)]
        [InlineData(Suit.Diamonds, Rank.Queen, Suit.Diamonds, Rank.Queen)]
        public void When_MatchType_Is_Suit_And_Cards_Are_Of_SameSuit_Then_IsMatch_ShouldBeTrue(Suit card1suit, Rank card1rank, Suit card2suit, Rank card2rank)
        {
            var sut = new GameRuleManger(CardMatchType.Suit);

            var result = sut.IsAMatch((new Card(card1suit, card1rank), new Card(card2suit, card2rank)));

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Suit.Spades, Rank.Ace, Suit.Diamonds, Rank.Three)]
        [InlineData(Suit.Hearts, Rank.Queen, Suit.Clubs, Rank.Queen)]
        public void When_MatchType_Is_Suit_And_Cards_Are_Of_DifferentSuit_Then_IsMatch_ShouldBeFalse(Suit card1suit, Rank card1rank, Suit card2suit, Rank card2rank)
        {
            var sut = new GameRuleManger(CardMatchType.Suit);

            var result = sut.IsAMatch((new Card(card1suit, card1rank), new Card(card2suit, card2rank)));

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(Suit.Spades, Rank.Ace, Suit.Diamonds, Rank.Ace)]
        [InlineData(Suit.Clubs, Rank.Queen, Suit.Clubs, Rank.Queen)]
        public void When_MatchType_Is_Rank_And_Cards_Are_Of_SameRank_Then_IsMatch_ShouldBeTrue(Suit card1suit, Rank card1rank, Suit card2suit, Rank card2rank)
        {
            var sut = new GameRuleManger(CardMatchType.Rank);

            var result = sut.IsAMatch((new Card(card1suit, card1rank), new Card(card2suit, card2rank)));

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Suit.Spades, Rank.Ace, Suit.Diamonds, Rank.Three)]
        [InlineData(Suit.Hearts, Rank.Queen, Suit.Hearts, Rank.King)]
        public void When_MatchType_Is_Rank_And_Cards_Are_Of_DifferentRank_Then_IsMatch_ShouldBeFalse(Suit card1suit, Rank card1rank, Suit card2suit, Rank card2rank)
        {
            var sut = new GameRuleManger(CardMatchType.Rank);

            var result = sut.IsAMatch((new Card(card1suit, card1rank), new Card(card2suit, card2rank)));

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(Suit.Diamonds, Rank.Ace, Suit.Diamonds, Rank.Ace)]
        [InlineData(Suit.Clubs, Rank.Queen, Suit.Clubs, Rank.Queen)]
        public void When_MatchType_Is_SuitAndRank_And_Cards_Are_Of_SameSuitAndRank_Then_IsMatch_ShouldBeTrue(Suit card1suit, Rank card1rank, Suit card2suit, Rank card2rank)
        {
            var sut = new GameRuleManger(CardMatchType.SuitAndRank);

            var result = sut.IsAMatch((new Card(card1suit, card1rank), new Card(card2suit, card2rank)));

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Suit.Diamonds, Rank.Ace, Suit.Clubs, Rank.Two)]
        [InlineData(Suit.Hearts, Rank.Queen, Suit.Hearts, Rank.King)]
        [InlineData(Suit.Spades, Rank.Queen, Suit.Diamonds, Rank.Queen)]
        public void When_MatchType_Is_SuitAndRank_And_Cards_Are_Of_DifferentSuitAndRank_Then_IsMatch_ShouldBeFalse(Suit card1suit, Rank card1rank, Suit card2suit, Rank card2rank)
        {
            var sut = new GameRuleManger(CardMatchType.SuitAndRank);

            var result = sut.IsAMatch((new Card(card1suit, card1rank), new Card(card2suit, card2rank)));

            result.Should().BeFalse();
        }
    }
}
