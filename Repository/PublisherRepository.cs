using Microsoft.EntityFrameworkCore;
using ReviewApp.Data;
using ReviewApp.Interfaces;
using ReviewApp.Models;
using System.Net.Mime;

namespace ReviewApp.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DataContext _context;

        public PublisherRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePublisher(Publisher publisher)
        {
            _context.Add(publisher);
            return Save();
        }

        public bool UpdatePublisher(Publisher publisher)
        {
            _context.Update(publisher);
            return Save();
        }

        public bool DeletePublisher(Publisher publisher)
        {
            _context.Remove(publisher);
            return Save();
        }

        public ICollection<Game> GetGamesByPublisher(int publisherId)
        {
            return _context.Publishers.Where(p => p.Id == publisherId).SelectMany(p => p.Games).Include(g => g.Publisher).ToList();
        }

        public Publisher GetPublisher(int publisherId)
        {
            return _context.Publishers.Where(p => p.Id == publisherId).Include(e => e.Games).FirstOrDefault();
        }

        public ICollection<Publisher> GetPublishers()
        {
            return _context.Publishers.OrderBy(p => p.Id).ToList();
        }

        public bool PublisherExists(int publisherId)
        {
            return _context.Publishers.Any(p => p.Id == publisherId);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
