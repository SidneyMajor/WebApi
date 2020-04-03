using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class EmpregadoController : ApiController
    {
        List<Empregado> Funcionarios;

        public EmpregadoController()
        {
            Funcionarios = new List<Empregado>
            {
                new Empregado{Id=1,Nome="Joana",Apelido="Matos"},
                new Empregado{Id=2,Nome="Carlota",Apelido="Morais"},
                new Empregado{Id=3,Nome="Rui",Apelido="Santos"},
            };
        }

        /// <summary>
        /// Dados campletos de todos os empregados
        /// </summary>
        /// <returns>lista de empregados</returns>
        // GET: api/Empregado //Perminte ler os dados na api
        public List<Empregado> Get()
        {
            return Funcionarios;
        }
        /// <summary>
        /// Dados completos do empregado
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>empregado</returns>
        // GET: api/Empregado/5 //Perminte ler os dados na api consoante o filtro
        public Empregado Get(int id)
        {
            return Funcionarios.FirstOrDefault(x => x.Id == id);
        }


        // GET: api/Empregado/GetNomes //Perminte ler os nomes 
        /// <summary>
        /// Nome próprio de todos os empregados
        /// </summary>
        /// <returns> lista com os nomes de todos os empregados</returns>

        // Como já tenho varios Get criado devo indicar o novo caminho
        [Route("api/empregado/GetNomes")]
        public List<string> GetNomes()
        {
            List<string> output = new List<string>();

            foreach (var e in Funcionarios)
            {
                output.Add(e.Nome);
            }
            return output;
        }
        /// <summary>
        /// Registo de novo empregado
        /// </summary>
        /// <param name="value">Empregado</param>
        // POST: api/Empregado // Permite criar dados na api 
        public void Post([FromBody]Empregado value)
        {
            Funcionarios.Add(value);
        }

        // PUT: api/Empregado/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Empregado/5 //Permite apagar os dados da api
        public void Delete(int id)
        {
            //Funcionarios.Remove(id);
        }
    }
}
