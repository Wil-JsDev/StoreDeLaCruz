using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;
using StoreDeLaCruz.Core.Aplication.Features.Notes.Commands.CreateNotes;
using StoreDeLaCruz.Core.Aplication.Features.Notes.Commands.DeleteNotes;
using StoreDeLaCruz.Core.Aplication.Features.Notes.Commands.UpdateNotes;
using StoreDeLaCruz.Core.Aplication.Features.Notes.Queries.GetAllNotes;
using StoreDeLaCruz.Core.Aplication.Features.Notes.Queries.GetNoteById;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;

namespace StoreDeLaCruz.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NoteController : BaseController
    {     
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
             Ok(await Mediator.Send(new GetAllNotesQuery()));

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetNotesByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateNoteCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            
        }

        //Try() Catch es solo para devolver StatusCode500
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateNoteCommand command)
        {
            try
            {
                if (id != command.Id)
                {
                    return BadRequest();
                }

                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<NotaDTos>> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteNoteByIdCommand { Id = id });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

    }
}
