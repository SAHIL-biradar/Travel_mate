using HotelRatingwebapi.DataServices;
using HotelRatingDll.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelRatingwebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("cors")]
    public class HotelRatingController : Controller
    {
        private readonly IHotelRatingDataService _hotelRatingDataService;

        public HotelRatingController(IHotelRatingDataService hotelRatingDataService)
        {
            _hotelRatingDataService = hotelRatingDataService;
        }

        // POST: api/hotelrating
        [HttpPost]
        public async Task<IActionResult> AddHotelRating([FromBody] HotelRating hotelRating, [FromQuery] int currentUserId)
        {
            try
            {
                await _hotelRatingDataService.AddHotelRatingAsync(hotelRating, currentUserId);
                return Ok(new { Message = "Hotel rating added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/hotelrating
        [HttpPut]
        public async Task<IActionResult> UpdateHotelRating([FromBody] HotelRating hotelRating, [FromQuery] int currentUserId)
        {
            try
            {
                await _hotelRatingDataService.UpdateHotelRatingAsync(hotelRating, currentUserId);
                return Ok(new { Message = "Hotel rating updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/hotelrating/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelRating(int id, [FromQuery] int currentUserId)
        {
            try
            {
                await _hotelRatingDataService.DeleteHotelRatingAsync(id, currentUserId);
                return Ok(new { Message = "Hotel rating deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/hotelrating/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelRatingById(int id, [FromQuery] int currentUserId)
        {
            try
            {
                var rating = await _hotelRatingDataService.GetHotelRatingByIdAsync(id, currentUserId);
                return Ok(rating);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // GET: api/hotelrating
        [HttpGet]
        public async Task<IActionResult> GetAllHotelRatings([FromQuery] int currentUserId)
        {
            try
            {
                var ratings = await _hotelRatingDataService.GetAllHotelRatingsAsync(currentUserId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST: api/hotelrating/review
        [HttpPost("review")]
        public async Task<IActionResult> AddReview([FromQuery] int ratingId, [FromQuery] string reviewText, [FromQuery] int currentUserId)
        {
            try
            {
                await _hotelRatingDataService.AddReviewAsync(ratingId, reviewText, currentUserId);
                return Ok(new { Message = "Review added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/hotelrating/review
        [HttpPut("review")]
        public async Task<IActionResult> UpdateReview([FromQuery] int ratingId, [FromQuery] string reviewText, [FromQuery] int currentUserId)
        {
            try
            {
                await _hotelRatingDataService.UpdateReviewAsync(ratingId, reviewText, currentUserId);
                return Ok(new { Message = "Review updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/hotelrating/review
        [HttpDelete("review")]
        public async Task<IActionResult> DeleteReview([FromQuery] int ratingId, [FromQuery] int currentUserId)
        {
            try
            {
                await _hotelRatingDataService.DeleteReviewAsync(ratingId, currentUserId);
                return Ok(new { Message = "Review deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
