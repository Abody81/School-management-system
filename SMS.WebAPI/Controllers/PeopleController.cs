using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PeopleAPIServer;
using SMS.Application.Services;
using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Application.UseCases.People.Commands.DeletePerson;
using SMS.Application.UseCases.People.Queries.GetAllPeople;
using SMS.Application.UseCases.People.Queries.GetAllPeopleFiltered;
using SMS.WebAPI.DTOs.PersonDTOs;
using SMS.WebAPI.Requests.PersonRequests;

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
        public async Task<ActionResult<PersonResponse>> GetPersonByID([FromRoute] int Id, CancellationToken cancellationToken = default)
        {
            var result = await _personService.Get.ExecuteAsync(Id, cancellationToken);

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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAllPeople([FromQuery] int lastId, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _personService.GetAll.ExecuteAsync(new GetAllPeopleQuery(lastId, pageSize), cancellationToken);
            if (!result.IsSuccess) return this.ToActionResult(result);

            List<PersonResponse> PeopleList = _mapper.Map<List<PersonResponse>>(result.Data);

            foreach (var PersonDTO in PeopleList)
            {
                PersonDTO.ImageURl = Url.Action(nameof(GetPersonImage),
                    "People",
                    new { fileName = PersonDTO.ImageURl }, Request.Scheme)!;
            }

            object Response = new
            {
                PeopleList,
                lastId = PeopleList.LastOrDefault()?.PersonID ?? lastId
            };

            return Ok(Response);
        }


        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAllPeopleFiltered([FromQuery] GetAllPeopleFilteredRequest request, CancellationToken cancellationToken = default)
        {
            var query = new GetAllPeopleFilteredQuery(request.LastId, request.PageSize, request.PersonId,
                request.NationalNumber, request.FirstName, request.SecondName,
                request.ThirdName, request.LastName);

            var result = await _personService.GetAllFiltered.ExecuteAsync(query, cancellationToken);
            if (!result.IsSuccess) return this.ToActionResult(result);

            List<PersonResponse> PeopleList = _mapper.Map<List<PersonResponse>>(result.Data);

            foreach (var PersonDTO in PeopleList)
            {
                PersonDTO.ImageURl = Url.Action(nameof(GetPersonImage),
                    "People",
                    new { fileName = PersonDTO.ImageURl }, Request.Scheme)!;
            }

            object Response = new
            {
                PeopleList,
                lastId = PeopleList.LastOrDefault()?.PersonID ?? request.LastId
            };

            return Ok(Response);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestFormLimits(MultipartBoundaryLengthLimit = 5302880)]
        public async Task<ActionResult<int>> AddPerson([FromForm] CreatePersonRequest request, CancellationToken cancellationToken = default)
        {
            await using var ImageStream = request.Image.OpenReadStream();

            var Command = new CreatePersonCommand(request.FirstName, request.SecondName, request.ThirdName, request.LastName,
                request.Gender, request.DateOfBirth, request.NationalNumber, request.PhoneNumber,
                request.CountryID, request.Address, ImageStream);

            var result = await _personService.Add.ExecuteAsync(Command, cancellationToken);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetPersonByID), new { Id = result.Data }, result.Data);

            return this.ToActionResult(result);
        }



        [HttpPut("{Id}")]
        [Consumes("multipart/form-data")]
        [RequestFormLimits(MultipartBoundaryLengthLimit = 5302880)]
        public async Task<ActionResult> UpdatePerson([FromRoute] int Id, [FromForm] UpdatePersonRequest request, CancellationToken cancellationToken = default)
        {
            await using var ImageStream = request.ImageFile?.OpenReadStream(); // Stream will close automatically after the operation, because of the await using statement.

            var Command = new UpdatePersonCommand(Id, request.FirstName, request.SecondName, request.ThirdName, request.LastName,
              request.Gender, request.DateOfBirth, request.NationalNumber, request.PhoneNumber,
              request.CountryID, request.Address, ImageStream);

            var result = await _personService.Update.ExecuteAsync(Command, cancellationToken);

            return this.ToActionResult(result);
        }



        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePerson([FromRoute] int Id, CancellationToken cancellationToken = default)
        {
            var Command = new DeletePersonCommand(Id);

            var result = await _personService.Delete.ExecuteAsync(Command, cancellationToken);
            return this.ToActionResult(result);
        }

    }
}