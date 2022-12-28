using AutoMapper;

namespace ImaginaryRealEstate.Services;

public class UserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, ILogger<UserService> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }
    
    
    
    
}