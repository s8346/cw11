using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbd11.Entities;
using apbd11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apbd11.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private IDoctorsService _service;
        public DoctorsController(IDoctorsService service)
        {
            _service = service;
        }

        [HttpPost("samples")]
        public IActionResult AddDefault()
        {
            _service.AddDefault();
            return Ok("Added!");
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            var list = _service.GetDoctors();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult AddNewDoctor(Doctor doctor)
        {
            var succeeded = _service.AddDoctor(doctor);
            if (succeeded)
            {
                return Ok("Doctor added!");
            } else
            {
                return BadRequest("Bad request! Probably it's the id!");
            }
        }

        [HttpPut]
        public IActionResult UpdateDoctor(Doctor doctor)
        {
            var succeeded = _service.UpdateDoctor(doctor);
            if (succeeded)
            {
                return Ok("Doctor updated!");
            }
            else
            {
                return BadRequest("Bad request! Probably it's the id!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var succeeded = _service.DeleteDoctor(id);
            if (succeeded)
            {
                return Ok("Doctor deleted!");
            }
            else
            {
                return BadRequest("Bad request! Probably it's the id!");
            }
        }
    }
}