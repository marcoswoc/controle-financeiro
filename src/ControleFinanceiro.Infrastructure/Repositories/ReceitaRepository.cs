using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Context;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class ReceitaRepository : Repository<Receita>, IReceitaRepository
    {
        protected ReceitaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}