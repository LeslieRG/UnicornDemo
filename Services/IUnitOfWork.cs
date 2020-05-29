using UnicornDemo.Entities.Models;

namespace UnicornDemo.Services
{
    public interface IUnitOfWork
    {
        IRepository<Usuario> Usuarios { get; }
        IRepository<Contacto> Contactos { get; }
        void Save();
    }
}