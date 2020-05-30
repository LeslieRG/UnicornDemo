using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                    var contactos = unitOfWork.Contactos.Get(x => x.IdUsuario == idUsuario);
                    if (contactos != null)
                    {
                        var result = CreateMappedObject(contactos, idUsuario);

                        var serializedlist = JsonConvert.SerializeObject(result, Formatting.Indented,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                            });


                        return Ok(serializedlist);
                    }
                    else
                        return NoContent();
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

        private ContactosList CreateMappedObject(IEnumerable<Contacto> contactos, int idUser)
        {
            ContactosList listFriends = new ContactosList();
            foreach (var item in contactos)
            {
                Usuario contactoAmigo = unitOfWork.Usuarios.GetByID(item.IdContacto);
                listFriends.usuarios.Add(contactoAmigo);
            }
            listFriends.idUserFriend = idUser;
            return listFriends;
        }

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

        [HttpPost("{idUsuario}")]
        public IActionResult CreateContact([FromBody] Usuario contactoAmigo, int idUsuario)
        {
                try
                {
                    if (ModelState.IsValid)
                    {
                        unitOfWork.Usuarios.Insert(contactoAmigo);
                        unitOfWork.Save();
                        
                        Contacto contacto = new Contacto();
                        contacto.IdUsuario = idUsuario;
                        contacto.IdContacto = contactoAmigo.Id;
                        contacto.FechaCreacion = DateTime.Now;
                        unitOfWork.Contactos.Insert(contacto);
                    unitOfWork.Save();
                    return Created("UnicornDemo/CreateContact" ,contactoAmigo);
                    }
                }
                    catch (DataException ex)
                    {
                         return BadRequest(ex);
                    }
             return BadRequest(contactoAmigo);
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