using ZooplaMatch.Domain.Models.Enums;

namespace ZooplaMatch.Domain.Models
{
    public record Card (Suit Suit, Rank Rank)
    {
        public string GetCard => $"{Enum.GetName(typeof(Rank), Rank)} of {Enum.GetName(typeof(Suit), Suit)}";
    }
}
