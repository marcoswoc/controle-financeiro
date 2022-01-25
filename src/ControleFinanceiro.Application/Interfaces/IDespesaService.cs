using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Despesa;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IDespesaService
    {
        Task<IEnumerable<DespesaDto>> GetAllDespesasAsync();
        Task<DespesaDto> GetDespesaByIdAsync(int id);
        Task<ResponseDto<DespesaDto>> CreateDespesaAsync(CreateDespesaDto despesaDto);
        Task<DespesaDto> UpdateDespesaAsync(int id, CreateDespesaDto despesaDto);
        Task<bool> DeleteDespesaAsync(int id);
         
    }
}