using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Tarefas.Models;
using Tarefas.Models.ViewModels;

namespace Tarefas.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Config { get; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            Config = config;
        }

        public IActionResult Index()
        {
            var tarefaViewModel = GetAllTarefas();
            return View(tarefaViewModel);
        }

        [HttpGet]
        public JsonResult PopulateForm(int id)
        {
            var tarefa = GetById(id);
            return Json(tarefa);
        }

        internal TarefaViewModel GetAllTarefas()
        {
            List<TarefaItem> tarefaList = new();

            using (SqlConnection con = new())
            {
                con.ConnectionString = Config.GetConnectionString("DefaultConnection");
                con.Open();

                using (SqlCommand cmd = new())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "EXEC TarefasDB.dbo.SelectTarefas ";

                    SqlDataReader rd = cmd.ExecuteReader();
                    
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            tarefaList.Add(
                                new TarefaItem
                                {
                                    Id = rd.GetInt32(0),
                                    Descricao = rd.GetString(1),
                                    DtCriacao = rd.GetString(2)
                                });
                        }
                    }
                    else
                    {
                        con.Close();
                        return new TarefaViewModel
                        {
                            TarefaList = tarefaList
                        };
                    }
                }
                con.Close();
                return new TarefaViewModel
                {
                    TarefaList = tarefaList
                };
            }
        }

        internal TarefaViewModel GetById(int id)
        {
            TarefaViewModel tarefaItem = new();
            TarefaItem tarefa = new();

            tarefaItem.Tarefa = tarefa;

            using (SqlConnection con = new())
            {
                con.ConnectionString = Config.GetConnectionString("DefaultConnection");
                con.Open();

                using (SqlCommand cmd = new())
                {
                    SqlParameter parm = new();

                    cmd.Connection = con;
                    cmd.CommandText = "EXEC TarefasDB.dbo.SelectTarefa @Id = @Id ";

                    parm.ParameterName = "@Id";
                    parm.SqlDbType = SqlDbType.Int;
                    parm.Value = id;

                    cmd.Parameters.Add(parm);

                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.HasRows)
                    {
                        if (rd.Read())
                        {
                            tarefaItem.Tarefa.Id = rd.GetInt32(0);
                            tarefaItem.Tarefa.Descricao = rd.GetString(1);
                            tarefaItem.Tarefa.DtCriacao = rd.GetString(2);
                        }
                    }
                    else
                    {
                        con.Close();
                        return tarefaItem;                      
                    }
                }

                return tarefaItem;
            }
        }

        public RedirectResult Insert(TarefaViewModel tarefasItem)
        {
            using (SqlConnection con = new())
            {
                con.ConnectionString = Config.GetConnectionString("DefaultConnection");
                con.Open();

                using (SqlCommand cmd = new())
                {
                    SqlParameter parm = new();

                    cmd.Connection = con;
                    cmd.CommandText = "EXEC TarefasDB.dbo.InsertTarefa @Descricao = @Descricao ";

                    parm.ParameterName = "@Descricao";
                    parm.SqlDbType = SqlDbType.NVarChar;
                    parm.Value = tarefasItem.Tarefa.Descricao;

                    cmd.Parameters.Add(parm);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();
                    return Redirect("http://localhost:5000/");
                }
            }
        }

        public RedirectResult Update(TarefaViewModel tarefasItem)
        {
            using (SqlConnection con = new())
            {
                con.ConnectionString = Config.GetConnectionString("DefaultConnection");
                con.Open();

                using (SqlCommand cmd = new())
                {
                    SqlParameter parm = new();

                    cmd.Connection = con;
                    cmd.CommandText = "EXEC TarefasDB.dbo.UpdateTarefa @Id = @Id, @Desc = @Descricao ";

                    cmd.Parameters.AddRange(
                        new SqlParameter[]
                        {
                            new SqlParameter("@Id", SqlDbType.Int) { Value = tarefasItem.Tarefa.Id },
                            new SqlParameter("@Descricao", SqlDbType.NVarChar) { Value = tarefasItem.Tarefa.Descricao }
                        });

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();
                    return Redirect("http://localhost:5000/");
                }
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (SqlConnection con = new())
            {
                con.ConnectionString = Config.GetConnectionString("DefaultConnection");
                con.Open();

                using (SqlCommand cmd = new())
                {
                    SqlParameter parm = new();

                    cmd.Connection = con;
                    cmd.CommandText = "EXEC TarefasDB.dbo.DeleteTarefa @Id = @Id ";

                    parm.ParameterName = "@Id";
                    parm.SqlDbType = SqlDbType.NVarChar;
                    parm.Value = id;

                    cmd.Parameters.Add(parm);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();
                    return Json(new { });
                }
            }
        }
    }
}
