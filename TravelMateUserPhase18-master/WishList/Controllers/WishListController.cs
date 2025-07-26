using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishListDll.Models;
using WishListWebApi.Services;

namespace WishListWebApi.Controllers
{
    [Route("api/wishlist")]
    [ApiController]
    [EnableCors("cors")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListDataService _wishListDataService;

        public WishListController(IWishListDataService wishListDataService)
        {
            _wishListDataService = wishListDataService;
        }

        [HttpGet("AllWishListItems")]
        public async Task<ActionResult<List<WishListTable>>> GetAllWishListItems(int currentUserId)
        {
            try
            {
                var wishListItems = await _wishListDataService.GetAllWishListItems(currentUserId);
                return Ok(wishListItems);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WishListTable>> GetWishListItemById(int id, int currentUserId)
        {
            try
            {
                var wishListItem = await _wishListDataService.GetWishListItemById(id, currentUserId);
                return Ok(wishListItem);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddWishListItem([FromBody] WishListTable wishList, int currentUserId)
        {
            try
            {
                await _wishListDataService.AddWishListItem(wishList, currentUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishListItem(int id, int currentUserId)
        {
            try
            {
                await _wishListDataService.DeleteWishListItemById(id, currentUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
