using APS2.Application.Services;
using APS2.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APS2.Presentation.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ProdutoService _produtoService;
        private readonly CategoriaService _categoriaService;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(ProdutoService produtoService, CategoriaService categoriaService, ILogger<ProdutosController> logger)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? search)
        {
            try
            {
                IEnumerable<ProdutoViewModel> produtos;

                if (!string.IsNullOrEmpty(search))
                {
                    produtos = await _produtoService.BuscarPorNomeAsync(search);
                }
                else
                {
                    produtos = await _produtoService.ObterTodosAsync();
                }

                return View(produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter produtos");
                TempData["ErrorMessage"] = "Erro ao carregar produtos.";
                return View(new List<ProdutoViewModel>());
            }
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategoryDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoryDropdown(viewModel.CategoriaId);
                return View(viewModel);
            }

            try
            {
                await _produtoService.CriarAsync(viewModel);
                TempData["SuccessMessage"] = "Produto criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validação ao criar produto");
                ModelState.AddModelError("", ex.Message);
                await PopulateCategoryDropdown(viewModel.CategoriaId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar produto");
                ModelState.AddModelError("", "Erro ao criar produto.");
                await PopulateCategoryDropdown(viewModel.CategoriaId);
                return View(viewModel);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);
            if (produto == null)
            {
                TempData["ErrorMessage"] = "Produto não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateCategoryDropdown(produto.CategoriaId);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProdutoViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                TempData["ErrorMessage"] = "ID inválido.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                await PopulateCategoryDropdown(viewModel.CategoriaId);
                return View(viewModel);
            }

            try
            {
                var resultado = await _produtoService.AtualizarAsync(id, viewModel);
                if (resultado == null)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = "Produto atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validação ao atualizar produto");
                ModelState.AddModelError("", ex.Message);
                await PopulateCategoryDropdown(viewModel.CategoriaId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar produto");
                ModelState.AddModelError("", "Erro ao atualizar produto.");
                await PopulateCategoryDropdown(viewModel.CategoriaId);
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sucesso = await _produtoService.RemoverAsync(id);
                if (!sucesso)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = "Produto deletado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar produto");
                TempData["ErrorMessage"] = "Erro ao deletar produto.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Json(new { success = false, message = "Consulta vazia." });
            }

            try
            {
                var produtos = await _produtoService.BuscarPorNomeAsync(query);
                return Json(new { success = true, data = produtos });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produtos");
                return Json(new { success = false, message = "Erro ao buscar." });
            }
        }

        private async Task PopulateCategoryDropdown(int selectedId = 0)
        {
            var categorias = await _categoriaService.ObterTodosAsync();
            ViewBag.Categories = new SelectList(categorias, "Id", "Nome", selectedId);
        }
    }
}
