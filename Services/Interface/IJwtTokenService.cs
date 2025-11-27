using ordem_servico_backend.Models;

namespace ordem_servico_backend.Services.Interface
{
    public interface IJwtTokenService
    {
        string GenerateToken(AppUser user);
    }
}
