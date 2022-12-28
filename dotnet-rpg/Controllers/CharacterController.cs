using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dto.Character;
using dotnet_rpg.models;
using dotnet_rpg.Services.CharacterServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_rpg.Controllers
{
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await characterService.GetAllCharacter());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
        {
            var response = await characterService.DeleteCharacter(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await characterService.AddCharacter(newCharacter));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            var response = await characterService.UpdateCharacter(updateCharacterDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}