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
public class OrganizationsController : ControllerBase
{
    private SampleDataDbContext DbContext { get; }
    private IMapper Mapper { get; }

    public OrganizationsController(SampleDataDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    [EnableQuery]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [HttpGet("Organizations")]
    [ProducesResponseType(typeof(IEnumerable<Model.V1.Organization>), Status200OK)]
    public async Task<IActionResult> GetV1(ODataQueryOptions<Model.V1.Organization> queryOptions) =>
        Ok(await DbContext.Organizations
            .AsNoTracking()
            .GetAsync(Mapper, queryOptions, (QuerySettings) null));

    [EnableQuery]
    [ApiVersion("3")]
    [HttpGet("Organizations")]
    [ProducesResponseType(typeof(IEnumerable<Model.V3.Organization>), Status200OK)]
    public async Task<IActionResult> GetV3(ODataQueryOptions<Model.V3.Organization> queryOptions) =>
        Ok(await DbContext.Organizations
            .AsNoTracking()
            .GetAsync(Mapper, queryOptions, (QuerySettings)null));
}