using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SamuraiApp.Data;
using System;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISamuraiData samuraiData;


        public QuoteController(IMapper mapper, ISamuraiData samuraiData)
        {
            this.mapper = mapper;
            this.samuraiData = samuraiData;

        }
        [HttpGet]
        public async Task<ActionResult<QuoteModel[]>> Get()
        {
            try
            {
                var result = await samuraiData.GetAllQuoteAsync();
                return mapper.Map<QuoteModel[]>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuoteModel>> Get(int id)
        {
            try
            {
                var result = await samuraiData.GetSamuraiByIdAsync(id);
                if (result == null) return NotFound();
                return mapper.Map<QuoteModel>(result);


            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Databse Failiure");
            }
        }


    }
}
