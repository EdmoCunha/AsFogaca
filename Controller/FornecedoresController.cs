using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController : ControllerBase
{
    private readonly IFornecedorRepository _fornecedorRepository;

    public FornecedoresController(IFornecedorRepository fornecedorRepository){
        _fornecedorRepository = fornecedorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fornecedor>>> GetAll(){
        var fornecedores = await _fornecedorRepository.GetAllAsync();
        return Ok(fornecedores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Fornecedor>> GetById(int id)
    {
        var fornecedor = await _fornecedorRepository.GetByIdAsync(id);
        if (fornecedor == null)
            return NotFound();

        return Ok(fornecedor);
    }

    [HttpPost]
    public async Task<ActionResult> Create( Fornecedor fornecedor)
    {
        await _fornecedorRepository.AddAsync(fornecedor);
        return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id }, fornecedor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Fornecedor fornecedor)
    {
        if (id != fornecedor.Id)
            return BadRequest("O ID do fornecedor não corresponde ao informado na URL.");

        await _fornecedorRepository.UpdateAsync(fornecedor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var fornecedor = await _fornecedorRepository.GetByIdAsync(id);
        if (fornecedor == null)
            return NotFound();

        await _fornecedorRepository.DeleteAsync(id);
        return NoContent();
    }
}
