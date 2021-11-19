using HailStorm.Core.Exceptions;
using HailStorm.Core.Interfaces;
using HailStorm.Core.Models;
using HailStormPredictionSystem.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HailStormPredictionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HailItemsController : ControllerBase
    {
        private readonly IHailItemRepository _repo;
        public HailItemsController(IHailItemRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Creating a new HailItem Item
        /// </summary>
        /// <param name="newTodoItem"> JSON New HailItem document</param>
        /// <returns>Returns the new HailItem Id </returns>
        /// <returns>Returns 201 Created success</returns>
        /// <returns>Returns 400 Bad Request error</returns>
        /// <returns>Returns 500 Internal Server Error </returns>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> CreateItem([FromBody] HailItem newTodoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _repo.AddAsync(newTodoItem);

            return Ok(response);
        }

        /// <summary>
        /// Creating a new HailItem Item
        /// </summary>
        /// <param name="filePath"> JSON New HailItem document</param>
        /// <returns>Returns 201 Created success</returns>
        /// <returns>Returns 400 Bad Request error</returns>
        /// <returns>Returns 500 Internal Server Error </returns>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost] //("{filePath},{fileName}")
        [Route("{filePath}")]
        public async Task<ActionResult> CreateItemsByFilePath(string filePath,string filename)
        {
            var hailItems = ExcelToJson.ExcelToJsonConverter(filePath);
           
            foreach (var hailItem in hailItems)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _repo.AddAsync(hailItem);
            }
            return Ok();
        }

        /// <summary>
        /// Retrieving a TodoItem using its TodoItem Id
        /// </summary>
        /// <remarks>
        /// Retrieves a TodoItem using its TodoItem Id
        /// </remarks>
        /// <param name="toDoId">The Id of the TodoItem item to be retrieved</param>
        /// <returns>Returns the full TodoItem document </returns>
        /// <returns>Returns 200 OK success </returns>
        /// <returns>Returns 404 Not Found error</returns>
        /// <returns>Returns 500 Internal Server Error </returns>
        [HttpGet("{hailItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HailItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetItem(string hailItemId)
        {
            try
            {
                var toDo = await _repo.GetByIdAsync(hailItemId);
                return Ok(toDo);
            }
            catch (EntityNotFoundException)
            {
                return NotFound(hailItemId);
            }
        }
    }
}
