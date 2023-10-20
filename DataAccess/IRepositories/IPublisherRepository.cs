using Entities.Dtos;
using Entities.Entity;
using Entities.RequestModels;

namespace DataAccess.IRepositories;

public interface IPublisherRepository
{
    public List<PublisherDto> GetPublishers();
    public PublisherDto? GetPublisherById(int id);
    public PublisherDto AddPublisher(AddPublisherRequest request);
    public PublisherDto UpdatePublisher(PublisherDto publisherDto);
    public void DeletePublisher(int id);
}