﻿using MeuPonto.Dados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPonto.Model
{
    //Link para duvida de data e hora
    //https://forum.imasters.com.br/topic/338129-resolvido%C2%A0pegar-apenas-a-hora-de-um-campo-datetime/
    public class Ponto
    {
        public int Id { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        public DateTime IdaAlmoco { get; set; }
        public DateTime VoltaAlmoco { get; set; }
        public int FuncionarioId { get; set; }
        public DateTime DiaSemana { get; set; }
        public string DiaSemanaString { get; set; }

        //Retorna os dados de ponto do funcionario
        public List<Ponto> RegistroFuncionario(int funcionarioId, out List<Ponto> pontoList)
        {
            Ponto ponto = null;
            pontoList = new List<Ponto>();
            DAL dados = new DAL();

            //Monta a string de pesquisa
            string sql = string.Format("SELECT * FROM PONTO WHERE FuncionarioId = {0}", funcionarioId);

            DataTable dt = dados.RetDataTable(sql);
            if (dt != null)
            {
                //Verifica se o resultado não é vazio
                if (dt.Rows.Count > 0)
                {
                    //percorre a lista de funcionarios retornados e adiciona na lista
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ponto = new Ponto();
                        ponto.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        ponto.FuncionarioId = Convert.ToInt32(dt.Rows[i]["FuncionarioId"].ToString());
                        ponto.Entrada = Convert.ToDateTime(dt.Rows[i]["Entrada"].ToString());
                        ponto.Saida = Convert.ToDateTime(dt.Rows[i]["Saida"].ToString());
                        ponto.IdaAlmoco = Convert.ToDateTime(dt.Rows[i]["IdaAlmoco"].ToString());
                        ponto.VoltaAlmoco = Convert.ToDateTime(dt.Rows[i]["VoltaAlmoco"].ToString());
                        ponto.DiaSemana = Convert.ToDateTime(dt.Rows[i]["DiaSemana"].ToString());
                        ponto.DiaSemanaString = dt.Rows[i]["DiaSemanaString"].ToString();

                        //Adiciona o objeto na lista
                        pontoList.Add(ponto);
                    }
                }
            }

            return pontoList;
        }
    }
}
