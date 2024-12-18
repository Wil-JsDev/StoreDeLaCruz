﻿using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;

namespace StoreDeLaCruz.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Basic")]
    public class NoteController : BaseController
    {
        private INoteService<NotaDTos, NotaInsertDTos, NotaUpdateDTos> _serviceNota;
        private IValidator<NotaInsertDTos> _validationInsert;
        private IValidator<NotaUpdateDTos> _validationUpdate;

        public NoteController(INoteService<NotaDTos, NotaInsertDTos, NotaUpdateDTos> serviceNota,
             IValidator<NotaInsertDTos> validationInsert, IValidator<NotaUpdateDTos> validationUpdate)
        {
            _serviceNota = serviceNota;
            _validationInsert = validationInsert;
            _validationUpdate = validationUpdate;
        }

        [HttpGet]
        public async Task<IEnumerable<NotaDTos>> GetAll() =>
            await _serviceNota.GetAll();

        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [HttpGet("{id}")]
        public async Task<ActionResult<NotaDTos>> GetById(int id)
        {
            var folderId = await _serviceNota.GetById(id);

            return folderId == null ? NotFound() : Ok(folderId);
        }


        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        [HttpPost]
        public async Task<ActionResult<NotaDTos>> Add(NotaInsertDTos notaInsertDTos)
        {

            var validationInsert = await _validationInsert.ValidateAsync(notaInsertDTos);

            if (!validationInsert.IsValid)
            {
                return BadRequest(validationInsert.Errors);
            }

            var note = await _serviceNota.Add(notaInsertDTos);

            return note == null ? NotFound() : CreatedAtAction(nameof(GetById), new { id = note.ID }, note);
        }

        //[HttpGet]
        //public async Task<ActionResult<NotaDTos>> Filter([FromQuery] string filter)
        //{
        //    var filterResult = await _serviceNota.Filter(filter);

        //    return filterResult == null ? NotFound() : Ok(filterResult);
        //}

        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        [HttpPut("{id}")]
        public async Task<ActionResult<NotaDTos>> Update(int id, NotaUpdateDTos notaUpdateD)
        {
            var validationUpdate = await _validationUpdate.ValidateAsync(notaUpdateD);

            if (!validationUpdate.IsValid)
            {
                BadRequest(validationUpdate.Errors);
            }

            var folderUpdate = await _serviceNota.Update(id, notaUpdateD);

            return folderUpdate == null ? NotFound() : Ok(folderUpdate);
        }


        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<NotaDTos>> Delete(int id)
        {
            var folderDelete = await _serviceNota.Delete(id);

            return folderDelete == null ? NotFound() : NoContent();
        }

    }
}
