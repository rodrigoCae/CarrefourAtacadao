using System;
using System.Collections.Generic;

namespace Carrefour_Atacadao_BackEnd.Entites;

public partial class TbCliente
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Rg { get; set; }

    public string? Cpf { get; set; }

    public DateTime? DataNascimento { get; set; }

    public string? Telefone { get; set; }

    public string? Email { get; set; }

    public int CodEmpresa { get; set; }

    public virtual ICollection<TbClienteEndereco> TbClienteEnderecos { get; set; } = new List<TbClienteEndereco>();
}
