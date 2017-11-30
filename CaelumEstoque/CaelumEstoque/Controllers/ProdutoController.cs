using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using CaelumEstoque.Filtro;

namespace CaelumEstoque.Controllers
{
    [AutorizacaoFilter]

    public class ProdutoController : Controller
    {

        [Route("produtos", Name ="ListaProdutos")]
        // GET: Produto
        public ActionResult Index()
        {
            
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            return View(produtos);
        }

        public ActionResult Form()
        {
            ViewBag.Produto = new Produto();
            CategoriasDAO CategoriaDao = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = CategoriaDao.Lista();
            ViewBag.Categorias = categorias;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adiciona(Produto produto)
        {
            int idDaInformatica = 1;

            if(produto.CategoriaId.Equals(idDaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.Invalido","Informática com preço abaixo de 100 reais");
            }

            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Produto = produto;
                CategoriasDAO dao = new CategoriasDAO();
                ViewBag.Categorias = dao.Lista();
                return View("Form");
            }

        }

        [Route("produtos/{id}", Name = "VisualizaProduto")]

        public ActionResult Visualiza (int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            ViewBag.Produto = produto;
            return View(); 
        }

        public ActionResult DecrementaQtd(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            return Json(produto);
        }
    }
}