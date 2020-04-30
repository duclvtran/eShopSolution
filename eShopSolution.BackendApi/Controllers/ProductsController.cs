using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        private readonly IStorageService _storageService;

        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService, IStorageService storageService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
            _storageService = storageService;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok();
        }

        // /product/1
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find Product");

            return Ok(product);
        }

        // /products?pageindex=1&pagesize=10&category=1
        [HttpGet("languageId")]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(languageId, request);
            return Ok(products);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetByCategoryId([FromQuery]GetPublicProductPagingRequest request)
        //{
        //    var products = await _publicProductService.GetAllByCategoryId(request);
        //    return Ok(products);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
                return BadRequest("Product not found");

            var product = _manageProductService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _manageProductService.Update(request);
            if (result == 0)
                return BadRequest("Product not found");
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _manageProductService.Delete(productId);
            if (result == 0)
                return BadRequest("Product not found");
            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var result = await _manageProductService.UpdatePrice(productId, newPrice);
            if (result)
                return Ok();
            return BadRequest("Product not found");
        }

        [HttpPatch("{productId}/{addedQuantity}")]
        public async Task<IActionResult> UpdateStock(int productId, int addedQuantity)
        {
            var result = await _manageProductService.UpdateStock(productId, addedQuantity);
            if (result)
                return Ok();
            return BadRequest("Product not found");
        }

        //Image Management
        // /product/1
        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find Product");
            return Ok(image);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imageId = await _manageProductService.CreateImage(productId, request);

            if (imageId == 0)
                return BadRequest();

            var image = _manageProductService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);
            if (image == null)
                return BadRequest();

            var result = await _manageProductService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();
            else
                await _storageService.DeleteFileAsync(image.ImagePath);

            return Ok();
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _manageProductService.UpdateImage(imageId, request);

            if (result == 0)
                return BadRequest("Image not found");
            return Ok();
        }

        [HttpGet("{productId}/images")]
        public async Task<IActionResult> GetListImages(int productId)
        {
            var images = await _manageProductService.GetListImages(productId);
            if (images == null)
                return BadRequest("Cannot find Images");

            return Ok(images);
        }
    }
}