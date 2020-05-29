using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using UnicornDemo.Entities.DTOs;
using UnicornDemo.Entities.Models;

namespace UnicornDemo.Services{

    [Route("api/v1/UnicornDemo/[controller]")]
    public class ContactosController : Controller
    {
        
        private UnitOfWork unitOfWork = new UnitOfWork(new UnicornDemoContext());

        [HttpGet("{idUsuario}")]
        public IActionResult GetAllContactoByUser(int idUsuario)
        {
            if (idUsuario!= 0)
            {
                var user = unitOfWork.Usuarios.Get(x=>x.Id== idUsuario);
                if (user != null)
                {
                    //var contactos = unitOfWork.Contactos.Get(x => x.IdUsuario == idUsuario);
                    //if (contactos != null)
                    //    var result =CreateMappedObject(contactos);
                    //return Ok(result);
                    //else
                       return Ok();
                }
                else
                {
                    return BadRequest("Usuario no existe");
                }
               
            }
            else
            {
                return BadRequest("Usuario no existe");
            }
        }

        //private List<ContactosList> CreateMappedObject(IEnumerable<Contacto> contactos)
        //{
        //    ContactosList listFriends = new ContactosList();
        //    foreach (var item in contactos)
        //    {
        //        var contactoAmigo = unitOfWork.Usuarios.Get(x => x.Id == item.IdContacto);
        //        listFriends.usuarios.Add(contactoAmigo);
        //    }
        //}

        [HttpGet("{id}/{contactoid, }")]
        public IActionResult GetContactDetails(int Id, bool isFriend)
        {
            if (isFriend)
            {

                var contacto = unitOfWork.Contactos.Get(c=> c.Id == Id);
                if (contacto != null)
                    return Ok(contacto);
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] Contacto contacto)
        {
                try
                {
                    if (ModelState.IsValid)
                    {
                        unitOfWork.Contactos.Insert(contacto);
                        unitOfWork.Save();
                        return Created("UnicornDemo/CreateContact" ,contacto);
                    }
                }
                    catch (DataException ex)
                    {
                         return BadRequest(ex);
                    }
             return BadRequest(contacto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact([FromRoute]int id, [FromBody] Contacto contacto)
        {
            Contacto ContactoSearch = unitOfWork.Contactos.GetByID(id);
            if (ContactoSearch != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        unitOfWork.Contactos.Update(contacto);
                        unitOfWork.Save();
                        return Ok();
                    }
                }
                catch (DataException ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return NotFound("El usuario que intenta actualizar no existe");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContactoi(int Id)
        {

            if (Id != 0)
            {
                unitOfWork.Contactos.Delete(Id);
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