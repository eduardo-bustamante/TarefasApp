using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Tarefas.Models;

namespace Tarefas.Controllers
{
    public class HomeController : Controller
    {
        private TarefaContext context;

        public HomeController(TarefaContext ctx) => context = ctx;

        public IActionResult Index(string id)
        {
            var filtros = new Filtros(id);
            ViewBag.Filtros = filtros;

            ViewBag.Categorias = context.Categorias.ToList();
            ViewBag.Condicoes = context.Condicoes.ToList();
            ViewBag.VencimentoFiltro = Filtros.VencimentoFiltroValores;

            IQueryable<Tarefa> query = context.Tarefas
                .Include(t => t.Categoria)
                .Include(t => t.Condicao);

            if (filtros.TemCategoria)
            {
                query = query.Where(t => t.CategoriaId == filtros.CategoriaId);
            }

            if (filtros.TemCondicao)
            {
                query = query.Where(t => t.CondicaoId == filtros.CondicaoId);
            }

            if (filtros.TemVencimento)
            {
                var hoje = DateTime.Today;
                if (filtros.EhPassado)
                {
                    query = query.Where(t => t.DataDeVencimento < hoje);
                }
                else if (filtros.EhFuturo)
                {
                    query = query.Where(t => t.DataDeVencimento > hoje);
                }
                else if (filtros.EhHoje)
                {
                    query = query.Where(t => t.DataDeVencimento == hoje);
                }
            }

            var atividades = query.OrderBy(t => t.DataDeVencimento).ToList();


            return View(atividades);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categorias = context.Categorias.ToList();
            ViewBag.Condicoes = context.Condicoes.ToList();

            var atividades = new Tarefa { CondicaoId = "aberto" };
            return View(atividades);

        }

        [HttpPost]
        public IActionResult Add(Tarefa atividades)
        {
            if (ModelState.IsValid)
            {
                context.Tarefas.Add(atividades);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categorias = context.Categorias.ToList();
                ViewBag.Condicoes = context.Condicoes.ToList();
                return View(atividades);
            }
        }

        [HttpPost]
        public IActionResult Filtro(string[] filtro)
        {
            string id = string.Join('-', filtro);

            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult MarcarCompleto([FromRoute] string id, Tarefa selecionado)
        {
            selecionado = context.Tarefas.Find(selecionado.Id);

            if (selecionado != null)
            {
                selecionado.CondicaoId = "concluido";
                context.SaveChanges();
            }

            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteCompleto(string id)
        {
            var toDelete = context.Tarefas.Where(t => t.CondicaoId == "concluido").ToList();

            foreach (var atividades in toDelete)
            {
                context.Tarefas.Remove(atividades);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });


        }



    }
}
