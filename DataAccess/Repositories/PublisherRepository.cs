using AutoMapper;
using DataAccess.Daos;
using DataAccess.IRepositories;
using Entities.Dtos;
using Entities.Entity;
using Entities.RequestModels;

namespace DataAccess.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private IMapper _mapper;

    public PublisherRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public List<PublisherDto> GetPublishers()
    {
        return _mapper.Map<List<PublisherDto>>(PublisherDao.GetPublishers());
    }

    public PublisherDto? GetPublisherById(int id)
    {
        return _mapper.Map<PublisherDto>(PublisherDao.GetPublisherById(id));
    }

    public PublisherDto AddPublisher(AddPublisherRequest request)
    {
        var publisher = _mapper.Map<Publisher>(request);
        return _mapper.Map<PublisherDto>(PublisherDao.AddPublisher(publisher));
    }

    public PublisherDto UpdatePublisher(PublisherDto publisherDto)
    {
        var publisher = _mapper.Map<Publisher>(publisherDto);
        return _mapper.Map<PublisherDto>(PublisherDao.UpdatePublisher(publisher));
    }

    public void DeletePublisher(int id)
    {
        PublisherDao.DeletePublisher(id);
    }
}