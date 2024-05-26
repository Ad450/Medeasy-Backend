
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repository;


namespace Application.Services;

public class DayService(
    IBaseRepository<Day> _dayRepository,
    IBaseRepository<Practitioner> _practitionerRepository
) : IDayService
{
    public async Task CreateDays(CreateDaysDto dto)
    {
        using var transaction = await _dayRepository.GetContext().Database.BeginTransactionAsync();
        try
        {
            var days = dto.Days.Select(async d =>
            {
                var practitioner = await _practitionerRepository.GetById(d.PractitionerId)
                    ?? throw new Exception("practitioner not found");

                return new Day
                {
                    Practitioner = practitioner,
                    DayOfWeek = d.DayOfWeek,
                    WeekNumber = d.WeekNumber
                };
            }).ToList();

            await _dayRepository.GetContext().AddRangeAsync(days);
            await _dayRepository.Update();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Exception {e} creating days");
        }
    }

    public ICollection<Day> GetDays()
    {
        return [.. _dayRepository.GetAll()];
    }
}