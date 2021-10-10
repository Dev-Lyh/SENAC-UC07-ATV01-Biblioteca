using System.Linq;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class UsuarioService
    
    {
        public List<Usuario> Listar(){
            using (BibliotecaContext bc = new BibliotecaContext()) {
                return bc.Usuarios.ToList();
            }
        }
        public Usuario Listar(int Id) {
             using(BibliotecaContext bc = new BibliotecaContext()) {
                return bc.Usuarios.Find(Id);
            }
        } 
        public void include(Usuario u) {
            using(BibliotecaContext bc = new BibliotecaContext()) {
                bc.Add(u);
                bc.SaveChanges();
            }
        }

        public void excluirUsuario(int Id) {
            using(BibliotecaContext bc = new BibliotecaContext()) {
                bc.Usuarios.Remove(bc.Usuarios.Find(Id));
                bc.SaveChanges();
            }
        }

        public void editarUsuario(Usuario ue) {
            using(BibliotecaContext bc = new BibliotecaContext()) {
                Usuario u =bc.Usuarios.Find(ue.Id);
                u.NomeUsuario = ue.NomeUsuario;
                u.Login = ue.Login;
                u.SenhaUsuario = ue.SenhaUsuario;
                u.Tipo = ue.Tipo;

                bc.SaveChanges();
            }
        }
    }
}