using CrimeStatistics.Business.StatisticsHandler;
using CrimeStatistics.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeStatistics.Controllers
{
    public class StatisticsController : Controller
    {
        public readonly IStatisticsHandler _statisticsHandler;

        public StatisticsController(IStatisticsHandler statisticsHandler)
        {
            _statisticsHandler = statisticsHandler;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var statistics = await _statisticsHandler.GetCrimeStatisticsAsync();

            var model = new StatisticsViewModel
            {
                Categories = statistics.Categories
            };

            return View(model);
        }
    }
}
