namespace Domain.Models;

public class CategoriaTotais
{
    public Guid CategoriaId { get; set; }
    public string Nome { get; set; }
    public decimal TotalReceita { get; set; }
    public decimal TotalDespesa { get; set; }
    public decimal Saldo => TotalReceita - TotalDespesa;
}
