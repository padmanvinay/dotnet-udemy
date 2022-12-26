using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
      public async Task<ActionResult<ServiceResponse<List<Character>>>> Get()
      {
      return Ok(await characterService.GetAllCharacter());
      }
      
      [HttpGet("{id}")]
      public async Task<ActionResult<ServiceResponse<Character>>> GetSingle(int id)
      {
         return Ok(await characterService.GetCharacterById(id));
      }
      
      [HttpPost]
      public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacter(Character newCharacter)
      {
         return Ok(await characterService.AddCharacter(newCharacter));
      }
   }
}