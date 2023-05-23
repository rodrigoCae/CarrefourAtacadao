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
    public class CidadeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarrefourAtacadaoContext dbContext;

        public CidadeController(CarrefourAtacadaoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this._mapper = mapper;
        }

        // GET: TbAcessosController
        [HttpGet]
        //[Authorize]
        public ActionResult Get()
        {
            var cidade = dbContext.TbCidades.ToList();
            if (cidade != null)
                return Ok(cidade);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize]
        // GET: TbAcessosController/Details/5
        public ActionResult Details([FromRoute] int id)
        {
            var cidade = dbContext.TbCidades.Find(id);
            
            if (cidade != null)
            {
                return Ok(cidade);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        //[Authorize]
        // POST: TbAcessosController/Create
        public ActionResult Create([FromBody] TbCidadeDTO cidadeDTO)
        {
            
            TbCidade cidade = _mapper.Map<TbCidade>(cidadeDTO);

            dbContext.TbCidades.Add(cidade);
            dbContext.SaveChanges();

            return Ok(cidade);
        }

        [HttpPut]
        //[Authorize]
        // GET: TbAcessosController/Edit/5
        public ActionResult Edit(int id, [FromBody] TbCidadeDTO cidadeDTO)
        {
            var cidade = dbContext.TbCidades.Find(id);
            if (cidade != null)
            {
                //cidade = _mapper.Map<TbCidade>(cidadeDTO);
                cidade.Nome = cidadeDTO.Nome;
                cidade.Estado = cidadeDTO.Estado;


                //dbContext.TbCidades.Update(cidade);
                dbContext.SaveChanges();

                return Ok(cidade);
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
            var cidade = dbContext.TbCidades.Find(id);
            if (cidade != null)
            { 
                dbContext.TbCidades.Remove(cidade);
                dbContext.SaveChanges();

                return Ok(cidade);
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
