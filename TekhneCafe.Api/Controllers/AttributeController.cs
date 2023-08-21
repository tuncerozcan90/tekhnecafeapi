using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Attribute;
using TekhneCafe.Core.Exceptions.Attribute;

namespace TekhneCafe.Api.Controllers
{
    /// <summary>
    /// Controller to manage attributes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        /// <summary>
        /// Creates a new attribute.
        /// </summary>
        /// <param name="attributeAddDto">The data for the new attribute.</param>
        /// <returns>A response indicating the result of the attribute creation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> CreateAttribute(AttributeAddDto attributeAddDto)
        {
            await _attributeService.CreateAttributeAsync(attributeAddDto);
            return Ok("Attribute created successfully.");
        }

        /// <summary>
        /// Retrieves a list of all attributes.
        /// </summary>
        /// <returns>A list of attributes.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<AttributeListDto>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllAttributes()
        {
            var attributes = _attributeService.GetAllAttributeAsync();
            return Ok(attributes);
        }

        /// <summary>
        /// Retrieves an attribute by its ID.
        /// </summary>
        /// <param name="id">The ID of the attribute.</param>
        /// <returns>The attribute with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetAttributeById(string id)
        {
            var attribute = await _attributeService.GetAttributeByIdAsync(id);
            return Ok(attribute);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(string id)
        {
           
              await _attributeService.DeleteAttributeAsync(id);
            return(Ok("Attribute deleted successfully."));

        }
        [HttpPut]
        public async Task<IActionResult> UpdateAttribute([FromBody] AttributeUpdateDto attributeUpdateDto)
        {
           
                await _attributeService.UpdateAttributeAsync(attributeUpdateDto);
                return Ok("Attribute updated successfully.");
        }


    }
}