using capacitacion4b_api.Data.interfaces;
using capacitacion4b_api.Data.services;
using capacitacion4b_api.DTOs.user;
using capacitacion4b_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace capacitacion4b_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : Controller
    {

        private iUserService _userService;
        public userController(iUserService userService) => _userService = userService;


        [HttpGet]
        /* obtiene todos los usuarios */
        public async Task<IActionResult> FindAll()
        {

            var users = await _userService.FindAll();
            return Ok(users);

        }

        [HttpGet("idUsuario")]
        /* obtiene solo el usuario indicado */
        public async Task<IActionResult> FindOne(int idUsuario)
        {

            userModel? user = await _userService.FindOne(idUsuario);
            return Ok(user);

        }

        [HttpPost]
        /* crea un nuevo usuario */
        public async Task<IActionResult> Create([FromBody] createUserDto createUserDto)
        {

            userModel? user = await _userService.Create(createUserDto);
            return Created(user?.idUsuario.ToString(), user);

        }

        [HttpPut("{idUsuario}")]
        /* actualiza al usuario indicado */
        public async Task<IActionResult> Update(int idUsuario, [FromBody] updateUserDto updateUserDto)
        {

            userModel? user = await _userService.Update(idUsuario, updateUserDto);
            return Ok(user);

        }

        [HttpDelete("{idUsuario}")]
        /* elimina la tarea indicada */
        public async Task<IActionResult> Remove(int idUsuario)
        {

            userModel? user = await _userService.Remove(idUsuario);
            return Ok(user);

        }

    }

}
