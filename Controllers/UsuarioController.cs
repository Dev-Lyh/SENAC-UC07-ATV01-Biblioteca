using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
       public IActionResult ListaDeUsuarios() {
           Autenticacao.CheckLogin(this);
           Autenticacao.VerificaSeUsuarioEAdmin(this);

           return View(new UsuarioService().Listar());
       }

       public IActionResult EditarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);

            return View(u);

        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario userEditado)
        {
            UsuarioService us = new UsuarioService();
            us.editarUsuario(userEditado);
            
            return RedirectToAction("ListaDeUsuarios");

        }

       public IActionResult RegistrarUsuario() {
           Autenticacao.CheckLogin(this);
           Autenticacao.VerificaSeUsuarioEAdmin(this);

           return View();
       }

       [HttpPost] 
       public IActionResult RegistrarUsuario(Usuario novoUsuario) {
           Autenticacao.CheckLogin(this);
           Autenticacao.VerificaSeUsuarioEAdmin(this);

           novoUsuario.SenhaUsuario = Criptografia.TxtCript(novoUsuario.SenhaUsuario);

           UsuarioService us = new UsuarioService();
           us.include(novoUsuario);

           return RedirectToAction("cadastroRealizado");
       }

       public IActionResult ExcluirUsuario(int id) {
           Usuario u = new UsuarioService().Listar(id);
           return View(u);
       }

       [HttpPost]
       public IActionResult ExcluirUsuario(string decisao, int id) {
           if(decisao == "EXCLUIR") {
               ViewData["Mensagem"] = "Exclusão do usuário" + new UsuarioService().Listar(id).NomeUsuario + " realizada com sucesso";
               new UsuarioService().excluirUsuario(id);
               return View("ListaDeUsuarios", new UsuarioService().Listar());
           } else {
               ViewData["Mensagem"] = "Exclusão cancelada";
               return View("ListaDeUsuarios", new UsuarioService().Listar());
           }
       }

       public IActionResult cadastroRealizado(){
           Autenticacao.CheckLogin(this);
           Autenticacao.VerificaSeUsuarioEAdmin(this);

           return View();
       }

       public IActionResult NeedAdmin() {
           Autenticacao.CheckLogin(this);
           return View();
       }

       public IActionResult Sair() {
           HttpContext.Session.Clear();
           return RedirectToAction("Index", "Home");
       }

    }
}