using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Notes.Data.Models;
using Notes.Data.Services;
using Notes.Extensions;
using Notes.RestResults;

using System.Net;

namespace Notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : NotesApiController
    {
        NoteService _noteService;
        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("all")]
        public async Task<string> GetNotes()
        {
            try
            {
                var token = Request.Headers.GetUserToken();
                var data = await _noteService.FindAsync(x => x.AuthorToken == token);

                return new ListResult<Note>(HttpStatusCode.OK, data).ToJson();
            }
            catch (UnauthorizedException exc)
            {
                return new ErrorResult(HttpStatusCode.Unauthorized, exc).ToJson();
            }
            catch (Exception exc)
            {
                return new ErrorResult(exc).ToJson();
            }
        }

        [HttpGet("{id}")]
        public async Task<string> GetNote(int id)
        {
            try
            {
                var token = Request.Headers.GetUserToken();
                var data = await _noteService.FindAsync(x => x.Id == id);
                var item = data.FirstOrDefault();

                if (item.AuthorToken != token)
                {
                    throw new AccessRestrictionException();
                }

                return new SimpleResult<Note>(HttpStatusCode.OK, item).ToJson();
            }
            catch (UnauthorizedException exc)
            {
                return new ErrorResult(HttpStatusCode.Unauthorized, exc).ToJson();
            }
            catch (Exception exc)
            {
                return new ErrorResult(exc).ToJson();
            }
        }

        [HttpPut("create")]
        public async Task<string> CreateNote()
        {
            try
            {
                var token = Request.Headers.GetUserToken();

                var title = Request.Form["Title"].ToString();
                var content = Request.Form["Content"].ToString();

                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
                {
                    throw new Exception("Invalid request");
                }

                var item = new Note
                {
                    Title = title,
                    Content = content,
                    AuthorToken = token
                };
                item = await _noteService.CreateAsync(item);

                return new SimpleResult<Note>(HttpStatusCode.OK, item).ToJson();
            }
            catch (UnauthorizedException exc)
            {
                return new ErrorResult(HttpStatusCode.Unauthorized, exc).ToJson();
            }
            catch (Exception exc)
            {
                return new ErrorResult(exc).ToJson();
            }
        }

        [HttpPost("update/{id}")]
        public async Task<string> UpdateNote(int id)
        {
            try
            {
                var token = Request.Headers.GetUserToken();

                var data = await _noteService.FindAsync(x => x.Id == id);
                var item = data.FirstOrDefault();

                if (item == null)
                {
                    throw new Exception("Note is not found");
                }

                if (item.AuthorToken != token)
                {
                    throw new AccessRestrictionException();
                }

                var title = Request.Form["Title"].ToString();
                var content = Request.Form["Content"].ToString();

                item.Title = title ?? item.Title;
                item.Content = content ?? item.Content;

                await _noteService.UpdateAsync(item);

                return new SimpleResult<Note>(HttpStatusCode.OK, item).ToJson();
            }
            catch (UnauthorizedException exc)
            {
                return new ErrorResult(HttpStatusCode.Unauthorized, exc).ToJson();
            }
            catch (Exception exc)
            {
                return new ErrorResult(exc).ToJson();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<string> DeleteNote(int id)
        {
            try
            {
                var token = Request.Headers.GetUserToken();

                var data = await _noteService.FindAsync(x => x.Id == id);
                var item = data.FirstOrDefault();

                if (item == null)
                {
                    throw new Exception("Note is not found");
                }

                if (item.AuthorToken != token)
                {
                    throw new AccessRestrictionException();
                }

                await _noteService.DeleteAsync(item);

                return new SimpleResult<Object>(HttpStatusCode.OK, new
                {
                    success = "true"
                }).ToJson();
            }
            catch (UnauthorizedException exc)
            {
                return new ErrorResult(HttpStatusCode.Unauthorized, exc).ToJson();
            }
            catch (Exception exc)
            {
                return new ErrorResult(exc).ToJson();
            }
        }
    }
}
