using Microsoft.AspNetCore.Mvc;
using MvcCoreSeriesPersonajes.Models;
using MvcCoreSeriesPersonajes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreSeriesPersonajes.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceSeries service;

        public SeriesController(ServiceSeries service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Series()
        {
            List<Serie> series = await this.service.GetSeries();
            return View(series);
        }

        public async Task<IActionResult> DetailsSerie(int id)
        {
            Serie serie = await this.service.FindSerie(id);
            return View(serie);
        }

        public async Task<IActionResult> PersonajesSerie(int idserie)
        {
            Serie serie = await this.service.FindSerie(idserie);
            ViewData["SERIE"] = serie;
            List<Personaje> personajes = await this.service.GetPersonajesSerie(idserie);
            return View(personajes);
        }

        public async Task<IActionResult> UpdatePersonaje()
        {
            List<Serie> series = await this.service.GetSeries();
            ViewData["SERIES"] = series;
            List<Personaje> personajes = await this.service.GetPersonajes();
            return View(personajes);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonaje(int idpersonaje, int idserie)
        {
            List<Serie> series = await this.service.GetSeries();
            ViewData["SERIES"] = series;
            List<Personaje> personajes = await this.service.GetPersonajes();
            await this.service.UpdatePersonajesSerie(idpersonaje, idserie);
            return RedirectToAction("PersonajesSerie", new { idserie = idserie });
        }

        public async Task<IActionResult> Personajes()
        {
            List<Personaje> personajes = await this.service.GetPersonajes();
            return View(personajes);
        }
    }
}
