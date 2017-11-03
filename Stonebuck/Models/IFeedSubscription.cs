namespace Stonebuck.Models
{
    public interface IFeedSubscription : ISubscribable
    {
        string Url { get; }
    }
}