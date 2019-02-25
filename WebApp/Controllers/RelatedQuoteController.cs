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
    [Route("api/samurai/{samuraiId:int}/quotes")]
    [ApiController]
    public class RelatedQuoteController:ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISamuraiData samuraiData;
        private readonly LinkGenerator linkGenerator;

        public RelatedQuoteController(IMapper mapper,ISamuraiData samuraiData, LinkGenerator linkGenerator)
        {
            this.mapper = mapper;
            this.samuraiData = samuraiData;
            this.linkGenerator = linkGenerator;
        }
        [HttpGet]
        public async Task<ActionResult<QuoteModel[]>> Get(int samuraiId)
        {
            try
            {
                var result = await samuraiData.GetRelatedAllQuoteByIdAsync(samuraiId);
                return mapper.Map<QuoteModel[]>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
        }
        [HttpGet("{quoteId:int}")]
        public async Task<ActionResult<QuoteModel>> Get(int samuraiId, int quoteId)
        {
            try
            {
                var result = await samuraiData.GetRelatedSingelQuoteByIdAsync(samuraiId, quoteId);
                if (result == null) return NotFound();
                return mapper.Map<QuoteModel>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
        }
        [HttpPost]
        public async Task<ActionResult<QuoteModel>> Post(int samuraiId, QuoteModel model)
        {
            try
            {
                var samurai = await samuraiData.GetSamuraiByIdAsync(samuraiId);
                var quote=mapper.Map<Quote>(model);
             
                quote.Samurai = samurai;
                samuraiData.Add(quote);
                if(await samuraiData.SaveChangesAsync())
                {
                    var url = linkGenerator.GetPathByAction(HttpContext,"Get",values:new { samuraiId, id=quote.Id });
                    return Created(url, mapper.Map<QuoteModel>(quote));
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
            return BadRequest();
        }
        [HttpPut("{quoteId:int}")]
        public async Task<ActionResult<QuoteModel>> Update(int samuraiId, int quoteId,QuoteModel model)
        {
            try
            {
                var oldQuote = await samuraiData.GetRelatedSingelQuoteByIdAsync(samuraiId,quoteId);
                if (oldQuote == null) return NotFound();
                mapper.Map(model,oldQuote);
                if(await samuraiData.SaveChangesAsync())
                {
                    return mapper.Map<QuoteModel>(oldQuote);
                }


            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
            return BadRequest("Failed to update database");
        }
        [HttpDelete("{quoteId:int}")]
        public async Task<IActionResult> Delete(int samuraiId, int quoteId)
        {
            try
            {
                var result = await samuraiData.GetRelatedSingelQuoteByIdAsync(samuraiId, quoteId);
                if (result == null) return NotFound("Quote not found");
                samuraiData.Delete(result);
                if (await samuraiData.SaveChangesAsync())
                {
                    return Ok();
                }
                else return BadRequest();

            }
            catch (Exception)
            {

               return StatusCode(StatusCodes.Status500InternalServerError, "database failiure");
            }
           
          
        }
    }
}
