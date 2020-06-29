using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornDemo.Entities.Models;

namespace UnicornDemo.Entities.DTOs
{
    public class ContactosList
    {
        public Usuario usuario = new Usuario();
        public List<Usuario> amigos = new List<Usuario>();
    }
}
