using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.Interfaces;
using Loja.Model;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PecaController : ControllerBase
    {
        private readonly IRepository _repository;
        public PecaController(IRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet]
        public async Task<IEnumerable<Peça>> Obter()
        {
            return await _repository.ObterTodosRegistros();
        }

        [HttpGet("{id}")]
        public async Task<Peça> ObterPecaPorId(string id)
        {
            return await _repository.ObterRegistroPorId(id);
        }

        [HttpPost]
        public async Task CriarNovaPeca(Peça peca)
        {
            if (ModelState.IsValid)
            {
                await _repository.AdicionarPeca(peca);
            }
        }

        [HttpPut]
        public async Task EditarPeca(Peça peca)
        {
            if (ModelState.IsValid)
            {
                await _repository.AtualizarPeca(peca);
            }
        }

        [HttpDelete]
        public async Task DeletarPeca(string id)
        {
            await _repository.DeletarPeca(id);
        }
    }
}