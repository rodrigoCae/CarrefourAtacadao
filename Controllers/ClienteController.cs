using AutoMapper;
using Carrefour_Atacadao_BackEnd.Context;
using Carrefour_Atacadao_BackEnd.Entites;
using Carrefour_Atacadao_BackEnd.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Carrefour_Atacadao_BackEnd.Enum;
using Microsoft.Identity.Client;

namespace Carrefour_Atacadao_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarrefourAtacadaoContext dbContext;

        public ClienteController(CarrefourAtacadaoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this._mapper = mapper;
        }

        // GET: TbAcessosController
        [HttpGet]
        //[Authorize]
        public ActionResult Get()
        {
            var cliente = dbContext.TbClientes.ToList();
            if (cliente != null)
                return Ok(cliente);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize]
        // GET: TbAcessosController/Details/5
        public ActionResult Details([FromRoute] int id)
        {
            var cliente = dbContext.TbClientes.Find(id);
            
            if (cliente != null)
            {
                return Ok(cliente);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        //[Authorize]
        // POST: TbAcessosController/Create
        public ActionResult Create([FromBody] TbClienteDTO clienteDTO)
        {
            try
            {
                if (RetornarCpfExistente(clienteDTO) && RetornarCodigoEmpresaValido(clienteDTO))
                {
                    TbCliente cliente = _mapper.Map<TbCliente>(clienteDTO);

                    dbContext.TbClientes.Add(cliente);
                    dbContext.SaveChanges();

                    return Ok(cliente);
                }
                else
                {
                    return NotFound("Já existe um usuário cadastrado com esse CPF para esta empresa ou o codigo da empresa não foi aceito!");
                }
            }catch(Exception ex) 
            {
                throw new Exception("Ocorreram erros ao executar o comando de salvar,tente novamente!");            
            }
        }

        [HttpPut]
        //[Authorize]
        // GET: TbAcessosController/Edit/5
        public ActionResult Edit(int id, [FromBody] TbClienteDTO clienteDTO)
        {
            var cliente = dbContext.TbClientes.Find(id);
            if (cliente != null)
            {
                cliente = _mapper.Map<TbCliente>(clienteDTO);

                dbContext.TbClientes.Update(cliente);
                dbContext.SaveChanges();

                return Ok(cliente);
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
            var cliente = dbContext.TbClientes.Find(id);
            if (cliente != null)
            { 
                dbContext.TbClientes.Remove(cliente);
                dbContext.SaveChanges();

                return Ok(cliente);
            }
            else
            {
                return NotFound(id);
            }
        }


        private bool RetornarCpfExistente(TbClienteDTO cliente)
        {
            if (dbContext.TbClientes.Where(a => a.Cpf == cliente.Cpf && a.CodEmpresa == cliente.CodEmpresa).Any())
                return false;
            else
                return true;
        }

        private enum Codigo_Empresa
        {
            Carrefour = 1,
            Atacadao = 2
        }

        private bool RetornarCodigoEmpresaValido(TbClienteDTO cliente)
        {
            if (cliente.CodEmpresa == (int)Codigo_Empresa.Atacadao || cliente.CodEmpresa == (int)Codigo_Empresa.Atacadao)
                return true;
            else
                return false;
        }
    }
}
