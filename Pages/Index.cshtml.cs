using INFNETPBVENDADECARROS.Models;
using INFNETPBVENDADECARROS.Services.Database;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace INFNETPBVENDADECARROS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VeiculoService _service;

        public IndexModel(VeiculoService service)
        {
            _service = service;
        }

        public IList<Veiculo> Veiculos { get; set; }

        public void OnGet()
        {
            Veiculos = _service.ObterTodos();
        }
    }
}
