using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Images", file.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { path = "/images/" + file.FileName });
    }
}