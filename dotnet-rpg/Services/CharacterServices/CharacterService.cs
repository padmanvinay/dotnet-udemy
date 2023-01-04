using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dto.Character;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> { };
        private readonly IMapper mapper;
        private readonly CharacterDbContext context;

        public CharacterService(IMapper mapper, CharacterDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = mapper.Map<Character>(newCharacter);
            context.Characters.Add(character);
            await context.SaveChangesAsync();
            serviceResponse.Data = await context.Characters.Select(
                    c => mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character character = await context.Characters.FirstAsync(c => c.Id == id);
                context.Characters.Remove(character);
                await context.SaveChangesAsync();
                response.Data = context.Characters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await context.Characters.ToListAsync();
            response.Data = dbCharacters.Select(c =>
                mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await context.Characters.FirstOrDefaultAsync(u => u.Id == id);
            serviceResponse.Data = mapper.Map<GetCharacterDto>(dbCharacters);
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {

                var character = await context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacterDto.Id);

                if (response.Data == null)
                {
                    character.Name = updateCharacterDto.Name;
                    character.HitPoints = updateCharacterDto.HitPoints;
                    character.Strength = updateCharacterDto.Strength;
                    character.Defense = updateCharacterDto.Defense;
                    character.Intelligence = updateCharacterDto.Intelligence;
                    character.Class = updateCharacterDto.Class;


                    await context.SaveChangesAsync();

                }
                response.Data = mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;

        }
    }
}