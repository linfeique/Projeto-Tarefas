using System;
using System.Collections.Generic;
using System.IO;
using Senai.Cadastro.Tarefas.Models;

namespace Senai.Cadastro.Tarefas.Repositorios
{
    public class TarefaRepositorio
    {
        public List<TarefaModel> CarregarTarefaCSV(){

            List<TarefaModel> lsTarefas = new List<TarefaModel>();

            string[] linhas = File.ReadAllLines("tarefas.csv");

            foreach (string linha in linhas)
            {
                string[] dados = linha.Split(";");

                TarefaModel tarefa = new TarefaModel{
                    Id = int.Parse(dados[0]),
                    Nome = dados[1],
                    Descricao = dados[2],
                    Tipo = dados[3],
                    DataCriacao = DateTime.Parse(dados[4])
                };

                lsTarefas.Add(tarefa);
            }

            return lsTarefas;
        }
    }
}