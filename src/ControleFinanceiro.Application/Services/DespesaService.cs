using AutoMapper;
using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Despesa;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;
        private readonly IMapper _mapper;

        public DespesaService(IDespesaRepository despesaRepository, IMapper mapper)
        {
            _despesaRepository = despesaRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<DespesaDto>> CreateDespesaAsync(CreateDespesaDto despesaDto)
        {
            ResponseDto<DespesaDto> response = new();

            var exists = await _despesaRepository.FirstOrDefaultAsync(x => x.Descricao == despesaDto.Descricao && x.Data.Month == despesaDto.Data.Month && x.Data.Year == despesaDto.Data.Year);  

            if (exists is not null) 
            {
                response.Success = false;
                response.Erros.Add($"já existe uma despesa com adescrição {despesaDto.Descricao} para a data {despesaDto.Data.Month}/{despesaDto.Data.Year}");
                return response;
            }

            var despesa = await _despesaRepository.CreateAsync(_mapper.Map<Despesa>(despesaDto));
            
            response.Data = _mapper.Map<DespesaDto>(despesa); 

            return response;
        }


        public async Task<IEnumerable<DespesaDto>> GetAllDespesasAsync()
        {
            var despesas = await _despesaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DespesaDto>>(despesas);
        }

        public async Task<DespesaDto> GetDespesaByIdAsync(int id)
        {
            var despesa = await _despesaRepository.GetByIdAsync(id);
            return _mapper.Map<DespesaDto>(despesa);
        }

        public async Task<DespesaDto> UpdateDespesaAsync(int id, CreateDespesaDto despesaDto)
        {
            var despesa = await _despesaRepository.GetByIdAsync(id);
            
            if (despesa is null) return null;
            
            await _despesaRepository.UpdateAsync(_mapper.Map(despesaDto, despesa));

            return await GetDespesaByIdAsync(id);
        }

        public async Task<bool> DeleteDespesaAsync(int id)
        {
            var despesa = await _despesaRepository.GetByIdAsync(id);

            if(despesa is null) return false;

            await _despesaRepository.DeleteAsync(id);

            return true;
        }
    }
}