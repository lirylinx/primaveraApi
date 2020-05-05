﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Json;
using primaveraApi.modelo;

namespace primaveraApi.crud
{
    public class UsuarioCRUD
    {
        Basedados bd;

        String[] colunas = new String[] { "CDU_utilizador", "CDU_nome", "CDU_senha", "CDU_documento", "CDU_perfil", "Vendedor", "Nome", "Telefone", "Morada" };
        List<object[]> resultado = new List<object[]>();
        Usuario usuario = new Usuario();
        Vendedor vendedor = new Vendedor();
        List<Usuario> usuario_lista = new List<Usuario>();
        String sql_select = "";

        public UsuarioCRUD()
        {
            this.bd = new Basedados();
            sql_select = " SELECT " + string.Join(",", colunas) + " FROM TDU_primobUtilizador as user, Vendedores as vend where LEN(CDU_nome) > 2 and LEN(CDU_senha) >= 4 and user.CDU_vendedor = vend.vendedor; ";
        }

        public List<Usuario> read()
        {
            List<Usuario> usuario_lista = new List<Usuario>();
            resultado = this.bd.GetObjecto(this.sql_select, colunas.Length);
            //resultado.ForEach();


            foreach (object[] obj in resultado)
            {
                vendedor = new Vendedor(obj[5].ToString(), obj[6].ToString(), obj[7].ToString());
                usuario = new Usuario(obj[0].ToString(), obj[1].ToString(), obj[2].ToString(), obj[3].ToString(), obj[4].ToString(), vendedor);
                usuario_lista.Add(usuario);
            }



            return usuario_lista;
        }
        public Usuario readById(String utilizador_id)
        {


            Usuario usuario = null;
            String sql = " ";
            sql += "select  " + string.Join(",", colunas) + " from TDU_primobUtilizador where CDU_utilizador = '" + utilizador_id + "'";
            resultado = this.bd.GetObjecto(sql, colunas.Length);
            if (resultado.Count > 0)
            {
                object[] obj = resultado[0];
                vendedor = new Vendedor(obj[5].ToString(), obj[6].ToString(), obj[7].ToString());
                usuario = new Usuario(obj[0].ToString(), obj[1].ToString(), obj[2].ToString(), obj[3].ToString(), obj[4].ToString(), vendedor);
            }
            else
            {
                usuario = null;
            }
            return usuario;

        }

        public Usuario readByNome(String utilizador_nome)
        {


            Usuario usuario = null;
            String sql = " ";
            sql += "select  " + string.Join(",", colunas) + " from TDU_primobUtilizador where CDU_nome = '" + utilizador_nome + "'";
            resultado = this.bd.GetObjecto(sql, colunas.Length);
            if (resultado.Count > 0)
            {
                object[] obj = resultado[0];
                vendedor = new Vendedor(obj[5].ToString(), obj[6].ToString(), obj[7].ToString());
                usuario = new Usuario(obj[0].ToString(), obj[1].ToString(), obj[2].ToString(), obj[3].ToString(), obj[4].ToString(), vendedor);
            }
            else
            {
                usuario = null;
            }
            return usuario;

        }
        public bool create(Usuario usuario)
        {
            bool rv = false ;
            String sql = "insert into TDU_primobUtilizador (CDU_nome, CDU_senha, CDU_documento, CDU_perfil, CDU_vendedor) " +
                         "VALUES ('" + usuario.nome + "', '" + usuario.senha + "', '" + usuario.documento + "', '" + usuario.nivel + "', '" + usuario.vendedor.vendedor + "') ";


            rv = this.bd.ExecuteNonQuery(sql);
     
            return rv;
        }


        public bool update(Usuario usuario)
        {
            bool rv = false;
            String sql = "UPDATE TDU_primobUtilizador set CDU_nome = '" + usuario.nome + "', CDU_senha = '" + usuario.senha + "', CDU_documento = '" + usuario.documento + "', CDU_perfil = '" + usuario.nivel +     "' where CDU_utilizador = '" + usuario.usuario + "' ";
            rv = this.bd.ExecuteNonQuery(sql);
            return rv;
        }

        public bool delete(Usuario usuario)
        {
            bool rv;
            String sql = " ";
            sql += "delete from TDU_primobUtilizador where CDU_utilizador = '"+ usuario.usuario +"'";
            rv = this.bd.ExecuteNonQuery(sql);
            return rv;

        }

        public Usuario login(String nome, String senha)
        {


            Usuario usuario = null;
            String sql = " ";
            sql += "select top 1 " + string.Join(",", colunas) + " from TDU_primobUtilizador, Vendedores where CDU_nome = '" + nome + "' and CDU_senha = '"+ senha + "' and CDU_vendedor = vendedor";
            resultado = this.bd.GetObjecto(sql, colunas.Length);
            if (resultado.Count > 0)
            {
                object[] obj = resultado[0];
                vendedor = new Vendedor(obj[5].ToString(), obj[6].ToString(), obj[7].ToString());
                usuario = new Usuario(obj[0].ToString(), obj[1].ToString(), obj[2].ToString(), obj[3].ToString(), obj[4].ToString(), vendedor);
            } else
            {
                usuario = null;
            }
            return usuario;

        }



    }

}
