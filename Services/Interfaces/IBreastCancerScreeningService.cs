using System.Collections.Generic;
using System.Threading.Tasks;
using NirmayaMobileApp.DTO;

namespace NirmayaMobileApp.Services.Interfaces
{
    public interface IBreastCancerScreeningService
    {
        Task<List<BreastCancerScreeningDTO>> GetAllScreeningsAsync();
        Task<BreastCancerScreeningDTO> GetScreeningByIdAsync(int id);
        Task<BreastCancerScreeningDTO> CreateScreeningAsync(BreastCancerScreeningDTO screening);
        Task<BreastCancerScreeningDTO> UpdateScreeningAsync(BreastCancerScreeningDTO screening);
        Task<bool> DeleteScreeningAsync(int id);
    }
} 