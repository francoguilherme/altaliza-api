using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AltalizaApi.Application;
using AltalizaApi.Models;

namespace AltalizaApi.Controllers
{
    [Route("api/personagens")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private ApiContext _context;

        public CharacterController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("{characterId}")]
        public IActionResult GetCharacterById(int characterId)
        {
            try
            {
                var character = new CharacterApplication(_context).GetCharacterById(characterId);

                if (character != null)
                {
                    return Ok(character);
                }
                else
                {
                    return BadRequest("Personagem não encontrado");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        public IActionResult InsertCharacter([FromBody]Character character)
        {
            try {
                if (character == null) {
                    return BadRequest("Empty request");
                } else {
                    var rand = new Random();
                    character.Id = rand.Next(1000);
                    var response = new CharacterApplication(_context).InsertCharacter(character);
                    return Ok(character);
                }
            } catch (Exception) {
                return BadRequest("Error on insert character");
            }
        }

        [HttpPost("{characterId}/veiculos")]
        public IActionResult RentVehicle([FromBody]RentalRequest rentalRequest, int characterId)
        {
            try
            {
                if (rentalRequest == null) {
                    return BadRequest("Empty request");
                } else {
                    Rental rental = new Rental();
                    var rand = new Random();
                    rental.Id = rand.Next(1000);
                    rental.IdPersonagem = characterId;
                    rental.IdVeiculo = rentalRequest.IdVeiculo;
                    rental.DataExpiracao = rentalRequest.DataExpiracao;

                    var response = new CharacterApplication(_context).InsertRental(rental, rentalRequest.Preco);
                    if (response.Contains("success")) {
                        return Ok(rental);
                    } else {
                        return BadRequest(response);
                    }                    
                }
            }
            catch (Exception)
            {
                return BadRequest("Error on rental");
            }
        }

        [HttpGet("{characterId}/veiculos")]
        public IActionResult GetAllRentals(int characterId)
        {        
            try
            {
                var lista = new CharacterApplication(_context).GetAllUserVehicles(characterId);

                if (lista != null)
                {
                    return Ok(lista);
                }
                else
                {
                    return BadRequest("Nenhum usuário cadastrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost("{characterId}/veiculos/{rentalId}/renovar")]
        public IActionResult RenewRental([FromBody] RenewRentalRequest renewRequest, int characterId, int rentalId)
        {
           try
            {
                var response = new CharacterApplication(_context).RenewRental(renewRequest, characterId, rentalId);
                if (response != null) {
                    return Ok(response);
                } else {
                    return BadRequest("Error on renew rental");
                }                    
            }
            catch (Exception)
            {
                return BadRequest("Error on renew rental");
            }
        }

        [HttpPost("{characterId}/veiculos/{rentalId}/devolver")]
        public IActionResult CancelRental(int characterId, int rentalId)
        {
           try
            {
                var response = new CharacterApplication(_context).CancelRental(characterId, rentalId);
                if (response != null) {
                    return Ok(response);
                } else {
                    return BadRequest("Error on cancel rental");
                }                    
            }
            catch (Exception)
            {
                return BadRequest("Error on cancel rental");
            }
        }
    }
}