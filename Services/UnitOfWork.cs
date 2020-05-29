using UnicornDemo.Entities.Models;

namespace UnicornDemo.Services
{
    public class UnitOfWork: IUnitOfWork
    {
        private UnicornDemoContext _dbContext;
        private BaseRepository<Usuario> _usuario;
        private BaseRepository<Contacto> _contacto;

        public UnitOfWork(UnicornDemoContext dbContext)
        {
            _dbContext =  dbContext;
        }

        public IRepository<Usuario> Usuarios
        {
            get{
                return _usuario ??
                                    (_usuario = new BaseRepository<Usuario>(_dbContext));
            }
        }

        public IRepository<Contacto> Contactos
        {
            get
            {
                return _contacto ?? 
                                    (_contacto=new BaseRepository<Contacto>(_dbContext));

            }
        }

        //public IRepository<Usuario> Usuario => throw new System.NotImplementedException();

        //public IRepository<Contacto> Contacto => throw new System.NotImplementedException();

        public void Save()
        {
                _dbContext.SaveChanges();
        }
        
    }
}