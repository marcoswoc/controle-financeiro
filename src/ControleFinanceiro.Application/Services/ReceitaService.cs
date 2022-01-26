using AutoMapper;
using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Receita;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class ReceitaService : IReceitaService
    {
         private readonly IReceitaRepository _ReceitaRepository;
        private readonly IMapper _mapper;

        public ReceitaService(IReceitaRepository ReceitaRepository, IMapper mapper)
        {
            _ReceitaRepository = ReceitaRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<ReceitaDto>> CreateReceitaAsync(CreateReceitaDto receitaDto)
        {
            ResponseDto<ReceitaDto> response = new();            

            var receita = await _ReceitaRepository.CreateAsync(_mapper.Map<Receita>(receitaDto));
            
            response.Data = _mapper.Map<ReceitaDto>(receita); 

            return response;
        }


        public async Task<IEnumerable<ReceitaDto>> GetAllReceitasAsync()
        {
            var Receitas = await _ReceitaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReceitaDto>>(Receitas);
        }

        public async Task<ReceitaDto> GetReceitaByIdAsync(int id)
        {
            var Receita = await _ReceitaRepository.GetByIdAsync(id);
            return _mapper.Map<ReceitaDto>(Receita);
        }

        public async Task<ReceitaDto> UpdateReceitaAsync(int id, CreateReceitaDto ReceitaDto)
        {
            var Receita = await _ReceitaRepository.GetByIdAsync(id);
            
            if (Receita is null) return null;
            
            await _ReceitaRepository.UpdateAsync(_mapper.Map(ReceitaDto, Receita));

            return await GetReceitaByIdAsync(id);
        }

        public async Task<bool> DeleteReceitaAsync(int id)
        {
            var Receita = await _ReceitaRepository.GetByIdAsync(id);

            if(Receita is null) return false;

            await _ReceitaRepository.DeleteAsync(id);

            return true;
        }
    }
}