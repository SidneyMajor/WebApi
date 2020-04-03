using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class LivrosController : ApiController
    {
        // GET: api/Livros
        public List<Livro> Get()
        {
            return Biblioteca.Livros;
        }

        // GET: api/Livros/5
        //IHttpActionResult retorna um resultado da pergunta que faço a api, uma ação de resposta
        public IHttpActionResult Get(int id)
        {
            Livro livro = Biblioteca.Livros.FirstOrDefault(x => x.Id == id);
            if (livro!=null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, livro));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Livro não Localizado"));
        }

        // POST: api/Livros
        public IHttpActionResult Post([FromBody]Livro livro)
        {
            Livro obj= Biblioteca.Livros.FirstOrDefault(x => x.Id == livro.Id);
            if (obj!=null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict, "Já existe um livro registado com esse Id."));
            }
            Biblioteca.Livros.Add(livro);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, "Livro criado com sucesso!"));
        }

        // PUT: api/Livros/5
        public IHttpActionResult Put([FromBody]Livro livro)
        {
            Livro obj = Biblioteca.Livros.FirstOrDefault(x => x.Id == livro.Id);
            if (obj != null)
            {
                obj.Titulo = livro.Titulo;
                obj.Autor = livro.Autor;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Livro não Localizado"));
        }

        // DELETE: api/Livros/5
        public IHttpActionResult Delete(int id)
        {
            Livro obj = Biblioteca.Livros.FirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                Biblioteca.Livros.Remove(obj);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Livro não Localizado"));
        }
    }
}
