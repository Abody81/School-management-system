using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PeopleAPIServer;
using SMS.Business.PersonServices;
using SMS.Business.Util;
using SMS.Core.Entities;
using SMS.WebAPI.DTOs.PersonDTOs;
using System.ComponentModel;

namespace SMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _personService;
        private readonly IMapper _mapper;
        public PeopleController(PersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PersonResponseDTO>> GetPersonByID([FromRoute] int Id)
        {
            var result = await _personService.GetPersonByID(Id);

            if (!result.IsSuccess) return this.ToActionResult(result);

            PersonResponseDTO PersonDTO = _mapper.Map<PersonResponseDTO>(result.Data);

            PersonDTO.ImageURl = Url.Action(nameof(GetPersonImage),
                "People",
                new { fileName = PersonDTO.ImageURl }, Request.Scheme)!;

            return Ok(PersonDTO);
        }


        [HttpGet("image/{fileName}")]
        public IActionResult GetPersonImage(string fileName)
        {
            var safeFileName = Path.GetFileName(fileName);      // for security very important  
            string path = Path.Combine(ImageHandler._folderPath, safeFileName);

            if (!System.IO.File.Exists(path))
                return NotFound("Image not found.");

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out string? contentType))
            {
                contentType = "application/octet-stream";  // default content type if the file extension is unknown
            }

            return PhysicalFile(path, contentType);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<int>> AddPerson([FromForm] PersonCreateDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);

            await using var stream = personDTO.ImageFile.OpenReadStream();
            var fileExtension = Path.GetExtension(personDTO.ImageFile.FileName);

            var result = await _personService.AddPerson(person, stream, fileExtension);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetPersonByID), new { Id = result.Data }, result.Data);

            return this.ToActionResult(result);
        }


        [HttpPut("{Id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UpdatePerson([FromRoute] int Id, [FromForm] PersonUpdateDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);

            person.PersonID = Id;

            // if no image uploaded, imageStream will be null, and the service should handle this case by keeping the existing image unchanged.
            await using var imageStream = personDTO.ImageFile?.OpenReadStream(); // Stream will close automatically after the operation, because of the await using statement.
            // (using) for close and dispose the stream and (await) for closing and disposing async.

            var fileExtension = personDTO.ImageFile != null ? Path.GetExtension(personDTO.ImageFile.FileName) : null;

            var result = await _personService.UpdatePerson(person, imageStream, fileExtension);

            return this.ToActionResult(result);
        }

    }
}
