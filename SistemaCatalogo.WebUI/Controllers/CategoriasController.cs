using Microsoft.AspNetCore.Mvc;
using SistemaCatalogo.Application.Services;
using SistemaCatalogo.Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SistemaCatalogo.WebUI.Controllers
{
    // O Controller é a porta de entrada da aplicação, usando apenas a Camada de Aplicação
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        // Injeção de Dependência (DI) do Serviço de Aplicação (IOC)
        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: Categorias/Index (Listar todas as categorias) - Requisito 5
        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.GetAllCategoriasAsync();
            return View(categorias);
        }

        // GET: Categorias/Create (Exibir formulário de criação) - Requisito 5
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create (Criar uma nova categoria) - Requisito 5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaCreateUpdateDTO categoriaDto)
        {
            // Requisito 6: Validação de entrada (Data Annotations)
            if (ModelState.IsValid)
            {
                await _categoriaService.AddCategoriaAsync(categoriaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaDto);
        }
        
        // GET: Categorias/Edit/5 (Exibir formulário de edição) - Requisito 5
        public async Task<IActionResult> Edit(int id)
        {
            var categoriaView = await _categoriaService.GetCategoriaByIdAsync(id);
            if (categoriaView == null) return NotFound();

            // Mapeamos o DTO de visualização para o DTO de edição/criação
            // para preencher o formulário. Usaremos o Mapster aqui na View (ou no Controller)
            // Aqui, por simplicidade, faremos um cast direto, assumindo campos compatíveis.
            var categoriaDto = new CategoriaCreateUpdateDTO { Nome = categoriaView.Nome };
            
            return View(categoriaDto);
        }

        // POST: Categorias/Edit/5 (Salvar edição) - Requisito 5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaCreateUpdateDTO categoriaDto)
        {
            if (id <= 0) return NotFound();

            if (ModelState.IsValid)
            {
                await _categoriaService.UpdateCategoriaAsync(id, categoriaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaDto);
        }

        // POST: Categorias/Delete/5 (Excluir uma categoria) - Requisito 5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriaService.DeleteCategoriaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}