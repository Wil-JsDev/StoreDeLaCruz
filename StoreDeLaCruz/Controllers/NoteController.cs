using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;

namespace StoreDeLaCruz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private ICommonService<NotaDTos, NotaInsertDTos, NotaUpdateDTos> _serviceNota;
        private IValidator<NotaInsertDTos> _validationInsert;
        private IValidator<NotaUpdateDTos> _validationUpdate;

        public NoteController(ICommonService<NotaDTos, NotaInsertDTos, NotaUpdateDTos> commonService,
             IValidator<NotaInsertDTos> validationInsert, IValidator<NotaUpdateDTos> validationUpdate )
        {
            _serviceNota = commonService;
            _validationInsert = validationInsert;
            _validationUpdate = validationUpdate;
        }

        [HttpGet]
        public async Task<IEnumerable<NotaDTos>> GetAll () =>
            await _serviceNota.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<NotaDTos>> GetById(int id)
        {
            var folderId = await _serviceNota.GetById(id);

            return folderId == null ? NotFound() : Ok(folderId);
        }


        [HttpPost]
        public async Task<ActionResult<NotaDTos>> Add(NotaInsertDTos notaInsertDTos)
        {

            var validationInsert = await _validationInsert.ValidateAsync(notaInsertDTos);

            if (!validationInsert.IsValid)
            {
                return BadRequest(validationInsert.Errors);
            }

            var folder = await _serviceNota.Add(notaInsertDTos);

            return folder == null ? NotFound() : Ok(folder);
        }

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

        [HttpDelete("{id}")]
        public async Task<ActionResult<NotaDTos>> Delete(int id)
        {
            var folderDelete = await _serviceNota.Delete(id);

            return folderDelete == null ? NotFound() : Ok(folderDelete);
        }

    }
}
