using INFNETPBVENDADECARROS.Models;
using INFNETPBVENDADECARROS.Services.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INFNETPBVENDADECARROS.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly VeiculoService _service;

        public DetailsModel(VeiculoService service)
        {
            _service = service;
        }

        [BindProperty]
        public Veiculo Veiculo { get; set; }

        public IActionResult OnGet(int id)
        {
            Veiculo = _service.Obter(id);

            if (Veiculo == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostExcluir(int id)
        {
            _service.Excluir(id);
            return RedirectToPage("Index");
        }
    }
}

