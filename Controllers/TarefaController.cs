using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Cadastro.Tarefas.Models;
using Senai.Cadastro.Tarefas.Repositorios;

namespace Senai.Cadastro.Tarefas.Controllers {
    public class TarefaController : Controller {
        [HttpGet]

        public IActionResult Cadastrar () {

            if (string.IsNullOrEmpty (HttpContext.Session.GetString ("idUsuario"))) {
                return RedirectToAction ("Login", "Usuario");
            }

            return View ();
        }

        public IActionResult Cadastrar (IFormCollection form) {

            TarefaModel tarefa = new TarefaModel {
                Id = 1,
                Nome = form["nome"],
                Descricao = form["descricao"],
                Tipo = form["tipo"],
            };

            tarefa.DataCriacao = DateTime.Now;

            TarefaRepositorio tarefaRap = new TarefaRepositorio();
            
            List<TarefaModel> lsTarefas = tarefaRap.CarregarTarefaCSV();
            tarefa.Id = lsTarefas.Count + 1;

            using (StreamWriter escrever = new StreamWriter ("tarefas.csv", true)) {
                escrever.WriteLine ($"{tarefa.Id};{tarefa.Nome};{tarefa.Descricao};{tarefa.Tipo};{tarefa.DataCriacao}");
            }

            ViewBag.Mensagem = "Tarefa cadastrada com sucesso";

            return View ();
        }
    }
}