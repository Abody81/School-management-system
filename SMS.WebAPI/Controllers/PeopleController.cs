using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PeopleAPIServer;
using SMS.Application.Services;
using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Domain.Entities;
using SMS.WebAPI.DTOs.PersonDTOs;

namespace SMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequestSizeLimit(5302880)]
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
        public async Task<ActionResult<PersonResponse>> GetPersonByID([FromRoute] int Id)
        {
            var result = await _personService.Get.ExecuteAsync(Id);

            if (!result.IsSuccess) return this.ToActionResult(result);

            PersonResponse PersonDTO = _mapper.Map<PersonResponse>(result.Data);

            PersonDTO.ImageURl = Url.Action(nameof(GetPersonImage),
                "People",
                new { fileName = PersonDTO.ImageURl }, Request.Scheme)!;

            return Ok(PersonDTO);
        }


        [HttpGet("image/{fileName}")]
        public async Task<IActionResult> GetPersonImage(string fileName)
        {
            var result = await _personService.GetImage.ExecuteAsync(fileName);

            if (!result.IsSuccess) return this.ToActionResult(result);

            return Ok(result.Data);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestFormLimits(MultipartBoundaryLengthLimit = 5302880)]
        public async Task<ActionResult<int>> AddPerson([FromForm] CreatePersonRequest request)
        {
            await using var ImageStream = request.Image.OpenReadStream();

            var Command = new CreatePersonCommand(request.FirstName, request.SecondName, request.ThirdName, request.LastName,
                request.Gender, request.DateOfBirth, request.NationalNumber, request.PhoneNumber,
                request.CountryID, request.Address, ImageStream);

            var result = await _personService.Add.ExecuteAsync(Command);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetPersonByID), new { Id = result.Data }, result.Data);

            return this.ToActionResult(result);
        }


        [HttpPut("{Id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UpdatePerson([FromRoute] int Id, [FromForm] UpdatePersonRequest request)
        {
            await using var ImageStream = request.ImageFile?.OpenReadStream(); // Stream will close automatically after the operation, because of the await using statement.

            var Command = new UpdatePersonCommand(Id, request.FirstName, request.SecondName, request.ThirdName, request.LastName,
              request.Gender, request.DateOfBirth, request.NationalNumber, request.PhoneNumber,
              request.CountryID, request.Address, ImageStream);

            var result = await _personService.Update.ExecuteAsync(Command);

            return this.ToActionResult(result);
        }

    }
}
