using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using System.Linq;
using System.Collections.Generic;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
        public static bool VerificaLoginSenha(string Login, string SenhaUsuario, Controller controller) {
            using(BibliotecaContext bc = new BibliotecaContext()) {
                VerificaSeUsuarioAdminExiste(bc);

                SenhaUsuario = Criptografia.TxtCript(SenhaUsuario);

                IQueryable<Usuario> userFind = bc.Usuarios.Where(u => u.Login==Login && u.SenhaUsuario==SenhaUsuario);
                List<Usuario>UserFindList = userFind.ToList();

                if(UserFindList.Count == 0) {
                    return false;
                } else {
                    controller.HttpContext.Session.SetString("Login", UserFindList[0].Login);
                    controller.HttpContext.Session.SetString("Nome", UserFindList[0].NomeUsuario);
                    controller.HttpContext.Session.SetInt32("Tipo", UserFindList[0].Tipo);
                    return true;
                }
            }
        }
        public static void VerificaSeUsuarioAdminExiste(BibliotecaContext bc) {
            IQueryable<Usuario> userFind = bc.Usuarios.Where(u => u.Login=="admin");

            // caso não exista ele cria um padrão
            if(userFind.ToList().Count == 0) {
                Usuario admin = new Usuario();
                admin.Login = "admin";
                admin.SenhaUsuario = Criptografia.TxtCript("123");
                admin.Tipo = Usuario.ADMIN;
                admin.NomeUsuario = "Administrador";

                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }
        }

        public static void VerificaSeUsuarioEAdmin(Controller controller) {
           if(!(controller.HttpContext.Session.GetInt32("Tipo")==Usuario.ADMIN)) {
               controller.Request.HttpContext.Response.Redirect("/Usuario/NeedAdmin");
           } 
        }
    }
}