using System.Collections.Generic;
using Senai.Cadastro.Tarefas.Models;
using System.IO;
using System;

namespace Senai.Cadastro.Tarefas.Repositorios
{
    public class UsuarioRepositorio
    {
        public List<UsuarioModel> CarregarDoCSV(){

            List<UsuarioModel> lsUsuarios = new List<UsuarioModel>();

            string[] linhas = File.ReadAllLines("usuarios.csv");

            foreach (string linha in linhas)
            {
                string[] dados = linha.Split(";");

                UsuarioModel usuarioModel = new UsuarioModel{
                    Id = int.Parse(dados[0]),
                    Nome = dados[1],
                    Email = dados[2],
                    Senha = dados[3],
                    Tipo = dados[4],
                    DataCriacao = DateTime.Parse(dados[5])
                };

                lsUsuarios.Add(usuarioModel);
            }

            return lsUsuarios;
        }

        public UsuarioModel BuscarEmailSenha (string email, string senha){
            List<UsuarioModel> lsUsuarios = CarregarDoCSV();

            foreach (UsuarioModel item in lsUsuarios)
            {
                if(item.Email == email && item.Senha == senha){
                    return item;
                }
            }

            return null;
        }
    }
}