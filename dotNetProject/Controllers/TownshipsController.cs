using dotNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownshipsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TownshipsController(AppDbContext context)
        {
            _context = context;
        }

        #region CRUD Operation

        [HttpGet]
        [EndpointSummary("Get all townships")]
        [EndpointDescription("Get all townships")]
        public async Task<IActionResult> GetTownships()
        {
            List<Township> townships = await _context.Townships.ToListAsync();
            return Ok(new DefaultResponseModel()
            {
                Success = true,
                Message = "Successfully fetched.",
                Data = new
                {
                    TotalTownships = townships.Count,
                    Townships = townships
                }
            });
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get township by id")]
        [EndpointDescription("Get a township by id")]
        public async Task<IActionResult> GetTownshipById(string id)
        {
            Township? township = await _context.Townships.FindAsync(id);
            return township == null
                ? NotFound(new DefaultResponseModel()
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "Township Not Found."
                })
                : Ok(new DefaultResponseModel()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = township,
                    Message = "Successfully fetched."
                });
        }

        [HttpGet("tspagination")]
        [EndpointSummary("Get all townships with pagination")]
        [EndpointDescription("Get all townships with pagination")]
        public async Task<IActionResult> GetTownshipsWithPagination(int page = 1, int pageSize = 10)
        {
            List<Township> townships = await _context.Townships.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(townships);
        }

        [HttpPost]
        [EndpointSummary("Create new township")]
        [EndpointDescription("Creating a new township")]
        public async Task<IActionResult> PostTownship([FromBody] Township township)
        {
            if (await _context.Townships.AnyAsync(x => x.TownshipId == township.TownshipId))
            {
                return BadRequest("Township Already Exist");
            }
            _context.Townships.Add(township);
            await _context.SaveChangesAsync();
            return Created("api/Townships", new DefaultResponseModel()
            {
                Success = true,
                StatusCode = StatusCodes.Status201Created,
                Data = township,
                Message = "Successfully saved."
            });
        }

        [HttpPut("{id}")]
        [EndpointSummary("Update township")]
        [EndpointDescription("Update a township")]
        public async Task<IActionResult> PutTownship(string id, [FromBody] Township township)
        {
            Township? townshipData = await _context.Townships.FindAsync(id);
            if (townshipData == null)
            {
                return NotFound(new DefaultResponseModel()
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "Township Not Found."
                });
            }
            townshipData.TownshipName = township.TownshipName;
            townshipData.Latitude = township.Latitude;
            _context.Townships.Update(townshipData);
            await _context.SaveChangesAsync();
            return Ok(new DefaultResponseModel()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Data = townshipData,
                Message = "Successfully updated."
            });
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete township")]
        [EndpointDescription("Delete a township")]
        public async Task<IActionResult> DeleteTownship(string id)
        {
            Township? township = await _context.Townships.FindAsync(id);
            if (township == null)
            {
                return NotFound(new DefaultResponseModel()
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "Township Not Found."
                });
            }
            _context.Townships.Remove(township);
            await _context.SaveChangesAsync();
            return Ok(new DefaultResponseModel()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Data = township,
                Message = "Successfully deleted."
            });
        }

        #endregion
    }
}