using Microsoft.AspNetCore.Mvc;
using RestAPI_Mashup.Models;
using RestAPI_Mashup.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestAPI_Mashup.Controllers
{
    // testa: 5b11f4ce-a62d-471e-81fc-a69a8278c7da

    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {

        [HttpGet("{mbid}")]
        public async Task<IActionResult> GetArtistDetails(string mbid)
        {
            //ApiHelper är en basklass för att använda en HTTPClient
            ApiHelper.InitializeClient();
            var mbProcessor = new MBrainzProcessor(); //MusicBrainz api
            var artistResponse = await mbProcessor.LoadArtistAsync(mbid);
            if (artistResponse == null)
            {
                return BadRequest("Ingen artist hittad");
            }
            return Ok(artistResponse); //OK 200
        }

        // POST api/<ArtistController> 
        // Nedan är metoder för Post/Put/Delete OM databas vill användas senare för spara data
        [HttpPost]
        public async Task<IActionResult> AddArtist(AddArtistRequest addArtistRequest)
        {
            //var artist = new Artist()
            //{
            //    Id = Guid.NewGuid(),
            //    mbid = addArtistRequest.mbid,
            //    albums = addArtistRequest.albums,
            //    description = addArtistRequest.description,
            //};

            //await _dbContext.Artists.AddAsync(artist);
            //await _dbContext.SaveChangesAsync();

            return Ok();
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateArtist([FromRoute] Guid id, UpdateArtistRequest updateArtistRequest)
        {
            //var artist = await _dbContext.Artists.FindAsync(id);
            //if (artist != null)
            //{
            //    artist.mbid = updateArtistRequest.mbid;
            //    artist.albums = updateArtistRequest.albums;
            //    artist.description = updateArtistRequest.description;

            //    await _dbContext.SaveChangesAsync();
            //    return Ok(artist);
            //}

            return Ok();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteArtist([FromRoute] Guid id)
        {
            //var artist = await _dbContext.Artists.FindAsync(id);
            //if (artist != null)
            //{
            //    _dbContext.Remove(artist);
            //   await _dbContext.SaveChangesAsync();
            //    return Ok(artist); // skickar tbx artist ifall ngt mer skall göras
            //}

            return NotFound();
        }

    }
}
