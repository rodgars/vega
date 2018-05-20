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
using Microsoft.AspNetCore.Authorization;

namespace Vega.Controllers
{
    public class FeatureController : Controller
    {
        private VegaDbContext context;
        private readonly IMapper mapper;
        public FeatureController(VegaDbContext _context, IMapper mapper)
        {
            this.mapper = mapper;
            context = _context;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();
            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }
    }
}