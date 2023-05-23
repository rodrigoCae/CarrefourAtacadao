using AutoMapper;
using Carrefour_Atacadao_BackEnd.Context;
using Carrefour_Atacadao_BackEnd.Entites;
using Carrefour_Atacadao_BackEnd.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carrefour_Atacadao_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarrefourAtacadaoContext dbContext;

        public EnderecoController(CarrefourAtacadaoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this._mapper = mapper;
        }

        // GET: TbAcessosController
        [HttpGet]
        //[Authorize]
        public ActionResult Get()
        {
            var endereco = dbContext.TbEnderecos.ToList();
            if (endereco != null)
                return Ok(endereco);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize]
        // GET: TbAcessosController/Details/5
        public ActionResult Details([FromRoute] int id)
        {
            var endereco = dbContext.TbEnderecos.Find(id);
            
            if (endereco != null)
            {
                return Ok(endereco);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        //[Authorize]
        // POST: TbAcessosController/Create
        public ActionResult Create([FromBody] TbEnderecoDTO enderecoDTO)
        {
            
            TbEndereco endereco = _mapper.Map<TbEndereco>(enderecoDTO);

            dbContext.TbEnderecos.Add(endereco);
            dbContext.SaveChanges();

            return Ok(endereco);
        }

        [HttpPut]
        //[Authorize]
        // GET: TbAcessosController/Edit/5
        public ActionResult Edit(int id, [FromBody] TbEnderecoDTO enderecoDTO)
        {
            var endereco = dbContext.TbEnderecos.Find(id);
            if (endereco != null)
            {
                //endereco = _mapper.Map<TbEndereco>(enderecoDTO);

                //dbContext.TbEnderecos.Update(endereco);

                endereco.Rua = enderecoDTO.Rua;
                endereco.Numero = enderecoDTO.Numero;
                endereco.Bairro = enderecoDTO.Bairro;
                endereco.Cep = enderecoDTO.Cep;
                endereco.Complemento = enderecoDTO.Complemento;
                endereco.TipoEndereco = enderecoDTO.TipoEndereco;
                endereco.CidadeId = enderecoDTO.CidadeId;

                dbContext.SaveChanges();

                return Ok(endereco);
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
            var endereco = dbContext.TbEnderecos.Find(id);
            if (endereco != null)
            { 
                dbContext.TbEnderecos.Remove(endereco);
                dbContext.SaveChanges();

                return Ok(endereco);
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
