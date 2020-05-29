using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using UnicornDemo.Entities.Models;

namespace UnicornDemo.Services{
    [Route("api/v1/UnicornDemo/[controller]")]
    public class UsuariosController : Controller
    {
        private UnicornDemoContext dbContext = new UnicornDemoContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new UnicornDemoContext());
        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                var usuarios = unitOfWork.Usuarios.Get();
                if (usuarios != null)
                    return Ok(usuarios);
                else
                    return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUserDetails(int Id)
        {
            Usuario usuario = unitOfWork.Usuarios.GetByID(Id);
            if (usuario != null)
            return Ok(usuario) ;
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
                try
                {
                    if (ModelState.IsValid)
                    {
                        unitOfWork.Usuarios.Insert(usuario);
                        unitOfWork.Save();
                        return Created("UnicornDemo/Create" ,usuario);
                    }
                }
                    catch (DataException ex)
                    {
                         return BadRequest(ex);
                    }
             return BadRequest(usuario);
        }
        
        // PUT api/values/5

        [HttpPut]
        public IActionResult UpdateUser( [FromBody] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Usuarios.Update(usuario);
                    unitOfWork.Save();
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete]
        public IActionResult DeleteUser([FromHeader]int Id)
        {
           
            if (Id != 0)
            {
                unitOfWork.Usuarios.Delete(Id);
                unitOfWork.Save();
                return Ok("Usuario Eliminado");
            }
            else
            {
                return NoContent();
            }
        }
    }

}