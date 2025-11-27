using ordem_servico_backend.Models;

namespace ordem_servico_backend.Services.Interface
{
    public interface IJwtTokenService
    {
        Task<string> GenerateTokenAsync(AppUser user);
    }
}
