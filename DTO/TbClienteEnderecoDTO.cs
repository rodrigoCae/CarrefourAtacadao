using System;
using System.Collections.Generic;

namespace Carrefour_Atacadao_BackEnd.DTO
{

    public partial class TbClienteEnderecoDTO
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int EnderecoId { get; set; }

        public virtual TbClienteDTO? Cliente { get; set; }

        public virtual TbEnderecoDTO? Endereco { get; set; }
    }
}