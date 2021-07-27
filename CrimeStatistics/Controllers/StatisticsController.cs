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

        [HttpGet]
        public IActionResult Index()
        {
            return View(new StatisticsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(StatisticsViewModel model)
        {
            if (!ValidateStatisticsViewModel(model))
            {
                return View(model);
            }

            var statistics = await _statisticsHandler.GetCrimeStatisticsAsync(model.Latitude.Value, model.Longitude.Value, DateTime.Parse(model.Month));

            var categories = statistics.Crimes.Select(c => c.CategoryName).Distinct().ToList();

            model.Statistics = statistics;
            model.Categories = categories;

            return View(model);
        }

        private bool ValidateStatisticsViewModel(StatisticsViewModel model)
        {
            model.Error = new List<string>();

            if (!model.Latitude.HasValue)
            {
                model.Error.Add("Latitude value is required");
            }

            if (!model.Longitude.HasValue)
            {
                model.Error.Add("Longitude value is required");
            }

            if (string.IsNullOrEmpty(model.Month))
            {
                model.Error.Add("Month value is required");
            } 
            else if (!DateTime.TryParse(model.Month, out _))
            {
                model.Error.Add("Month value is not valid");
            }

            if (model.Error.Count == 0) return true;
            return false;
        }
    }
}
