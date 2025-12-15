namespace Domain.Models;

public class UsuarioTotais
{
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public decimal TotalReceita { get; set; }
    public decimal TotalDespesa { get; set; }
    public decimal Saldo => TotalReceita - TotalDespesa;
}
