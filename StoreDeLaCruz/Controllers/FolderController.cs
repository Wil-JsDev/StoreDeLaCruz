using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreDeLaCruz.Core.Aplication.DTOs.Folder;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;

namespace StoreDeLaCruz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FolderController : ControllerBase
    {

        private IFolderService<FolderDTos, FolderInsertDTos, FolderUpdate> _folderService;
        private IValidator<FolderInsertDTos> _validator;
        private IValidator<FolderUpdate> _validatorUpdate;

        public FolderController(IFolderService<FolderDTos, FolderInsertDTos, FolderUpdate> folderService,
            IValidator<FolderInsertDTos> validator, IValidator<FolderUpdate> validatorUpdate)
        {
            _folderService = folderService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
        }

        [HttpGet]
        public async Task<IEnumerable<FolderDTos>> GetAll() =>
            await _folderService.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<FolderDTos>> GetById(int id)
        {
            var folderDto = await _folderService.GetById(id);

            return folderDto == null ? NotFound() : Ok(folderDto);
        }

        [HttpPost]
        public async Task<ActionResult<FolderDTos>> Add(FolderInsertDTos insertDTos)
        {
            
            var validationResult = await _validator.ValidateAsync(insertDTos);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var folderDto = await _folderService.Add(insertDTos);

            return CreatedAtAction(nameof(GetById), new { id = folderDto.Id }, folderDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FolderDTos>> Update(int id, FolderUpdate folderUpdate)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(folderUpdate);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var folderUp = await _folderService.Update(id, folderUpdate);

            return folderUp == null ? NotFound() : Ok(folderUp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var folderDelete = await _folderService.Delete(id);

            return folderDelete == null ? NotFound() : NoContent();
        }
    }
}
