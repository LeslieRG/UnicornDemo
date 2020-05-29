using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornDemo.Entities.Models;

namespace UnicornDemo.Entities.DTOs
{
    public class ContactosList
    {
        public int idUserFriend { get; set; }
        public List<Usuario> usuarios = new List<Usuario>();
    }
}
