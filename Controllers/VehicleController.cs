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
    [Route("api/veiculos")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private ApiContext _context;

        public VehicleController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            try
            {
                var lista = new VehicleApplication(_context).GetAllVehicles();

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
    }
}