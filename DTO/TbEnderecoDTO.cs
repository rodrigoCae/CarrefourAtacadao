using System;
using System.Collections.Generic;

namespace Carrefour_Atacadao_BackEnd.DTO {

    public partial class TbEnderecoDTO
    {
        public int Id { get; set; }

        public string? Rua { get; set; }

        public string? Bairro { get; set; }

        public string? Numero { get; set; }

        public string? Complemento { get; set; }

        public string? Cep { get; set; }

        public int TipoEndereco { get; set; }

        public int CidadeId { get; set; }

        public virtual TbCidadeDTO? Cidade { get; set; }
    }
}
