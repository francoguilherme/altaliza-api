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
    [Route("api/categorias")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ApiContext _context;

        public CategoryController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult InsertCategory([FromBody]Category category) {
            try {
                if (category == null) {
                    return BadRequest("Empty request");
                } else {
                    var rand = new Random();
                    category.Id = rand.Next(1000);
                    var response = new CategoryApplication(_context).InsertCategory(category);
                    return Ok(category);
                }
            } catch (Exception) {
                return BadRequest("Error on insert category");
            }
        }

        [HttpPost("{categoryId}/veiculos")]
        public IActionResult InsertVehicle([FromBody]Vehicle vehicle, int categoryId) {
            try {
                if (vehicle == null) {
                    return BadRequest("Empty request");
                } else {
                    var rand = new Random();
                    vehicle.Id = rand.Next(1000);
                    vehicle.IdCategoria = categoryId;
                    var response = new VehicleApplication(_context).InsertVehicle(vehicle);
                    return Ok(vehicle);
                }
            } catch (Exception) {
                return BadRequest("Error on insert vehicle");
            }
        }
    }
}