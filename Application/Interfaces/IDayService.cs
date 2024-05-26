using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IDayService
{
    public ICollection<Day> GetDays();
    public Task CreateDays(CreateDaysDto dto);
}