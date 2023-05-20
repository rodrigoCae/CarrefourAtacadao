using AutoMapper;
using Carrefour_Atacadao_BackEnd.Context;
using Carrefour_Atacadao_BackEnd.Entites;
using Carrefour_Atacadao_BackEnd.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Dapper;
using Carrefour_Atacadao_BackEnd.Enum;

namespace Carrefour_Atacadao_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteEnderecoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarrefourAtacadaoContext dbContext;

        public ClienteEnderecoController(CarrefourAtacadaoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this._mapper = mapper;
        }

        // GET: TbAcessosController
        [HttpGet("todos/{Cod_empresa}")]
        //[Route("todos/{Cod_empresa}")]
        //[Authorize]
        public ActionResult Get([FromRoute] string Cod_empresa, string? Nome = "", string? CPF = "", string? Cidade = "", string? UF = "")
        {
            //var clienteEndereco = dbContext.TbClienteEnderecos.ToList();
            //if(Cod_empresa != null)
            //    clienteEndereco = dbContext.TbClienteEnderecos.Where(a=>a.Cliente.CodEmpresa.Equals(Cod_empresa)).ToList();
            //if (Nome != null)
            //    clienteEndereco = dbContext.TbClienteEnderecos.Where(a => a.Cliente.Nome.Equals(Nome)).ToList();
            //if (CPF != null)
            //    clienteEndereco = dbContext.TbClienteEnderecos.Where(a => a.Cliente.Cpf.Equals(CPF)).ToList();
            //if(Cidade != null)
            //    clienteEndereco = dbContext.TbClienteEnderecos.Where(a => a.Endereco.Cidade.Nome.Equals(CPF)).ToList();
            //if (UF != null)
            //    clienteEndereco = dbContext.TbClienteEnderecos.Where(a => a.Endereco.Cidade.Estado.Equals(UF)).ToList();
            var ClienteEndereco = new List<TbClienteEndereco>();
            var Cliente = new TbCliente();
            var Endereco = new TbEndereco();

            var sql = @"select Id, cliente_Id AS ClienteId, Endereco_Id AS EnderecoId "+
                        "from tb_cliente_endereco "+
                        "where cliente_Id IN (select cliente.ID from TB_CLIENTE cliente where ((cod_empresa =   '"+Cod_empresa+"' Or Len('"+Cod_empresa+"') = 0)AND(nome = '" + Nome+ "' OR Len('"+Nome+"') = 0)AND(cpf = '"+CPF+"' OR Len('"+CPF+"') = 0))) AND " +
                        "Endereco_Id IN(select ID from TB_ENDERECO where CIDADE_ID in(select ID from TB_CIDADE where (nome = '"+Cidade+"' OR Len('"+Cidade+"') = 0)AND(ESTADO = '"+UF+"' OR Len('"+UF+"') = 0)))";

            using (var connection = new SqlConnection("Data Source=DESKTOP-LSEMRNN;Initial Catalog=Carrefour_Atacadao;Integrated Security=True;TrustServerCertificate=True;"))
            {
                connection.Open();
                ClienteEndereco = connection.Query<TbClienteEndereco>(sql).ToList();

                foreach (TbClienteEndereco tbClienteEndereco in ClienteEndereco)
                {
                    if (tbClienteEndereco != null)
                    {
                        if (tbClienteEndereco.Id != 0)
                        {
                            foreach (TbClienteEndereco tbCli in ClienteEndereco)
                            {
                                tbCli.Cliente = connection.Query<TbCliente>("select cliente.ID, cliente.NOME, cliente.RG, cliente.CPF, cliente.DATA_NASCIMENTO, cliente.TELEFONE, cliente.EMAIL, cliente.COD_EMPRESA  from TB_CLIENTE cliente where ID = " + tbCli.ClienteId + " AND ((cod_empresa =   '" + Cod_empresa + "' Or Len('" + Cod_empresa + "') = 0)AND(nome = '" + Nome + "' OR Len('" + Nome + "') = 0)AND(cpf = '" + CPF + "' OR Len('" + CPF + "') = 0))").FirstOrDefault();
                            }
                            foreach (TbClienteEndereco tbEnd in ClienteEndereco)
                            {
                                tbEnd.Endereco = connection.Query<TbEndereco>("select ID, RUA, BAIRRO, NUMERO, COMPLEMENTO, CEP, TIPO_ENDERECO, CIDADE_ID from TB_ENDERECO where  ID = " + tbEnd.EnderecoId + " AND  CIDADE_ID in (select ID from TB_CIDADE where (nome = '" + Cidade + "' OR Len('" + Cidade + "') = 0)AND(ESTADO = '" + UF + "' OR Len('" + UF + "') = 0))").FirstOrDefault();
                            }
                            foreach (TbClienteEndereco tbCliEnd in ClienteEndereco)
                            {
                                ClienteEndereco = ClienteEndereco.Where(x => x.ClienteId == tbCliEnd.Cliente.Id && x.EnderecoId == tbCliEnd.Endereco.Id).ToList();
                            }
                        }
                        else
                        {
                            NotFound("Não há registros com essa busca!");
                        }
                    }
                    else
                    {
                        NotFound("Não há registros com essa busca!");
                    }
                }
            }

            if (ClienteEndereco != null)
                return Ok(ClienteEndereco);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize]
        // GET: TbAcessosController/Details/5
        public ActionResult Details([FromRoute] int id)
        {
            var clienteEndereco = dbContext.TbClienteEnderecos.Find(id);
            
            if (clienteEndereco != null)
            {
                return Ok(clienteEndereco);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        //[Authorize]
        // POST: TbAcessosController/Create
        public ActionResult Create([FromBody] TbClienteEnderecoDTO clienteEnderecoDTO)
        {
            try
            {
                int idCliente = clienteEnderecoDTO.ClienteId;
                TbCliente? Clientes = dbContext.TbClientes.Find(idCliente);
                int idEndereco = clienteEnderecoDTO.EnderecoId;
                TbEndereco? Enderecos = dbContext.TbEnderecos.Find(idEndereco);

                if (Clientes != null && Enderecos != null)
                {
                    clienteEnderecoDTO.Cliente = _mapper.Map<TbClienteDTO>(Clientes);
                    clienteEnderecoDTO.Endereco = _mapper.Map<TbEnderecoDTO>(Enderecos);

                    if (RetornarTipoEnderecoExistente(clienteEnderecoDTO) && RetornarCpfExistente(clienteEnderecoDTO) && RetornarCodigoEmpresaValido(clienteEnderecoDTO) && RetornarTipoEnderecoValido(clienteEnderecoDTO))
                    {
                        TbClienteEndereco clienteEndereco = _mapper.Map<TbClienteEndereco>(clienteEnderecoDTO);

                        dbContext.TbClienteEnderecos.Add(clienteEndereco);
                        dbContext.SaveChanges();

                        return Ok(clienteEndereco);
                    }
                    else
                    {
                        return NotFound("Este cliente já possui o tipo de endereço solicitado ou o tipo de endereço não existe ou o código da empresa está inválido!");
                    }
                }
                else
                {
                    return NotFound("Não há o cliente ou endereço cadastrados com o código passado! revise as informações...");
                }
            }catch(Exception e)
            {
                throw new Exception("Ocorreram erros ao cadastrar um novo usuário e seu respectivo endereço!");
            }
        }

        [HttpPut]
        //[Authorize]
        // GET: TbAcessosController/Edit/5
        public ActionResult Edit(int id, [FromBody] TbClienteEnderecoDTO clienteEnderecoDTO)
        {
            var clienteEndereco = dbContext.TbClienteEnderecos.Find(id);
            if (clienteEndereco != null)
            {
                clienteEndereco = _mapper.Map<TbClienteEndereco>(clienteEnderecoDTO);

                dbContext.TbClienteEnderecos.Update(clienteEndereco);
                dbContext.SaveChanges();

                return Ok(clienteEndereco);
            }
            else
            {
                return NotFound(id);
            }
        }

        [HttpDelete]
        //[Authorize]
        // GET: TbAcessosController/Delete/5
        public ActionResult Delete(int id)
        {
            var clienteEndereco = dbContext.TbClienteEnderecos.Find(id);
            if (clienteEndereco != null)
            { 
                dbContext.TbClienteEnderecos.Remove(clienteEndereco);
                dbContext.SaveChanges();

                return Ok(clienteEndereco);
            }
            else
            {
                return NotFound(id);
            }
        }

        private bool RetornarTipoEnderecoExistente(TbClienteEnderecoDTO clienteEndereco)
        {
            try
            {
                List<int> cliEnd = dbContext.TbClienteEnderecos.Where(a => a.ClienteId == clienteEndereco.ClienteId).Select(a => a.EnderecoId).ToList();
                foreach (int tbEnd in cliEnd)
                {
                    if (dbContext.TbEnderecos.Where(b => b.TipoEndereco == clienteEndereco.Endereco.TipoEndereco).Any())
                    {
                        return false;
                    }
                }

                return true;

            }catch(Exception e)
            {
                throw new Exception("Problema com o endereço passado! Não foi possível validá-lo...");
            }
        }

        private bool RetornarCpfExistente(TbClienteEnderecoDTO cliente)
        {
            try
            {
                if (dbContext.TbClientes.Where(a => a.Cpf == cliente.Cliente.Cpf).Any())
                    return false;
                else
                    return true;
            }catch(Exception e)
            {
                throw new Exception("Problemas com as informações do usuário, não conseguiu validar asinformações!");
            }
        }

        private enum Codigo_Empresa
        {
            Carrefour = 1,
            Atacadao = 2
        }

        private enum TipoEndereco
        {
            Residencial = 1,
            Comercial = 2,
            Outros = 3
        }

        private bool RetornarCodigoEmpresaValido(TbClienteEnderecoDTO cliente)
        {
            if (cliente.Cliente.CodEmpresa == (int)Codigo_Empresa.Atacadao || cliente.Cliente.CodEmpresa == (int)Codigo_Empresa.Atacadao)
                return true;
            else
                return false;
        }

        private bool RetornarTipoEnderecoValido(TbClienteEnderecoDTO cliente)
        {
            if (cliente.Endereco.TipoEndereco == (int)TipoEndereco.Residencial || cliente.Endereco.TipoEndereco == (int)TipoEndereco.Comercial || cliente.Endereco.TipoEndereco == (int)TipoEndereco.Outros)
                return true;
            else
                return false;
        }
    }
}
