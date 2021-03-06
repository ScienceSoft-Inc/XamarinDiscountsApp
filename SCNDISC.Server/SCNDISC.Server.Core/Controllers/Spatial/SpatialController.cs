﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SCNDISC.Server.Application.Services.Spatial;
using SCNDISC.Server.Domain.Aggregates.Partners;

namespace SCNDISC.Server.Core.Controllers.Spatial
{
    public class SpatialController : ControllerBase
    {
        private const int DefaultProximity = 100;
        private readonly ISpatialApplicationService _spatialApplicationService;

        public SpatialController(ISpatialApplicationService spatialApplicationService)
        {
            _spatialApplicationService = spatialApplicationService;
        }
        //TODO
        [Route("spatial/discounts/{syncdate?}")]
        [HttpGet]
        public async Task<IEnumerable<Branch>> GetAllDiscountsAsync([FromQuery]DateTime? syncdate = null)
        {
            return await _spatialApplicationService.GetSpatialDiscountsDataAsync(syncdate);
        }

        [Route("spatial/lat/{latitude}/long/{longitude}/proximity/{proximity}/discounts/")]
        [HttpGet]
        public async Task<IEnumerable<Branch>> GetAllDiscountsInProximityAsync(double longitude, double latitude, int proximity = DefaultProximity)
        {
            return await _spatialApplicationService.GetAllDiscountsInProximityAsync(longitude, latitude, proximity);
        }

    }
}
