using Microsoft.Extensions.Configuration;
using myfinance_web_dotnet_infra.Interfaces.Base;

namespace myfinance_web_dotnet_infra
{
    public abstract class Repository<TEntity> : MyFinanceDbContext, IRepository<TEntity> where TEntity : class
    {
        protected Repository(IConfiguration configuration) : base(configuration)
        {
            
        }
        public void Cadastrar(TEntity Entidade)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> ListarRegistros()
        {
            throw new NotImplementedException();
        }

        public TEntity RetornarRegistro(int Id)
        {
            throw new NotImplementedException();
        }
   }
}