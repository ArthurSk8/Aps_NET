using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCatalogo.Application.Services;
using SistemaCatalogo.Application.DTOs;
using System.Threading.Tasks;
using System.Linq;

namespace SistemaCatalogo.WebUI.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;

        // Injeção de Dependência dos dois serviços necessários
        public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        // Método auxiliar para popular o Dropdown de Categorias
        private async Task PopulateCategoriasDropDownList()
        {
            var categorias = await _categoriaService.GetAllCategoriasAsync();
            
            // Converte a lista de DTOs para SelectList (necessário para a View)
            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nome");
        }
        
        // GET: Produtos/Index (Listar todos os produtos) - Requisito 5
        public async Task<IActionResult> Index()
        {
            // Inicialmente, carrega todos os produtos
            var produtos = await _produtoService.GetAllProdutosAsync();
            return View(produtos);
        }

        // GET: Produtos/SearchAjax - Requisito 7: Busca Dinâmica com AJAX
        [HttpGet]
        public async Task<IActionResult> SearchAjax(string searchTerm)
        {
            // O serviço de aplicação lida com a lógica de busca
            var produtos = await _produtoService.SearchProdutosAsync(searchTerm);
            
            // Retorna uma View Parcial para atualizar apenas o conteúdo da tabela
            return PartialView("_ProdutoListPartial", produtos);
        }

        // GET: Produtos/Create (Exibir formulário de criação) - Requisito 5
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriasDropDownList(); // Popula o Dropdown
            return View();
        }

        // POST: Produtos/Create (Criar um novo produto) - Requisito 5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoCreateUpdateDTO produtoDto)
        {
            // Requisito 6: Validação de entrada
            if (ModelState.IsValid)
            {
                await _produtoService.AddProdutoAsync(produtoDto);
                return RedirectToAction(nameof(Index));
            }
            
            // Se a validação falhar, repopula o Dropdown antes de retornar a View
            await PopulateCategoriasDropDownList();
            return View(produtoDto);
        }
        
        // GET: Produtos/Edit/5 (Exibir formulário de edição) - Requisito 5
        public async Task<IActionResult> Edit(int id)
        {
            var produtoView = await _produtoService.GetProdutoByIdAsync(id);
            if (produtoView == null) return NotFound();

            // Mapeia manualmente o DTO de Visualização para DTO de Edição
            var produtoDto = new ProdutoCreateUpdateDTO
            {
                Nome = produtoView.Nome,
                Preco = produtoView.Preco,
                Descricao = produtoView.Descricao,
                CategoriaId = produtoView.CategoriaId
            };

            await PopulateCategoriasDropDownList();
            // Passa o DTO de Edição para a View, que será preenchido com os dados
            return View(produtoDto);
        }

        // POST: Produtos/Edit/5 (Salvar edição) - Requisito 5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProdutoCreateUpdateDTO produtoDto)
        {
            if (id <= 0) return NotFound();

            if (ModelState.IsValid)
            {
                await _produtoService.UpdateProdutoAsync(id, produtoDto);
                return RedirectToAction(nameof(Index));
            }

            await PopulateCategoriasDropDownList();
            return View(produtoDto);
        }

        // POST: Produtos/Delete/5 (Excluir um produto) - Requisito 5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _produtoService.DeleteProdutoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}