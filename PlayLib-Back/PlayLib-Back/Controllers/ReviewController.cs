using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;
using PlayLib.Data.DTOs;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase {
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService) {
        _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
    }

    [HttpGet]
    [Route("videogame/{id}")]
    public async Task<IActionResult> GetForVideogame(Guid id) {
        var reviews = await _reviewService.GetReviewsForVideogame(id);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewDTO review) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ok = await _reviewService.CreateReview(review);
        if (!ok) return BadRequest(new { Success = false });
        return Ok(new { Success = true });
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
        var ok = await _reviewService.DeleteReview(id);
        if (!ok) return NotFound(new { Success = false });
        return Ok(new { Success = true });
    }
}
