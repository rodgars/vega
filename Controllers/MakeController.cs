using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Vega.Core.Models;
using Vega.Persistence;
using AutoMapper;
using Vega.Controllers.Resources;

namespace Vega.Controllers
{
    public class MakeController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public MakeController(VegaDbContext _context, IMapper mapper)
        {
            this.mapper = mapper;
            context = _context;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(s => s.Models).ToListAsync();

            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}