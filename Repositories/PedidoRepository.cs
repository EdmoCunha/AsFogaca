using Microsoft.EntityFrameworkCore;

public class PedidoRepository : IPedidoRepository{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context){
        _context = context;
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync(){
        return await _context.Pedidos.ToListAsync();
    }

    public async Task<Pedido?> GetByIdAsync(int id){
        return await _context.Pedidos.FindAsync(id);
    }

    public async Task AddAsync(Pedido pedido){
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Pedido pedido){
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id){
        var pedido = await GetByIdAsync(id);
        if (pedido != null){
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
