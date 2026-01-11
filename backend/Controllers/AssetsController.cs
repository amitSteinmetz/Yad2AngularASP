using AutoMapper;
using backend.DTOs;
using backend.IRepositories;
using backend.Models.AssetModels;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        public AssetsController(IAssetsRepository assetRepository, IMapper mapper)
        {
            _assetsRepository = assetRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets([FromQuery] AssetFilters filters)
        {
            // אם המשתמש לא שלח מספר דף, נניח דף 1
            if (filters.PageNumber <= 0) filters.PageNumber = 1;

            var (assets, assetsTotalCount) = await _assetsRepository.GetAssetsAsync(filters);

            var assetsDto = _mapper.Map<IEnumerable<AssetDto>>(assets); // fix and understand

            // מחזירים גם את התוצאות וגם את הספירה הכוללת
            return Ok(new
            {
                Items = assetsDto,
                assetsTotalCount,
                PageSize = 10 // כדי שהקליינט ידע איך לחלק
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] AssetDto assetDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var asset = _mapper.Map<Asset>(assetDto);
            asset.PublisherId = userId;

            var createdAsset = await _assetsRepository.CreateAssetAsync(asset);

            //return CreatedAtAction(nameof(GetAssetById), new { id = createdAsset.Id }, createdAsset);
            return Created();
        }
    }
}
