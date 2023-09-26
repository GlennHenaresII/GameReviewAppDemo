using ReviewApp.Models;

namespace ReviewApp.Interfaces
{
    public interface IPublisherRepository
    {
        ICollection<Publisher> GetPublishers();
        Publisher GetPublisher (int publisherId);
        bool PublisherExists(int publisherId);
        ICollection<Game> GetGamesByPublisher(int publisherId);
        bool CreatePublisher(Publisher publisher);
        bool UpdatePublisher(Publisher publisher);
        bool DeletePublisher(Publisher publisher);
        bool Save();
    }
}
