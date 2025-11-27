using APS2.Application.Services;
using APS2.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace APS2.Presentation.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaService _service;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(CategoriaService service, ILogger<CategoriasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? search)
        {
            try
            {
                IEnumerable<CategoriaViewModel> categorias;

                if (!string.IsNullOrEmpty(search))
                {
                    categorias = await _service.BuscarPorNomeAsync(search);
                }
                else
                {
                    categorias = await _service.ObterTodosAsync();
                }

                return View(categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter categorias");
                TempData["ErrorMessage"] = "Erro ao carregar categorias.";
                return View(new List<CategoriaViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await _service.CriarAsync(viewModel);
                TempData["SuccessMessage"] = "Categoria criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar categoria");
                ModelState.AddModelError("", "Erro ao criar categoria.");
                return View(viewModel);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _service.ObterPorIdAsync(id);
            if (categoria == null)
            {
                TempData["ErrorMessage"] = "Categoria não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                TempData["ErrorMessage"] = "ID inválido.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var resultado = await _service.AtualizarAsync(id, viewModel);
                if (resultado == null)
                {
                    TempData["ErrorMessage"] = "Categoria não encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = "Categoria atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar categoria");
                ModelState.AddModelError("", "Erro ao atualizar categoria.");
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sucesso = await _service.RemoverAsync(id);
                if (!sucesso)
                {
                    TempData["ErrorMessage"] = "Categoria não encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = "Categoria deletada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar categoria");
                TempData["ErrorMessage"] = "Erro ao deletar categoria.";
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
                var categorias = await _service.BuscarPorNomeAsync(query);
                return Json(new { success = true, data = categorias });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar categorias");
                return Json(new { success = false, message = "Erro ao buscar." });
            }
        }
    }
}
