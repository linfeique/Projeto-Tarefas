using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Cadastro.Tarefas.Models;
using Senai.Cadastro.Tarefas.Repositorios;

namespace Senai.Cadastro.Tarefas.Controllers {
    public class UsuarioController : Controller {
        [HttpGet]
        public IActionResult Cadastro () {

            return View ();
        }

        [HttpPost]
        public IActionResult Cadastro (IFormCollection form) {
            UsuarioModel usuarioModel = new UsuarioModel {
                Nome = form["nome"],
                Email = form["email"],
                Senha = form["senha"],
                Tipo = form["tipo"]
            };

            usuarioModel.DataCriacao = DateTime.Now;

            UsuarioRepositorio usuarioRap = new UsuarioRepositorio ();

            List<UsuarioModel> lsUsuarios = usuarioRap.CarregarDoCSV ();
            usuarioModel.Id = lsUsuarios.Count + 1;

            using (StreamWriter escrever = new StreamWriter ("usuarios.csv", true)) {
                escrever.WriteLine ($"{usuarioModel.Id};{usuarioModel.Nome};{usuarioModel.Email};{usuarioModel.Senha};{usuarioModel.Tipo};{usuarioModel.DataCriacao}");
            }

            return View ();
        }

        [HttpGet]

        public IActionResult Login () => View ();

        [HttpPost]

        public IActionResult Login (IFormCollection form) {
            UsuarioModel usuario = new UsuarioModel {
                Email = form["email"],
                Senha = form["senha"]
            };

            UsuarioRepositorio usuarioRap = new UsuarioRepositorio ();
            UsuarioModel usuarioModel = usuarioRap.BuscarEmailSenha (usuario.Email, usuario.Senha);

            if (usuarioModel != null) {
                HttpContext.Session.SetString ("idUsuario", usuarioModel.Id.ToString ());
                return RedirectToAction("Cadastrar", "Tarefa");
            } else {
                return ViewBag.Mensagem = "Acesso Negado";
            }
        }
    }
}