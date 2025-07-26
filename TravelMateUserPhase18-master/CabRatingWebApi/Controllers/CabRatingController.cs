using CabRatingDll.Models;
using CabRatingWebApi.DataServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CabRatingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("cors")]
    public class CabRatingController : Controller
    {
        private readonly CabRatingDataServices _cabRatingDataServices;

        public CabRatingController(CabRatingDataServices cabRatingDataServices)
        {
            _cabRatingDataServices = cabRatingDataServices;
        }

        // POST: api/cabrating
        [HttpPost]
        public async Task<IActionResult> AddCabRating([FromBody] CabRating rating, [FromQuery] int currentUserId)
        {
            try
            {
                await _cabRatingDataServices.AddCabRatingAsync(rating, currentUserId);
                return Ok(new { Message = "Cab rating added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST: api/cabrating/review
        //[HttpPost("review")]
        //public async Task<IActionResult> AddReviewForCab([FromQuery] int cabId, [FromQuery] int currentUserId, [FromQuery] string review, [FromQuery] int rating)
        //{
        //    try
        //    {
        //        await _cabRatingDataServices.AddReviewForCabAsync(cabId, currentUserId, review, rating);
        //        return Ok(new { Message = "Review added successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = ex.Message });
        //    }
        //}

        // PUT: api/cabrating
        [HttpPut]
        public async Task<IActionResult> UpdateCabRating([FromBody] CabRating rating, [FromQuery] int currentUserId)
        {
            try
            {
                await _cabRatingDataServices.UpdateCabRatingAsync(rating, currentUserId);
                return Ok(new { Message = "Cab rating updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/cabrating/review
        //[HttpPut("review")]
        //public async Task<IActionResult> UpdateReviewForCab([FromQuery] int cabRatingId, [FromQuery] int currentUserId, [FromQuery] string review, [FromQuery] int rating)
        //{
        //    try
        //    {
        //        await _cabRatingDataServices.UpdateReviewForCabAsync(cabRatingId, currentUserId, review, rating);
        //        return Ok(new { Message = "Review updated successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = ex.Message });
        //    }
        //}

        // DELETE: api/cabrating/{id}
        [HttpDelete("{cabRatingId}")]
        public async Task<IActionResult> DeleteCabRating(int cabRatingId, [FromQuery] int currentUserId)
        {
            try
            {
                await _cabRatingDataServices.DeleteCabRatingAsync(cabRatingId, currentUserId);
                return Ok(new { Message = "Cab rating deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //// DELETE: api/cabrating/review/{id}
        //[HttpDelete("review/{cabRatingId}")]
        //public async Task<IActionResult> DeleteReviewForCab(int cabRatingId, [FromQuery] int currentUserId)
        //{
        //    try
        //    {
        //        await _cabRatingDataServices.DeleteReviewForCabAsync(cabRatingId, currentUserId);
        //        return Ok(new { Message = "Review deleted successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = ex.Message });
        //    }
        //}

        // GET: api/cabrating/{cabId}/ratings
        [HttpGet("{cabId}/ratings")]
        public async Task<IActionResult> GetRatingsForCab(int cabId)
        {
            try
            {
                var ratings = await _cabRatingDataServices.GetRatingsForCabAsync(cabId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        // GET: api/cabrating/{cabId}/totalrating
        [HttpGet("{cabId}/totalrating")]
        public async Task<IActionResult> GetTotalRatingsForCab(int cabId)
        {
            try
            {
                var totalRating = await _cabRatingDataServices.GetTotalRatingsForCabAsync(cabId);
                return Ok(totalRating);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
            [HttpGet("user/{userId}/ratings")]
            public async Task<IActionResult> GetRatingsByUser(int userId)
            {
                try
                {
                    var ratings = await _cabRatingDataServices.GetRatingsByUserAsync(userId);
                    return Ok(ratings);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }
        // GET: api/cabrating/{cabId}/user/{userId}
        
        [HttpGet("{cabId}/{userId}")]
        public async Task<IActionResult> GetCabRatingByCabIdAndUserId(int cabId, int userId)
        {
            try
            {
                var rating = await _cabRatingDataServices.GetCabRatingByCabIdAndUserIdAsync(cabId, userId);
                if (rating == null)
                {
                    return NotFound(new { Message = "No rating found for the specified cab and user." });
                }
                return Ok(rating);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}