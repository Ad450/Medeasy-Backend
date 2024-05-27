using Application.Dto;

namespace Application.Interfaces;

public interface IKycService
{
    public Task Verify(VerifyKycDto dto);
}