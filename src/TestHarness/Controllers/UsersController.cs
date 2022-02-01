using AutoMapper;
using AutoMapper.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using TestHarness.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace TestHarness.Controllers;

[ApiController]
[ODataAttributeRouting]
[Route("sample/v{version:apiVersion}")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private SampleDataDbContext DbContext { get; }
    private IMapper Mapper { get; }

    public UsersController(SampleDataDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    [EnableQuery]
    [ApiVersion("1")]
    [HttpGet("Users")]
    [ProducesResponseType(typeof(IEnumerable<Model.V1.User>), Status200OK)]
    public async Task<IActionResult> GetV1(ODataQueryOptions<Model.V1.User> queryOptions)
    {
        var query = DbContext.Users.AsNoTracking();

        var selects = queryOptions.SelectExpand.GetSelects();

        if (!selects.Any()
            || selects.Contains(nameof(Model.V1.User.FirstName))
            || selects.Contains(nameof(Model.V1.User.LastName)))
        {
            query = query.Include(x => x.Identity);
        }

        return Ok(await query.GetAsync(Mapper, queryOptions, (QuerySettings) null));
    }

    [EnableQuery]
    [ApiVersion("2")]
    [HttpGet("Users")]
    [ProducesResponseType(typeof(IEnumerable<Model.V2.User>), Status200OK)]
    public async Task<IActionResult> GetV1(ODataQueryOptions<Model.V2.User> queryOptions)
    {
        var query = DbContext.Users.AsNoTracking();

        var selects = queryOptions.SelectExpand.GetSelects();

        if (!selects.Any()
            || selects.Contains(nameof(Model.V2.User.GivenName), StringComparer.OrdinalIgnoreCase)
            || selects.Contains(nameof(Model.V2.User.Surname), StringComparer.OrdinalIgnoreCase))
        {
            query = query.Include(x => x.Identity);
        }

        return Ok(await query.GetAsync(Mapper, queryOptions, (QuerySettings)null));
    }

    [EnableQuery]
    [ApiVersion("3")]
    [HttpGet("Users")]
    [ProducesResponseType(typeof(IEnumerable<Model.V3.User>), Status200OK)]
    public async Task<IActionResult> GetV3(ODataQueryOptions<Model.V3.User> queryOptions)
    {
        var query = DbContext.Users.AsNoTracking();

        var selects = queryOptions.SelectExpand.GetSelects();

        if (selects.Contains(nameof(Model.V3.User.Roles), StringComparer.OrdinalIgnoreCase))
        {
            query = query.Include(x => x.Roles);
        }

        return Ok(await query.GetAsync(Mapper, queryOptions, (QuerySettings)null));
    }
        
}