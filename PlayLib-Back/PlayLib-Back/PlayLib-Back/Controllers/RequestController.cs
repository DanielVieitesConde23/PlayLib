using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;
using PlayLib.Data.DTOs;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController(IRequestService requestService) : ControllerBase {

    private readonly IRequestService _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));

    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IActionResult> GetForUser(Guid userId)
    {
        var requests = await _requestService.GetRequestsForUser(userId);
        if (requests == null)
            return NotFound(new { Success = false });
        else
            return Ok(requests);
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllRequests()
    {
        var requests = await _requestService.GetAllRequests();
        if (requests == null)
            return NotFound(new { Success = false });
        else
            return Ok(requests);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RequestDTO request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _requestService.CreateRequest(request))
            return BadRequest(new { Success = false });
        else
            return Ok(new { Success = true });
    }

    [HttpPost]
    [Route("approve/{id}")]
    public async Task<IActionResult> Approve(Guid requestId)
    {
        if (!await _requestService.ApproveRequest(requestId))
            return NotFound(new { Success = false });
        else
            return Ok(new { Success = true });
    }

    [HttpPost]
    [Route("deny/{id}")]
    public async Task<IActionResult> Deny(Guid requestId)
    {
        if (!await _requestService.DenyRequest(requestId))
            return NotFound(new { Success = false });
        else
            return Ok(new { Success = true });
    }

    [HttpDelete]
    [Route("remove/{id}")]
    public async Task<IActionResult> Remove(Guid requestId)
    {
        if (!await _requestService.RemoveRequest(requestId))
            return NotFound(new { Success = false });
        else
            return Ok(new { Success = true });
    }
}