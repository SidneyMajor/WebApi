using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class CategoriasController : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        /// <summary>
        /// Dados campletos de todas as categorias
        /// </summary>
        /// <returns>lista de categoria</returns>
        // GET: api/Categorias
        public List<Categoria> Get()
        {
            var lista = from Categoria in dc.Categorias select Categoria;
            return lista.ToList();
        }
        /// <summary>
        /// Dados campletos da categoria
        /// </summary>
        /// <param name="sigla"></param>
        /// <returns>lista</returns>
        // GET: api/Categorias/AC
        //Criar novo route para para envitar o problema a quando da pesquisa de valores singulares na api. 
        [Route("api/categorias/{sigla}")]
        public IHttpActionResult Get(string sigla)
        {
            var lista = dc.Categorias.SingleOrDefault(c=> c.Sigla==sigla);
            if (lista != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, lista));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Categoria não Localizado"));
        }
        /// <summary>
        /// Registo da nova categoria
        /// </summary>
        /// <param name="novaCategoria"></param>
        /// <returns>nova categoria</returns>
        // POST: api/Categorias
        public IHttpActionResult Post([FromBody]Categoria novaCategoria)
        {
            Categoria categoria = dc.Categorias.SingleOrDefault(c => c.Sigla == novaCategoria.Sigla);

            if (categoria != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict, "já existe uma Categoria com essa sigla "));
            }
            //Introduzir a nova categoria na tabela categoria
            dc.Categorias.InsertOnSubmit(novaCategoria);
            try
            {
                //Grava a categoria nova na tabela
                dc.SubmitChanges();
            }
            catch (Exception e)
            {

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }
        /// <summary>
        /// Altera os dados da categoria
        /// </summary>
        /// <param name="novaCategoria"></param>
        /// <returns>nova categoria</returns>
        // PUT: api/Categorias/5
        public IHttpActionResult Put([FromBody]Categoria novaCategoria)
        {
            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == novaCategoria.Sigla);

            if (categoria == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Categoria não Localizado"));
               
            }

            categoria.Sigla = novaCategoria.Sigla;
            categoria.Categoria1 = novaCategoria.Categoria1;
            try
            {
                //Grava a categoria nova na tabela
                dc.SubmitChanges();
            }
            catch (Exception e)
            {

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }
        /// <summary>
        /// Elimina a categoria
        /// </summary>
        /// <param name="sigla"></param>
        /// <returns>resultado da ação</returns>
        // DELETE: api/Categorias/AC
        //Criar novo route para para envitar o problema a quando da pesquisa de valores singulares na api. 
        [Route("api/categorias/{sigla}")]
        public IHttpActionResult Delete(string sigla)
        {
            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == sigla);

            if (categoria != null)
            {
                dc.Categorias.DeleteOnSubmit(categoria);

                try
                {
                    //Grava a categoria nova na tabela
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {

                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Categoria não Localizado"));
        }
    }
}
