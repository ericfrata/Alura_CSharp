using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriasDAO dao = new CategoriasDAO(); 

        // GET: Categoria
        public ActionResult Index()
        {
            IList<CategoriaDoProduto> categorias = dao.Lista();
            ViewBag.Categorias = categorias;
            return View();
        }

        
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Adiciona(CategoriaDoProduto Categoria)
        {
            CategoriasDAO dao = new CategoriasDAO();
            dao.Adiciona(Categoria);

            return RedirectToAction("Index", "Categoria");
        }
    }
}