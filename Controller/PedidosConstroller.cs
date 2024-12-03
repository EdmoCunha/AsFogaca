using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidosController(IPedidoRepository pedidoRepository){
        _pedidoRepository = pedidoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pedido>>> GetAll(){
        var pedidos = await _pedidoRepository.GetAllAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pedido>> GetById(int id){
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        if (pedido == null)
            return NotFound();

        return Ok(pedido);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Pedido pedido){
        await _pedidoRepository.AddAsync(pedido);
        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Pedido pedido){
        if (id != pedido.Id)
            return BadRequest("O ID do pedido não corresponde ao informado na URL.");

        await _pedidoRepository.UpdateAsync(pedido);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id){
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        if (pedido == null)
            return NotFound();

        await _pedidoRepository.DeleteAsync(id);
        return NoContent();
    }
}