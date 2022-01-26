using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Receita;
using ControleFinanceiro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitaController : ControllerBase
    {

        private readonly IReceitaService _receitaService;

        public ReceitaController(IReceitaService receitaService)
        {
            _receitaService = receitaService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceitaDto>>> GetAllReceitaAsync()
        {
            return Ok(await _receitaService.GetAllReceitasAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceitaDto>> GetReceitaByIdAsync([FromRoute]int id)
        {
            var receita = await _receitaService.GetReceitaByIdAsync(id);
            if (receita is null) return NotFound();
            return Ok(receita);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ReceitaDto>>> CreateReceitaAsync([FromBody] CreateReceitaDto receitaDto)
        {
            var receita = await _receitaService.CreateReceitaAsync(receitaDto);
            
            if (!receita.Success) return BadRequest(receita);
            
            return Ok(receita);           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReceitaDto>> UpdateReceitaAsync([FromRoute]int id, [FromBody] CreateReceitaDto receitaDto)
        {
            var receita = await _receitaService.UpdateReceitaAsync(id, receitaDto);
            if (receita is null) return NotFound();
            return Ok(receita);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReceitaAsync([FromRoute]int id)
        {
            var deleted = await _receitaService.DeleteReceitaAsync(id);
            if (deleted) return NoContent();
            return NotFound();
        }
    }
}