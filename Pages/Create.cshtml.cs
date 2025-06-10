using INFNETPBVENDADECARROS.Models;
using INFNETPBVENDADECARROS.Services.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace INFNETPBVENDADECARROS.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        
            private VeiculoService _service;

            public CreateModel(VeiculoService veiculoService)
            {
                _service = veiculoService;
            }

            [BindProperty]
        public Veiculo Veiculo { get; set; }

        public void OnGet()
        {
       
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Veiculo.Nome == Veiculo.Descricao)
            {
                ModelState.AddModelError("Veiculo.Nome",
                    "Nome não pode ser igual a Descrição.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

           
            _service.Incluir(Veiculo);


            return RedirectToPage("Index"); 
        }
    }
}
