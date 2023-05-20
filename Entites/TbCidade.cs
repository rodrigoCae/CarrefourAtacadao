using System;
using System.Collections.Generic;

namespace Carrefour_Atacadao_BackEnd.Entites;

public partial class TbCidade
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<TbEndereco> TbEnderecos { get; set; } = new List<TbEndereco>();
}
