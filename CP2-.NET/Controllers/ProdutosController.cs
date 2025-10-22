using CP2_.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace CP2_.NET.Controllers
{
    public class ProdutosController : Controller
    {
        private static List<Funcionario> funcionarios = new()
        {
            new Funcionario { IdFuncionario = 1, Nome = "Vinicius Kenzo", Email = "vinicius@estoque.com", Cargo = "Gerente" },
            new Funcionario { IdFuncionario = 2, Nome = "João Victor", Email = "joao@estoque.com", Cargo = "Diretor" },
            new Funcionario { IdFuncionario = 3, Nome = "Lucas Chicote", Email = "chicote@estoque.com", Cargo = "Supervisor" },
            new Funcionario { IdFuncionario = 4, Nome = "Lucas Gomes", Email = "gomes@estoque.com", Cargo = "Coordenador" },
            new Funcionario { IdFuncionario = 5, Nome = "Marcel", Email = "marcel@estoque.com", Cargo = "CEO" }
        };

        private static List<Produto> produtos = new();
        private static int contador = 1;

        public IActionResult ListarProdutos(string busca)
        {
            var lista = string.IsNullOrEmpty(busca)
                ? produtos
                : produtos.Where(p => p.Nome.Contains(busca, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Funcionarios = funcionarios;
            return View(lista);
        }

        public IActionResult CadastrarProduto()
        {
            ViewBag.Funcionarios = funcionarios;
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            produto.IdProduto = contador++;
            produto.DataCadastro = DateTime.Now;
            produto.IdFuncionario = produto.IdFuncionario;
            produtos.Add(produto);
            return RedirectToAction("ListarProdutos");
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.IdProduto == id);
            ViewBag.Funcionarios = funcionarios;
            return View(produto);
        }

        [HttpPost]
        public IActionResult EditarProduto(Produto produto)
        {
            var p = produtos.FirstOrDefault(x => x.IdProduto == produto.IdProduto);
            if (p != null)
            {
                p.Nome = produto.Nome;
                p.Categoria = produto.Categoria;
                p.Quantidade = produto.Quantidade;
                p.Preco = produto.Preco;
                p.IdFuncionario = produto.IdFuncionario;
            }
            return RedirectToAction("ListarProdutos");
        }

        public IActionResult DetalharProduto(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.IdProduto == id);
            ViewBag.Funcionarios = funcionarios;
            return View(produto);
        }

        public IActionResult DeletarProduto(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.IdProduto == id);
            return View(produto);
        }

        [HttpPost, ActionName("DeletarProduto")]
        public IActionResult ConfirmarDelete(int id)
        {
            produtos.RemoveAll(p => p.IdProduto == id);
            return RedirectToAction("ListarProdutos");
        }
    }
}
