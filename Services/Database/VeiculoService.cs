using INFNETPBVENDADECARROS.Data;
using INFNETPBVENDADECARROS.Models;
using Microsoft.EntityFrameworkCore;

namespace INFNETPBVENDADECARROS.Services.Database
{
    public class VeiculoService
    {
        private readonly ApplicationDbContext _context;

        public VeiculoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Incluir(Veiculo veiculo)
        {
            _context.Veiculo.Add(veiculo);
            _context.SaveChanges();
        }

        public void Alterar(Veiculo veiculo)
        {
            var veiculoOriginal = Obter(veiculo.VeiculoId);

            if (veiculoOriginal == null) throw new InvalidOperationException("Veículo não encontrado.");

            veiculoOriginal.Nome = veiculo.Nome;
            veiculoOriginal.Descricao = veiculo.Descricao;
            veiculoOriginal.Preco = veiculo.Preco;
            veiculoOriginal.EntregaExpressa = veiculo.EntregaExpressa;
            veiculoOriginal.DataCadastro = veiculo.DataCadastro;
            veiculoOriginal.ImagemUri = veiculo.ImagemUri;

            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var veiculoEncontrado = Obter(id);

            if (veiculoEncontrado == null) throw new InvalidOperationException("Veículo não encontrado.");

            _context.Veiculo.Remove(veiculoEncontrado);
            _context.SaveChanges();
        }
        public void ExcluirTodos()
        {
            _context.Veiculo.RemoveRange(_context.Veiculo);
            _context.SaveChanges();
        }

        public Veiculo Obter(int id)
        {
            return _context.Veiculo
                .AsNoTracking() 
                .SingleOrDefault(item => item.VeiculoId == id);
        }

        public IList<Veiculo> ObterTodos()
        {
            return _context.Veiculo.AsNoTracking().ToList();
        }
    }
}
