using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraiController : ControllerBase
    {
        private readonly ISamuraiData samuraiData;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public SamuraiController(ISamuraiData samuraiData,IMapper mapper,LinkGenerator linkGenerator)
        {
            this.samuraiData = samuraiData;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }
        [HttpGet]
        public async Task<ActionResult<SamuraiModel[]>> Get(bool includeQuote = false)
        {
            try
            {
                var samurai = await samuraiData.GetAllSamuraisAsync(true);
                return mapper.Map<SamuraiModel[]>(samurai);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,"Database Failiure");
            }
            
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SamuraiModel>> Get(int id, bool includeQuotes=false)
        {
            try
            {
                var result = await samuraiData.GetSamuraiByIdAsync(id,true);
                if (result == null) return NotFound();
               var c=mapper.Map<SamuraiModel>(result);
                return c;
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
        }
        [HttpPost]
        public async Task<ActionResult<SamuraiModel>> Post(SamuraiModel model)
        {
            try
            {
                var location = linkGenerator.GetPathByAction("Get", "Samurai", new { model.Id });
                var samurai = mapper.Map<Samurai>(model);
                samuraiData.Add(samurai);
                if (await samuraiData.SaveChangesAsync())
                {
                    return Created(location, mapper.Map<SamuraiModel>(samurai));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
            return BadRequest();
       


        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SamuraiModel>> Update(int id, SamuraiModel model)
        {
            try
            {
                var oldSamurai = await samuraiData.GetSamuraiByIdAsync(id);
                if (oldSamurai == null) return NotFound("Samurai is not found");
                mapper.Map(model, oldSamurai);
                if(await samuraiData.SaveChangesAsync())
                {
                    return mapper.Map<SamuraiModel>(oldSamurai);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
            return BadRequest();
           

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await samuraiData.GetSamuraiByIdAsync(id);
                if (result == null) return NotFound($"samurai with id {id} is not found");
                samuraiData.Delete(result);
                if(await samuraiData.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
            return BadRequest();
         
        }
    }
}
