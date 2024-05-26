using Application.Dto;

namespace Application.Interfaces;

public interface IKycService
{
    Task Verify(VerifyKycDto dto);
}