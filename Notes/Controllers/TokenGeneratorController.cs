using Microsoft.AspNetCore.Mvc;

using Notes.Data.Models;
using Notes.Data.Services;
using Notes.Extensions;
using Notes.RestResults;

using System.Net;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenGeneratorController : NotesApiController
    {
        NoteService _noteService;
        public TokenGeneratorController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("generate")]
        public async Task<string> GenerateToken()
        {
            try
            {
                var data = await _noteService.GetAllAsync();
                var token = Guid.NewGuid().ToString();

                while (data.Any(x => x.AuthorToken == token))
                {
                    token = Guid.NewGuid().ToString();
                }

                return new SimpleResult<string>(HttpStatusCode.OK, token).ToJson();
            }
            catch (Exception exc)
            {
                return new ErrorResult(exc).ToJson();
            }
        }
    }
}
