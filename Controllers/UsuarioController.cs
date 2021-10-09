using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
       public  IActionResult ListaDeUsuarios() {
           Autenticacao.CheckLogin(this);
           Autenticacao.VerificaSeUsuarioEAdmin(this);

           return View(new UsuarioService().Listar());
       }

       public IActionResult EditarUsuario(int IdUsuario) {
           Usuario u = new UsuarioService().Listar(IdUsuario);

           return View();
       }

       [HttpPost]
       public IActionResult EditarUsuario(Usuario userEditado) {
           UsuarioService us = new UsuarioService();
           us.edit(userEditado);

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

       public IActionResult ExcluirUsuario(int IdUsuario) {
           return View(new UsuarioService().Listar(IdUsuario));
       }

       [HttpPost]
       public IActionResult ExcluirUsuario(string decisao, int IdUsuario) {
           if(decisao == "ECLUIR") {
               ViewData["Mensagem"] = "Exclusão do usuário" + new UsuarioService().Listar(IdUsuario).NomeUsuario + " realizada com sucesso";
               new UsuarioService().delete(IdUsuario);
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