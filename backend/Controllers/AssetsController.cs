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
        public async Task<IActionResult> GetAssetsByPage([FromQuery] AssetFilters filters)
        {
            var (pageAssets, assetsTotalCount) = await _assetsRepository.GetAssetsByPageAsync(filters);

            var pageAssetsDto = _mapper.Map<IEnumerable<AssetDto>>(pageAssets); 

            return Ok(new
            {
                Items = pageAssetsDto,
                assetsTotalCount
            });
        }

        [Authorize]
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
