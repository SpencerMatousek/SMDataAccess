using Microsoft.AspNetCore.Mvc;
using SMDataAccess.Data.IData;
using SMDataAccess.Models;

namespace SMAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class PartController : Controller
{
    private readonly IPartRepository _partRepository;
    public PartController(IPartRepository partRepository)
    {
        _partRepository = partRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<TbPart>> GetAsync()
    {
        var response = await _partRepository.GetPaginatedAsync(1, 30);
        if (!response.IsSuccessful)
            throw response.SqlException;
        else if (response.Data != null)
            return response.Data;
        else
            return Enumerable.Empty<TbPart>();
    }
}
