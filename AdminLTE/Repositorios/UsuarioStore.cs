using AdminLTE.Models;
using Microsoft.AspNetCore.Identity;

namespace AdminLTE.Repositorios
{
    public class UsuarioStore : IUserStore<UsuarioViewModel>, IUserEmailStore<UsuarioViewModel>, IUserPasswordStore<UsuarioViewModel>
    {
        private readonly IRepositorioUsuarios repositorioUsuarios;

        public UsuarioStore(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public async Task<IdentityResult> CreateAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            user.IdUsuario = await repositorioUsuarios.CrearUsuario(user);
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public async Task<UsuarioViewModel> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await repositorioUsuarios.BuscarUsuarioPorEmail(normalizedEmail);
        }

        public Task<UsuarioViewModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioViewModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await repositorioUsuarios.BuscarUsuarioPorEmail(normalizedUserName);
        }

        public Task<string> GetEmailAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.IdUsuario.ToString());
        }

        public Task<string> GetUserNameAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> HasPasswordAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(UsuarioViewModel user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(UsuarioViewModel user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(UsuarioViewModel user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.EmailNormalizado = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(UsuarioViewModel user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(UsuarioViewModel user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(UsuarioViewModel user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UsuarioViewModel user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
